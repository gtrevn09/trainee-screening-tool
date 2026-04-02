using TraineeScreeningTool.WinForms.Data;

namespace TraineeScreeningTool.WinForms;

public partial class ForgotPasswordForm : Form
{
    // Constructor - runs when the form opens
    public ForgotPasswordForm()
    {
        InitializeComponent();
    }

    // Generates a temporary password when the user clicks Submit
    private void btnSubmit_Click(object sender, EventArgs e)
    {
        // Validate all fields are filled in
        if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text) ||
            string.IsNullOrWhiteSpace(txtFullName.Text))
        {
            MessageBox.Show("Please fill in all fields.", "Validation Error");
            return;
        }

        using var context = new ApplicationDbContext();

        // Find the user - username, email AND full name must all match
        var user = context.Users.FirstOrDefault(u =>
            u.Username == txtUsername.Text.Trim() &&
            u.Email == txtEmail.Text.Trim() &&
            u.FullName.ToLower() == txtFullName.Text.Trim().ToLower() &&
            u.IsActive);

        if (user == null)
        {
            MessageBox.Show("No account found matching that information. Please check your username, full name and email.",
                "Not Found");
            return;
        }

        // Generate a random temporary password
        var tempPassword = GenerateTempPassword();

        // Save the hashed temp password and flag them to change it
        user.PasswordHash = ApplicationDbContext.HashPassword(tempPassword);
        user.MustChangePassword = true; // Force password change on next login
        context.SaveChanges();

        // Log the password reset
        context.Log(user.Username, "Forgot Password",
            "User requested a temporary password reset");

        // Show the temporary password on screen
        MessageBox.Show(
            $"Your temporary password is:\n\n{tempPassword}\n\nPlease use this to log in and then change your password.",
            "Temporary Password",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        this.Close();
    }

    // Generates a random 8 character temporary password
    private string GenerateTempPassword()
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz23456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 8)
            .Select(s => s[random.Next(s.Length)])
            .ToArray());
    }
}