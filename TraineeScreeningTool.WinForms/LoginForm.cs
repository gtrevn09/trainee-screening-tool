using TraineeScreeningTool.WinForms.Data;

namespace TraineeScreeningTool.WinForms;

public partial class LoginForm : Form
{
    // Tracks whether login was successful
    public bool LoginSuccessful { get; private set; } = false;

    // Stores the logged in username to pass to other forms
    public string LoggedInUsername { get; private set; } = string.Empty;

    // Stores the logged in role to pass to other forms
    public string LoggedInRole { get; private set; } = string.Empty;

    // Constructor - runs when the form opens
    public LoginForm()
    {
        InitializeComponent();
    }

    // Validates credentials against the database when Login is clicked
    private void btnLogin_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
            string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            MessageBox.Show("Please enter a username and password.", "Login Error");
            return;
        }

        using var context = new ApplicationDbContext();

        // Hash the entered password to compare with stored hash
        var hashedPassword = ApplicationDbContext.HashPassword(txtPassword.Text);

        // Look for a matching active user in the database
        var user = context.Users.FirstOrDefault(u =>
            u.Username == txtUsername.Text.Trim() &&
            u.PasswordHash == hashedPassword &&
            u.IsActive);

        if (user != null)
        {
            // Log successful login
            context.Log(user.Username, "Login", "User logged in successfully");

            LoginSuccessful = true;
            LoggedInUsername = user.Username;
            LoggedInRole = user.Role;
            this.Close();
        }
        else
        {
            // Log failed login attempt
            context.Log(txtUsername.Text.Trim(), "Login Failed",
                $"Failed login attempt for username: {txtUsername.Text.Trim()}", false);

            MessageBox.Show("Invalid username or password.", "Login Failed");
            txtPassword.Clear();
            txtPassword.Focus();
        }
    }

    // Allow pressing Enter to submit the login form
    private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            btnLogin_Click(sender, e);
        }
    }
}