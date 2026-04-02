namespace TraineeScreeningTool.WinForms;

// This file handles the visual layout of the Log Viewer form
partial class LogViewerForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        dataGridViewLogs = new DataGridView();
        btnRefresh = new Button();
        btnClearLogs = new Button();
        Label lblTitle = new Label();

        // Title label
        lblTitle.Text = "Activity Log";
        lblTitle.Location = new Point(12, 12);
        lblTitle.Size = new Size(760, 25);
        lblTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;

        // Logs grid
        dataGridViewLogs.Location = new Point(12, 45);
        dataGridViewLogs.Size = new Size(760, 450);
        dataGridViewLogs.Name = "dataGridViewLogs";
        dataGridViewLogs.ReadOnly = true;
        dataGridViewLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridViewLogs.MultiSelect = false;

        // Refresh button
        btnRefresh.Location = new Point(12, 510);
        btnRefresh.Size = new Size(130, 35);
        btnRefresh.Text = "Refresh";
        btnRefresh.Click += btnRefresh_Click;

        // Clear Logs button - red to indicate danger
        btnClearLogs.Location = new Point(152, 510);
        btnClearLogs.Size = new Size(130, 35);
        btnClearLogs.Text = "Clear All Logs";
        btnClearLogs.BackColor = Color.IndianRed;
        btnClearLogs.ForeColor = Color.White;
        btnClearLogs.Click += btnClearLogs_Click;

        // Add all controls
        ClientSize = new Size(784, 560);
        Controls.Add(lblTitle);
        Controls.Add(dataGridViewLogs);
        Controls.Add(btnRefresh);
        Controls.Add(btnClearLogs);
        Text = "Activity Log";
        StartPosition = FormStartPosition.CenterScreen;
    }

    // Declare all controls
    private DataGridView dataGridViewLogs;
    private Button btnRefresh;
    private Button btnClearLogs;
}