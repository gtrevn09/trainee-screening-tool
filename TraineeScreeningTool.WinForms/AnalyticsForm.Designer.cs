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
        flpStats = new FlowLayoutPanel();
        lblStatsTitle = new Label();
        lblTotalCandidates = new Label();
        lblSep1 = new Label();
        lblTotalPlacements = new Label();
        lblSep2 = new Label();
        lblTotalCerts = new Label();
        tabMain = new TabControl();
        tabPagePathway = new TabPage();
        lblPathwayNote = new Label();
        dgvByPathway = new DataGridView();
        lblEmptyPathway = new Label();
        tabPageCertification = new TabPage();
        lblCertNote = new Label();
        dgvByCertification = new DataGridView();
        lblEmptyCert = new Label();
        tabPagePassFail = new TabPage();
        lblPassFailNote = new Label();
        dgvPassFail = new DataGridView();
        lblEmptyPassFail = new Label();
        pnlButtons = new Panel();
        btnExportCsv = new Button();
        btnClose = new Button();

        // ── Summary stats panel ───────────────────────────────────────────────
        // FlowLayoutPanel lets each label auto-size; labels can't overlap or clip.
        flpStats.Location = new Point(12, 12);
        flpStats.Size = new Size(972, 46);
        flpStats.BorderStyle = BorderStyle.FixedSingle;
        flpStats.BackColor = Color.FromArgb(235, 245, 255);
        flpStats.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        flpStats.FlowDirection = FlowDirection.LeftToRight;
        flpStats.WrapContents = false;
        flpStats.Padding = new Padding(6, 0, 0, 0);

        lblStatsTitle.Text = "Summary:";
        lblStatsTitle.AutoSize = true;
        lblStatsTitle.Font = new Font(lblStatsTitle.Font, FontStyle.Bold);
        lblStatsTitle.Margin = new Padding(0, 13, 8, 0);

        lblTotalCandidates.Text = "Total Candidates: —";
        lblTotalCandidates.AutoSize = true;
        lblTotalCandidates.Margin = new Padding(0, 13, 0, 0);

        lblSep1.Text = "|";
        lblSep1.AutoSize = true;
        lblSep1.ForeColor = Color.SteelBlue;
        lblSep1.Margin = new Padding(12, 13, 12, 0);

        lblTotalPlacements.Text = "Successful Placements: —";
        lblTotalPlacements.AutoSize = true;
        lblTotalPlacements.Margin = new Padding(0, 13, 0, 0);

        lblSep2.Text = "|";
        lblSep2.AutoSize = true;
        lblSep2.ForeColor = Color.SteelBlue;
        lblSep2.Margin = new Padding(12, 13, 12, 0);

        lblTotalCerts.Text = "Certifications Earned: —";
        lblTotalCerts.AutoSize = true;
        lblTotalCerts.Margin = new Padding(0, 13, 0, 0);

        flpStats.Controls.Add(lblStatsTitle);
        flpStats.Controls.Add(lblTotalCandidates);
        flpStats.Controls.Add(lblSep1);
        flpStats.Controls.Add(lblTotalPlacements);
        flpStats.Controls.Add(lblSep2);
        flpStats.Controls.Add(lblTotalCerts);

        // ── Tab 1 — Scores by Career Pathway ─────────────────────────────────
        lblPathwayNote.Text =
            "Composite = mean of available overall percentile scores (CCAT, CBST, CMRA, Typing, CAST).  " +
            $"Pass threshold: {PassThreshold}th percentile.  Click any column header to sort.";
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
        dgvByPathway.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvByPathway.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        dgvByPathway.ColumnHeaderMouseClick += dgvByPathway_ColumnHeaderMouseClick;

        lblEmptyPathway.Text = "No data to display.\r\nAdd job placements to see pathway analytics.";
        lblEmptyPathway.Location = new Point(4, 30);
        lblEmptyPathway.Size = new Size(958, 438);
        lblEmptyPathway.TextAlign = ContentAlignment.MiddleCenter;
        lblEmptyPathway.ForeColor = Color.Gray;
        lblEmptyPathway.Font = new Font(Font, FontStyle.Italic);
        lblEmptyPathway.Visible = false;
        lblEmptyPathway.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

        tabPagePathway.Text = "Scores by Career Pathway";
        tabPagePathway.Padding = new Padding(4);
        tabPagePathway.Controls.Add(lblPathwayNote);
        tabPagePathway.Controls.Add(lblEmptyPathway);  // added before grid so grid renders on top
        tabPagePathway.Controls.Add(dgvByPathway);

        // ── Tab 2 — Certification Outcomes ───────────────────────────────────
        lblCertNote.Text =
            "\"Pursuing\" = currently enrolled.  " +
            "\"Passed\" and \"Failed\" reflect completed exams only.  " +
            "Click any column header to sort.";
        lblCertNote.Location = new Point(4, 6);
        lblCertNote.Size = new Size(958, 18);
        lblCertNote.ForeColor = Color.DimGray;
        lblCertNote.AutoSize = true;
        lblCertNote.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        dgvByCertification.Location = new Point(4, 30);
        dgvByCertification.Size = new Size(958, 438);
        dgvByCertification.Name = "dgvByCertification";
        dgvByCertification.ReadOnly = true;
        dgvByCertification.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvByCertification.MultiSelect = false;
        dgvByCertification.AllowUserToAddRows = false;
        dgvByCertification.AllowUserToDeleteRows = false;
        dgvByCertification.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvByCertification.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        dgvByCertification.ColumnHeaderMouseClick += dgvByCertification_ColumnHeaderMouseClick;

        lblEmptyCert.Text = "No data to display.\r\nAdd certifications to see outcomes.";
        lblEmptyCert.Location = new Point(4, 30);
        lblEmptyCert.Size = new Size(958, 438);
        lblEmptyCert.TextAlign = ContentAlignment.MiddleCenter;
        lblEmptyCert.ForeColor = Color.Gray;
        lblEmptyCert.Font = new Font(Font, FontStyle.Italic);
        lblEmptyCert.Visible = false;
        lblEmptyCert.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

        tabPageCertification.Text = "Certification Outcomes";
        tabPageCertification.Padding = new Padding(4);
        tabPageCertification.Controls.Add(lblCertNote);
        tabPageCertification.Controls.Add(lblEmptyCert);
        tabPageCertification.Controls.Add(dgvByCertification);

        // ── Tab 3 — Pass vs. Fail Analysis ───────────────────────────────────
        lblPassFailNote.Text =
            "Compares average test scores of candidates with successful placements (6+ months) vs. unsuccessful.  " +
            "Only placed candidates with at least one test score are included.  " +
            "Click any column header to sort.";
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
        dgvPassFail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvPassFail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        dgvPassFail.ColumnHeaderMouseClick += dgvPassFail_ColumnHeaderMouseClick;

        lblEmptyPassFail.Text = "No data to display.\r\nAdd job placements and assessment scores to see success correlation.";
        lblEmptyPassFail.Location = new Point(4, 30);
        lblEmptyPassFail.Size = new Size(958, 438);
        lblEmptyPassFail.TextAlign = ContentAlignment.MiddleCenter;
        lblEmptyPassFail.ForeColor = Color.Gray;
        lblEmptyPassFail.Font = new Font(Font, FontStyle.Italic);
        lblEmptyPassFail.Visible = false;
        lblEmptyPassFail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

        tabPagePassFail.Text = "Placement Success by Score";
        tabPagePassFail.Padding = new Padding(4);
        tabPagePassFail.Controls.Add(lblPassFailNote);
        tabPagePassFail.Controls.Add(lblEmptyPassFail);
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
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(996, 636);
        Controls.Add(flpStats);
        Controls.Add(tabMain);
        Controls.Add(pnlButtons);
        Text = "Analytics";
        StartPosition = FormStartPosition.CenterScreen;
        WindowState = FormWindowState.Maximized;
        MinimumSize = new Size(800, 550);
    }

    // Control declarations
    private FlowLayoutPanel flpStats;
    private Label lblStatsTitle;
    private Label lblTotalCandidates;
    private Label lblSep1;
    private Label lblTotalPlacements;
    private Label lblSep2;
    private Label lblTotalCerts;
    private TabControl tabMain;
    private TabPage tabPagePathway;
    private Label lblPathwayNote;
    private DataGridView dgvByPathway;
    private Label lblEmptyPathway;
    private TabPage tabPageCertification;
    private Label lblCertNote;
    private DataGridView dgvByCertification;
    private Label lblEmptyCert;
    private TabPage tabPagePassFail;
    private Label lblPassFailNote;
    private DataGridView dgvPassFail;
    private Label lblEmptyPassFail;
    private Panel pnlButtons;
    private Button btnExportCsv;
    private Button btnClose;
}
