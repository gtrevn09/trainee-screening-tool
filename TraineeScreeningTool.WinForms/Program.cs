using TraineeScreeningTool.WinForms.Data;

namespace TraineeScreeningTool.WinForms;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Wipe and recreate database to add the new AppLogs table
        using var context = new ApplicationDbContext();

        context.Database.EnsureCreated();  // Recreate with AppLogs table

        // Show login form first
        var loginForm = new LoginForm();
        loginForm.ShowDialog();

        // Only open the main dashboard if login was successful
        if (loginForm.LoginSuccessful)
        {
            Application.Run(new MainForm(loginForm.LoggedInUsername));
        }
    }
}