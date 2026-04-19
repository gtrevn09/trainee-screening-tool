namespace TraineeScreeningTool.WinForms;

partial class MainForm
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
        txtSearch = new TextBox();
        pnlNotification = new Panel();
        lblNotification = new Label();
        dataGridView1 = new DataGridView();
        btnAddCandidate = new Button();
        btnImport = new Button();
        btnImportPdf = new Button();
        btnViewPdf = new Button();
        btnAssess = new Button();
        btnProfile = new Button();
        btnDelete = new Button();
        btnUserDetails = new Button();
        btnLogout = new Button();
        btnViewLogs = new Button();
        btnAnalytics = new Button();

        // Search box at the top
        txtSearch.Location = new Point(12, 12);
        txtSearch.Size = new Size(760, 25);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Search by name or email...";
        txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtSearch.TextChanged += txtSearch_TextChanged;

        // Notification banner
        pnlNotification.Location = new Point(12, 44);
        pnlNotification.Size = new Size(760, 28);
        pnlNotification.BackColor = Color.FromArgb(255, 220, 100);
        pnlNotification.Cursor = Cursors.Hand;
        pnlNotification.Visible = false;
        pnlNotification.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlNotification.Click += pnlNotification_Click;

        lblNotification.Text = "";
        lblNotification.Location = new Point(6, 5);
        lblNotification.Size = new Size(748, 18);
        lblNotification.Font = new Font(lblNotification.Font, FontStyle.Bold);
        lblNotification.Cursor = Cursors.Hand;
        lblNotification.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblNotification.Click += pnlNotification_Click;

        pnlNotification.Controls.Add(lblNotification);

        // DataGridView
        dataGridView1.Location = new Point(12, 78);
        dataGridView1.Size = new Size(760, 284);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.ReadOnly = false;
        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridView1.MultiSelect = true;
        dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

        // Row 1 buttons
        btnAddCandidate.Location = new Point(12, 375);
        btnAddCandidate.Size = new Size(90, 35);
        btnAddCandidate.Text = "Add";
        btnAddCandidate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnAddCandidate.Click += btnAddCandidate_Click;

        btnImport.Location = new Point(108, 375);
        btnImport.Size = new Size(120, 35);
        btnImport.Text = "Import CSV";
        btnImport.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnImport.Click += btnImport_Click;

        btnImportPdf.Location = new Point(234, 375);
        btnImportPdf.Size = new Size(125, 35);
        btnImportPdf.Text = "Import PDF's";
        btnImportPdf.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnImportPdf.Click += btnImportPdf_Click;

        btnViewPdf.Location = new Point(365, 375);
        btnViewPdf.Size = new Size(110, 35);
        btnViewPdf.Text = "View PDF's";
        btnViewPdf.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnViewPdf.Click += btnViewPdf_Click;

        btnAssess.Location = new Point(481, 375);
        btnAssess.Size = new Size(90, 35);
        btnAssess.Text = "Assess";
        btnAssess.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnAssess.Click += btnAssess_Click;

        btnDelete.Location = new Point(577, 375);
        btnDelete.Size = new Size(90, 35);
        btnDelete.Text = "Delete";
        btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnDelete.Click += btnDelete_Click;

        btnProfile.Location = new Point(492, 420);
        btnProfile.Size = new Size(160, 35);
        btnProfile.Text = "Candidate Profile";
        btnProfile.BackColor = Color.FromArgb(60, 120, 60);
        btnProfile.ForeColor = Color.White;
        btnProfile.FlatStyle = FlatStyle.Flat;
        btnProfile.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnProfile.Click += btnProfile_Click;

        // Row 2 buttons
        btnUserDetails.Location = new Point(12, 420);
        btnUserDetails.Size = new Size(120, 35);
        btnUserDetails.Text = "User Details";
        btnUserDetails.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnUserDetails.Click += btnUserDetails_Click;

        btnLogout.Location = new Point(138, 420);
        btnLogout.Size = new Size(100, 35);
        btnLogout.Text = "Logout";
        btnLogout.BackColor = Color.IndianRed;
        btnLogout.ForeColor = Color.White;
        btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnLogout.Click += btnLogout_Click;

        btnViewLogs.Location = new Point(244, 420);
        btnViewLogs.Size = new Size(100, 35);
        btnViewLogs.Text = "View Logs";
        btnViewLogs.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnViewLogs.Click += btnViewLogs_Click;

        btnAnalytics.Location = new Point(350, 420);
        btnAnalytics.Size = new Size(100, 35);
        btnAnalytics.Text = "Analytics";
        btnAnalytics.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnAnalytics.Click += btnAnalytics_Click;

        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(784, 470);
        Controls.Add(txtSearch);
        Controls.Add(pnlNotification);
        Controls.Add(dataGridView1);
        Controls.Add(btnAddCandidate);
        Controls.Add(btnImport);
        Controls.Add(btnImportPdf);
        Controls.Add(btnViewPdf);
        Controls.Add(btnAssess);
        Controls.Add(btnDelete);
        Controls.Add(btnUserDetails);
        Controls.Add(btnLogout);
        Controls.Add(btnViewLogs);
        Controls.Add(btnAnalytics);
        Controls.Add(btnProfile);
        Text = "LIFE Works Trainee Screening Tool";
        MinimumSize = new Size(920, 510);
    }

    private TextBox txtSearch;
    private Panel pnlNotification;
    private Label lblNotification;
    private DataGridView dataGridView1;
    private Button btnAddCandidate;
    private Button btnImport;
    private Button btnImportPdf;
    private Button btnViewPdf;
    private Button btnAssess;
    private Button btnProfile;
    private Button btnDelete;
    private Button btnUserDetails;
    private Button btnLogout;
    private Button btnViewLogs;
    private Button btnAnalytics;
}