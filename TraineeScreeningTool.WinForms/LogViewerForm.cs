using TraineeScreeningTool.WinForms.Data;

namespace TraineeScreeningTool.WinForms;

public partial class LogViewerForm : Form
{
    // Constructor - runs when the form opens
    public LogViewerForm()
    {
        InitializeComponent();
        LoadLogs();
    }

    // Fetches all logs from the database and displays them in the grid
    private void LoadLogs()
    {
        using var context = new ApplicationDbContext();

        // Get logs ordered by most recent first
        var logs = context.AppLogs
            .OrderByDescending(l => l.Timestamp)
            .ToList();

        dataGridViewLogs.DataSource = logs;

        // Auto size all columns to fit their content
        dataGridViewLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        // Make the Details column stretch to fill remaining space
        dataGridViewLogs.Columns["Details"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
        context.AppLogs.RemoveRange(context.AppLogs);
        context.SaveChanges();

        LoadLogs();
        MessageBox.Show("Logs cleared successfully.", "Success");
    }

    // Refreshes the log grid
    private void btnRefresh_Click(object sender, EventArgs e)
    {
        LoadLogs();
    }
}