using Microsoft.EntityFrameworkCore;
using TraineeScreeningTool.WinForms.Data;

namespace TraineeScreeningTool.WinForms;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        using var context = new ApplicationDbContext();
        
        context.Database.EnsureCreated();

        // Create JobPlacements table for existing databases that pre-date this feature
        context.Database.ExecuteSqlRaw(@"
            CREATE TABLE IF NOT EXISTS JobPlacements (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                CandidateId INTEGER NOT NULL,
                CareerPathway TEXT NOT NULL DEFAULT '',
                PlacementDate TEXT NOT NULL,
                ExitDate TEXT NULL,
                IsSuccessful INTEGER NOT NULL DEFAULT 0
            );
        ");

        // Create Certifications table for existing databases that pre-date this feature
        context.Database.ExecuteSqlRaw(@"
            CREATE TABLE IF NOT EXISTS Certifications (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                CandidateId INTEGER NOT NULL,
                CertName TEXT NOT NULL DEFAULT '',
                Status TEXT NOT NULL DEFAULT 'Pursuing',
                Result TEXT NULL,
                Date TEXT NULL,
                Notes TEXT NOT NULL DEFAULT ''
            );
        ");

        var loginForm = new LoginForm();
        loginForm.ShowDialog();

        if (loginForm.LoginSuccessful)
        {
            var user = context.Users.FirstOrDefault(u =>
                u.Username == loginForm.LoggedInUsername);

            if (user != null && user.IsFirstLogin)
            {
                var setupForm = new FirstTimeSetupForm(loginForm.LoggedInUsername);
                setupForm.ShowDialog();
                if (!setupForm.SetupCompleted) return;
            }
            else if (user != null && user.MustChangePassword)
            {
                MessageBox.Show(
                    "You must change your password before continuing.",
                    "Password Change Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                var changePasswordForm = new ChangePasswordForm(loginForm.LoggedInUsername);
                changePasswordForm.ShowDialog();

                user.MustChangePassword = false;
                context.SaveChanges();
            }

            Application.Run(new MainForm(loginForm.LoggedInUsername));
        }
    }
}