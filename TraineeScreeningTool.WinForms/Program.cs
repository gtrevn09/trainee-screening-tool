using TraineeScreeningTool.WinForms.Data;

namespace TraineeScreeningTool.WinForms;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Set up database
        using var context = new ApplicationDbContext();
        
        context.Database.EnsureCreated();

        // Show login form first
        var loginForm = new LoginForm();
        loginForm.ShowDialog();

        if (loginForm.LoginSuccessful)
        {
            var user = context.Users.FirstOrDefault(u =>
                u.Username == loginForm.LoggedInUsername);

            if (user != null && user.IsFirstLogin)
            {
                // First time login - show full setup form
                var setupForm = new FirstTimeSetupForm(loginForm.LoggedInUsername);
                setupForm.ShowDialog();
                if (!setupForm.SetupCompleted) return;
            }
            else if (user != null && user.MustChangePassword)
            {
                // Password reset - force them to change password before continuing
                MessageBox.Show(
                    "You must change your password before continuing.",
                    "Password Change Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                var changePasswordForm = new ChangePasswordForm(loginForm.LoggedInUsername);
                changePasswordForm.ShowDialog();

                // Mark password change as complete
                user.MustChangePassword = false;
                context.SaveChanges();
            }

            // Open the main dashboard
            Application.Run(new MainForm(loginForm.LoggedInUsername));
        }
    }
}