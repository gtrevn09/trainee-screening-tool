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

        // Title label - increased height to prevent clipping
        lblTitle.Text = "Activity Log";
        lblTitle.Location = new Point(12, 12);
        lblTitle.Size = new Size(760, 35);
        lblTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;

        // Logs grid - moved down to give title room
        dataGridViewLogs.Location = new Point(12, 55);
        dataGridViewLogs.Size = new Size(760, 440);
        dataGridViewLogs.Name = "dataGridViewLogs";
        dataGridViewLogs.ReadOnly = true;
        dataGridViewLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridViewLogs.MultiSelect = false;
        dataGridViewLogs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

        // Refresh button
        btnRefresh.Location = new Point(12, 510);
        btnRefresh.Size = new Size(130, 35);
        btnRefresh.Text = "Refresh";
        btnRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnRefresh.Click += btnRefresh_Click;

        // Clear Logs button
        btnClearLogs.Location = new Point(152, 510);
        btnClearLogs.Size = new Size(130, 35);
        btnClearLogs.Text = "Clear All Logs";
        btnClearLogs.BackColor = Color.IndianRed;
        btnClearLogs.ForeColor = Color.White;
        btnClearLogs.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnClearLogs.Click += btnClearLogs_Click;

        // Add all controls
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(784, 560);
        Controls.Add(lblTitle);
        Controls.Add(dataGridViewLogs);
        Controls.Add(btnRefresh);
        Controls.Add(btnClearLogs);
        Text = "Activity Log";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.Sizable;
        MinimumSize = new Size(600, 400);
    }

    // Declare all controls
    private DataGridView dataGridViewLogs;
    private Button btnRefresh;
    private Button btnClearLogs;
}