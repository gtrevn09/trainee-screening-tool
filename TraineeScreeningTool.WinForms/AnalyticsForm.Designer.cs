namespace TraineeScreeningTool.WinForms;

partial class AnalyticsForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pnlStats = new Panel();
        lblStatsTitle = new Label();
        lblTotalCandidates = new Label();
        lblTotalPlacements = new Label();
        lblTotalCerts = new Label();
        tabMain = new TabControl();
        tabPagePathway = new TabPage();
        lblPathwayNote = new Label();
        dgvByPathway = new DataGridView();
        tabPageCertification = new TabPage();
        dgvByCertification = new DataGridView();
        tabPagePassFail = new TabPage();
        lblPassFailNote = new Label();
        dgvPassFail = new DataGridView();
        pnlButtons = new Panel();
        btnExportCsv = new Button();
        btnClose = new Button();

        // ── Summary stats panel ───────────────────────────────────────────────
        pnlStats.Location = new Point(12, 12);
        pnlStats.Size = new Size(972, 46);
        pnlStats.BorderStyle = BorderStyle.FixedSingle;
        pnlStats.BackColor = Color.FromArgb(235, 245, 255);
        pnlStats.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        lblStatsTitle.Text = "Summary:";
        lblStatsTitle.Location = new Point(8, 13);
        lblStatsTitle.Size = new Size(70, 20);
        lblStatsTitle.Font = new Font(lblStatsTitle.Font, FontStyle.Bold);
        lblStatsTitle.AutoSize = true;

        lblTotalCandidates.Text = "Total Candidates: —";
        lblTotalCandidates.Location = new Point(90, 13);
        lblTotalCandidates.Size = new Size(220, 20);
        lblTotalCandidates.AutoSize = true;

        lblTotalPlacements.Text = "Successful Placements: —";
        lblTotalPlacements.Location = new Point(330, 13);
        lblTotalPlacements.Size = new Size(250, 20);
        lblTotalPlacements.AutoSize = true;

        lblTotalCerts.Text = "Certifications Earned: —";
        lblTotalCerts.Location = new Point(600, 13);
        lblTotalCerts.Size = new Size(240, 20);
        lblTotalCerts.AutoSize = true;

        pnlStats.Controls.Add(lblStatsTitle);
        pnlStats.Controls.Add(lblTotalCandidates);
        pnlStats.Controls.Add(lblTotalPlacements);
        pnlStats.Controls.Add(lblTotalCerts);

        // ── Tab 1 — Scores by Career Pathway ─────────────────────────────────
        lblPathwayNote.Text =
            "Composite = mean of available overall percentile scores (CCAT, CBST, CMRA, Typing, CAST).  " +
            $"Pass threshold: {PassThreshold}th percentile.";
        lblPathwayNote.Location = new Point(4, 6);
        lblPathwayNote.Size = new Size(958, 18);
        lblPathwayNote.ForeColor = Color.DimGray;
        lblPathwayNote.AutoSize = true;
        lblPathwayNote.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        dgvByPathway.Location = new Point(4, 30);
        dgvByPathway.Size = new Size(958, 438);
        dgvByPathway.Name = "dgvByPathway";
        dgvByPathway.ReadOnly = true;
        dgvByPathway.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvByPathway.MultiSelect = false;
        dgvByPathway.AllowUserToAddRows = false;
        dgvByPathway.AllowUserToDeleteRows = false;
        dgvByPathway.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        dgvByPathway.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

        tabPagePathway.Text = "Scores by Career Pathway";
        tabPagePathway.Padding = new Padding(4);
        tabPagePathway.Controls.Add(lblPathwayNote);
        tabPagePathway.Controls.Add(dgvByPathway);

        // ── Tab 2 — Certification Outcomes ───────────────────────────────────
        dgvByCertification.Location = new Point(4, 8);
        dgvByCertification.Size = new Size(958, 460);
        dgvByCertification.Name = "dgvByCertification";
        dgvByCertification.ReadOnly = true;
        dgvByCertification.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvByCertification.MultiSelect = false;
        dgvByCertification.AllowUserToAddRows = false;
        dgvByCertification.AllowUserToDeleteRows = false;
        dgvByCertification.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        dgvByCertification.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

        tabPageCertification.Text = "Certification Outcomes";
        tabPageCertification.Padding = new Padding(4);
        tabPageCertification.Controls.Add(dgvByCertification);

        // ── Tab 3 — Pass vs. Fail Analysis ───────────────────────────────────
        lblPassFailNote.Text =
            $"Pass = composite ≥ {PassThreshold}th percentile.  " +
            "Only candidates with at least one percentile score on record are included.";
        lblPassFailNote.Location = new Point(4, 6);
        lblPassFailNote.Size = new Size(958, 18);
        lblPassFailNote.ForeColor = Color.DimGray;
        lblPassFailNote.AutoSize = true;
        lblPassFailNote.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        dgvPassFail.Location = new Point(4, 30);
        dgvPassFail.Size = new Size(958, 438);
        dgvPassFail.Name = "dgvPassFail";
        dgvPassFail.ReadOnly = true;
        dgvPassFail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPassFail.MultiSelect = false;
        dgvPassFail.AllowUserToAddRows = false;
        dgvPassFail.AllowUserToDeleteRows = false;
        dgvPassFail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        dgvPassFail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

        tabPagePassFail.Text = "Pass vs. Fail Analysis";
        tabPagePassFail.Padding = new Padding(4);
        tabPagePassFail.Controls.Add(lblPassFailNote);
        tabPagePassFail.Controls.Add(dgvPassFail);

        // ── TabControl ────────────────────────────────────────────────────────
        tabMain.Location = new Point(12, 66);
        tabMain.Size = new Size(972, 502);
        tabMain.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        tabMain.TabPages.Add(tabPagePathway);
        tabMain.TabPages.Add(tabPageCertification);
        tabMain.TabPages.Add(tabPagePassFail);

        // ── Buttons panel ─────────────────────────────────────────────────────
        pnlButtons.Location = new Point(12, 576);
        pnlButtons.Size = new Size(972, 48);
        pnlButtons.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

        btnExportCsv.Location = new Point(0, 6);
        btnExportCsv.Size = new Size(180, 35);
        btnExportCsv.Text = "Export Current Tab as CSV";
        btnExportCsv.BackColor = Color.SteelBlue;
        btnExportCsv.ForeColor = Color.White;
        btnExportCsv.FlatStyle = FlatStyle.Flat;
        btnExportCsv.Click += btnExportCsv_Click;

        btnClose.Location = new Point(190, 6);
        btnClose.Size = new Size(100, 35);
        btnClose.Text = "Close";
        btnClose.Click += (s, e) => this.Close();

        pnlButtons.Controls.Add(btnExportCsv);
        pnlButtons.Controls.Add(btnClose);

        // ── Form ──────────────────────────────────────────────────────────────
        ClientSize = new Size(996, 636);
        Controls.Add(pnlStats);
        Controls.Add(tabMain);
        Controls.Add(pnlButtons);
        Text = "Analytics";
        StartPosition = FormStartPosition.CenterParent;
        MinimumSize = new Size(900, 600);
    }

    // Control declarations
    private Panel pnlStats;
    private Label lblStatsTitle;
    private Label lblTotalCandidates;
    private Label lblTotalPlacements;
    private Label lblTotalCerts;
    private TabControl tabMain;
    private TabPage tabPagePathway;
    private Label lblPathwayNote;
    private DataGridView dgvByPathway;
    private TabPage tabPageCertification;
    private DataGridView dgvByCertification;
    private TabPage tabPagePassFail;
    private Label lblPassFailNote;
    private DataGridView dgvPassFail;
    private Panel pnlButtons;
    private Button btnExportCsv;
    private Button btnClose;
}
