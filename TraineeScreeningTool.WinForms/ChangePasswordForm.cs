using TraineeScreeningTool.WinForms.Data;

namespace TraineeScreeningTool.WinForms;

public partial class ChangePasswordForm : Form
{
    // Stores the username of the currently logged in user
    private readonly string _username;

    // Constructor - accepts the logged in username from MainForm
    public ChangePasswordForm(string username)
    {
        InitializeComponent();
        _username = username;
    }

    // Saves the new password when Save is clicked
    private void btnSave_Click(object sender, EventArgs e)
    {
        // Make sure all fields are filled in
        if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text) ||
            string.IsNullOrWhiteSpace(txtNewPassword.Text) ||
            string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
        {
            MessageBox.Show("Please fill in all fields.", "Validation Error");
            return;
        }

        // Make sure new passwords match
        if (txtNewPassword.Text != txtConfirmPassword.Text)
        {
            MessageBox.Show("New passwords do not match.", "Validation Error");
            txtConfirmPassword.Clear();
            return;
        }

        // Make sure new password is at least 6 characters
        if (txtNewPassword.Text.Length < 6)
        {
            MessageBox.Show("Password must be at least 6 characters.", "Validation Error");
            return;
        }

        using var context = new ApplicationDbContext(); // Open database connection

        // Hash the current password to verify it
        var currentHash = ApplicationDbContext.HashPassword(txtCurrentPassword.Text);

        // Find the user and verify their current password
        var user = context.Users.FirstOrDefault(u =>
            u.Username == _username &&
            u.PasswordHash == currentHash);

        if (user == null)
        {
            // Current password was wrong
            MessageBox.Show("Current password is incorrect.", "Error");
            txtCurrentPassword.Clear();
            return;
        }

        // Update the password with the new hashed version
        user.PasswordHash = ApplicationDbContext.HashPassword(txtNewPassword.Text);
        context.SaveChanges();

        MessageBox.Show("Password changed successfully!", "Success");
        this.Close();
    }
}