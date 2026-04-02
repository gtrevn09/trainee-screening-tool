using TraineeScreeningTool.WinForms.Data;

namespace TraineeScreeningTool.WinForms;

public partial class LogViewerForm : Form
{
    // Constructor - runs when the form opens
    public LogViewerForm()
    {
        InitializeComponent();
        LoadLogs(); // Load all logs into the grid on open
    }

    // Fetches all logs from the database and displays them in the grid
    private void LoadLogs()
    {
        using var context = new ApplicationDbContext(); // Open database connection

        // Get logs ordered by most recent first
        var logs = context.AppLogs
            .OrderByDescending(l => l.Timestamp)
            .ToList();

        dataGridViewLogs.DataSource = logs; // Bind logs to grid
        dataGridViewLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
    }

    // Clears all logs from the database
    private void btnClearLogs_Click(object sender, EventArgs e)
    {
        var confirm = MessageBox.Show(
            "Are you sure you want to clear all logs? This cannot be undone.",
            "Confirm Clear Logs",
            MessageBoxButtons.YesNo);

        if (confirm != DialogResult.Yes) return;

        using var context = new ApplicationDbContext();
        context.AppLogs.RemoveRange(context.AppLogs); // Remove all logs
        context.SaveChanges();

        LoadLogs(); // Refresh the grid
        MessageBox.Show("Logs cleared successfully.", "Success");
    }

    // Refreshes the log grid
    private void btnRefresh_Click(object sender, EventArgs e)
    {
        LoadLogs(); // Reload logs from database
    }
}