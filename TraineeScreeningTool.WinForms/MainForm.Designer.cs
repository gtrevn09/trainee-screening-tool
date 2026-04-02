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
        dataGridView1 = new DataGridView();
        btnAddCandidate = new Button();
        btnImport = new Button();
        btnAssess = new Button();
        btnDetails = new Button();
        btnDelete = new Button();
        btnUserDetails = new Button();
        btnLogout = new Button();
        btnViewLogs = new Button();

        // Search box
        txtSearch.Location = new Point(12, 12);
        txtSearch.Size = new Size(760, 25);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Search by name or email...";
        txtSearch.TextChanged += txtSearch_TextChanged;

        // DataGridView
        dataGridView1.Location = new Point(12, 47);
        dataGridView1.Size = new Size(760, 315);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.ReadOnly = false;
        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridView1.MultiSelect = true;

        // Row 1 buttons
        btnAddCandidate.Location = new Point(12, 375);
        btnAddCandidate.Size = new Size(130, 35);
        btnAddCandidate.Text = "Add Candidate";
        btnAddCandidate.Click += btnAddCandidate_Click;

        btnImport.Location = new Point(152, 375);
        btnImport.Size = new Size(130, 35);
        btnImport.Text = "Import CSV";
        btnImport.Click += btnImport_Click;

        btnAssess.Location = new Point(292, 375);
        btnAssess.Size = new Size(130, 35);
        btnAssess.Text = "Assess";
        btnAssess.Click += btnAssess_Click;

        btnDetails.Location = new Point(432, 375);
        btnDetails.Size = new Size(130, 35);
        btnDetails.Text = "Details";
        btnDetails.Click += btnDetails_Click;

        btnDelete.Location = new Point(572, 375);
        btnDelete.Size = new Size(130, 35);
        btnDelete.Text = "Delete";
        btnDelete.Click += btnDelete_Click;

        // Row 2 buttons
        btnUserDetails.Location = new Point(12, 420);
        btnUserDetails.Size = new Size(130, 35);
        btnUserDetails.Text = "User Details";
        btnUserDetails.Click += btnUserDetails_Click;

        btnLogout.Location = new Point(152, 420);
        btnLogout.Size = new Size(130, 35);
        btnLogout.Text = "Logout";
        btnLogout.BackColor = Color.IndianRed;
        btnLogout.ForeColor = Color.White;
        btnLogout.Click += btnLogout_Click;

        btnViewLogs.Location = new Point(292, 420);
        btnViewLogs.Size = new Size(130, 35);
        btnViewLogs.Text = "View Logs";
        btnViewLogs.Click += btnViewLogs_Click;

        // Add all controls
        ClientSize = new Size(784, 470);
        Controls.Add(txtSearch);
        Controls.Add(dataGridView1);
        Controls.Add(btnAddCandidate);
        Controls.Add(btnImport);
        Controls.Add(btnAssess);
        Controls.Add(btnDetails);
        Controls.Add(btnDelete);
        Controls.Add(btnUserDetails);
        Controls.Add(btnLogout);
        Controls.Add(btnViewLogs);
        Text = "LIFE Works Trainee Screening Tool";
    }

    // Declare all controls
    private TextBox txtSearch;
    private DataGridView dataGridView1;
    private Button btnAddCandidate;
    private Button btnImport;
    private Button btnAssess;
    private Button btnDetails;
    private Button btnDelete;
    private Button btnUserDetails;
    private Button btnLogout;
    private Button btnViewLogs;
}