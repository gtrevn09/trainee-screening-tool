using System.Text.RegularExpressions;
using TraineeScreeningTool.WinForms.Data;

namespace TraineeScreeningTool.WinForms;

public partial class UserDetailsForm : Form
{
    // Stores the logged in username
    private readonly string _username;

    // Constructor - accepts the logged in username
    public UserDetailsForm(string username)
    {
        InitializeComponent();
        _username = username;
        LoadUserDetails();
    }

    // Loads the user details from the database
    private void LoadUserDetails()
    {
        using var context = new ApplicationDbContext();
        var user = context.Users.FirstOrDefault(u => u.Username == _username);
        if (user == null) return;

        // Populate labels and text boxes with user data
        lblUsernameValue.Text = user.Username;
        lblRoleValue.Text = user.Role;
        lblPasswordValue.Text = "••••••••";

        // Split full name into first and last for the text boxes
        var nameParts = user.FullName.Split(' ', 2);
        txtFirstName.Text = nameParts.Length > 0 ? nameParts[0] : string.Empty;
        txtLastName.Text = nameParts.Length > 1 ? nameParts[1] : string.Empty;
        txtEmail.Text = user.Email;
    }

    // Validates that the email address is in a proper format
    private bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    // Updates the user details when Update Details is clicked
    private void btnUpdateDetails_Click(object sender, EventArgs e)
    {
        // Validate all fields are filled in
        if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
            string.IsNullOrWhiteSpace(txtLastName.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text))
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

        using var context = new ApplicationDbContext();
        var user = context.Users.FirstOrDefault(u => u.Username == _username);

        if (user != null)
        {
            // Update user details
            user.FullName = $"{txtFirstName.Text.Trim()} {txtLastName.Text.Trim()}";
            user.Email = txtEmail.Text.Trim();
            context.SaveChanges();

            context.Log(_username, "Update Details",
                $"User updated their details. Name: {user.FullName}, Email: {user.Email}");

            MessageBox.Show("Details updated successfully!", "Success");
            LoadUserDetails(); // Refresh the form
        }
    }

    // Opens the Change Password form
    private void btnChangePassword_Click(object sender, EventArgs e)
    {
        var form = new ChangePasswordForm(_username);
        form.ShowDialog();
    }
}