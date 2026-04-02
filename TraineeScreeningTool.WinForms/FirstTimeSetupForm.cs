using System.Text.RegularExpressions;
using TraineeScreeningTool.WinForms.Data;

namespace TraineeScreeningTool.WinForms;

public partial class FirstTimeSetupForm : Form
{
    // Stores the username of the user setting up their account
    private readonly string _username;

    // Tracks whether setup was completed successfully
    public bool SetupCompleted { get; private set; } = false;

    // Constructor - accepts the username from LoginForm
    public FirstTimeSetupForm(string username)
    {
        InitializeComponent();
        _username = username;

        // Prevent closing without completing setup
        this.FormClosing += FirstTimeSetupForm_FormClosing;
    }

    // Prevents the user from closing the form without completing setup
    private void FirstTimeSetupForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (!SetupCompleted && e.CloseReason == CloseReason.UserClosing)
        {
            MessageBox.Show("You must complete the account setup before continuing.",
                "Setup Required");
            e.Cancel = true;
        }
    }

    // Validates that the email address is in a proper format
    private bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    // Saves the setup data when Save is clicked
    private void btnSave_Click(object sender, EventArgs e)
    {
        // Validate all fields are filled in
        if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
            string.IsNullOrWhiteSpace(txtLastName.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text) ||
            string.IsNullOrWhiteSpace(txtNewPassword.Text) ||
            string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
        {
            MessageBox.Show("Please fill in all fields.", "Validation Error");
            return;
        }

        // Validate email format
        if (!IsValidEmail(txtEmail.Text.Trim()))
        {
            MessageBox.Show("Please enter a valid email address.", "Validation Error");
            txtEmail.Focus();
            return;
        }

        // Make sure passwords match
        if (txtNewPassword.Text != txtConfirmPassword.Text)
        {
            MessageBox.Show("Passwords do not match.", "Validation Error");
            txtConfirmPassword.Clear();
            return;
        }

        // Make sure password is at least 6 characters
        if (txtNewPassword.Text.Length < 6)
        {
            MessageBox.Show("Password must be at least 6 characters.", "Validation Error");
            return;
        }

        using var context = new ApplicationDbContext();

        var user = context.Users.FirstOrDefault(u => u.Username == _username);

        if (user != null)
        {
            // Combine first and last name into full name
            user.FullName = $"{txtFirstName.Text.Trim()} {txtLastName.Text.Trim()}";
            user.Email = txtEmail.Text.Trim();
            user.PasswordHash = ApplicationDbContext.HashPassword(txtNewPassword.Text);
            user.IsFirstLogin = false;

            context.SaveChanges();

            context.Log(_username, "First Time Setup",
                $"User completed account setup. Name: {user.FullName}, Email: {user.Email}");
        }

        SetupCompleted = true;
        MessageBox.Show("Account setup complete! Welcome to the LIFE Works Screening Tool.",
            "Setup Complete");
        this.Close();
    }
}