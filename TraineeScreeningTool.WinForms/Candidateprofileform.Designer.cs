namespace TraineeScreeningTool.WinForms;

partial class CandidateProfileForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        // ── Header labels ────────────────────────────────────────────
        lblName = new Label();
        lblEmail = new Label();
        lblTestDate = new Label();
        lblTalentSignal = new Label();

        // Recommendation banner
        lblRecommendation = new Label();
        lblReadiness = new Label();
        lblExplanation = new Label();

        // ── Score labels ─────────────────────────────────────────────
        lblCCATRaw = new Label();
        lblCCATOverall = new Label();
        lblCCATMath = new Label();
        lblCCATVerbal = new Label();
        lblCCATSpatial = new Label();
        lblCMRA = new Label();
        lblCBSTRaw = new Label();
        lblCBSTOverall = new Label();
        lblCBSTMath = new Label();
        lblCBSTVerbal = new Label();
        lblCLIKRaw = new Label();
        lblCLIKProf = new Label();
        lblTypingWPM = new Label();
        lblTypingErrors = new Label();
        lblTypingPercentile = new Label();
        lblCASTOverall = new Label();
        lblCASTDivided = new Label();
        lblCASTFiltering = new Label();
        lblCASTReaction = new Label();
        lblCASTVigilance = new Label();
        lblWordRaw = new Label();
        lblWordProf = new Label();
        lblExcelRaw = new Label();
        lblExcelProf = new Label();
        lblCSAPRec = new Label();
        lblCSAPAchievement = new Label();
        lblCSAPAssertiveness = new Label();
        lblCSAPCooperativeness = new Label();
        lblCSAPGoal = new Label();
        lblCSAPMotivation = new Label();
        lblCSAPTeamPlayer = new Label();

        // ── Certification controls ───────────────────────────────────
        dgvCertifications = new DataGridView();
        btnAddCertification = new Button();
        lblCertSummary = new Label();

        // ── Placement controls ───────────────────────────────────────
        dgvPlacements = new DataGridView();
        btnAddPlacement = new Button();
        btnUpdatePlacement = new Button();
        btnDeletePlacement = new Button();
        lblPlacementSummary = new Label();

        // ════════════════════════════════════════════════════════════
        //  Header panel (always visible above the tabs)
        // ════════════════════════════════════════════════════════════
        var pnlHeader = new Panel
        {
            Dock = DockStyle.Top,
            Height = 145,
            BackColor = Color.FromArgb(30, 80, 140),
            Padding = new Padding(16, 10, 16, 10)
        };

        lblName.Text = "";
        lblName.Font = new Font("Segoe UI", 16, FontStyle.Bold);
        lblName.ForeColor = Color.White;
        lblName.AutoSize = true;
        lblName.Location = new Point(16, 10);

        lblEmail.Text = "";
        lblEmail.Font = new Font("Segoe UI", 9);
        lblEmail.ForeColor = Color.FromArgb(200, 220, 255);
        lblEmail.AutoSize = false;
        lblEmail.Size = new Size(430, 24);
        lblEmail.AutoEllipsis = true;
        lblEmail.Location = new Point(18, 50);

        var lblTestDateCaption = MakeCaption("Test Date:", 18, 74, Color.FromArgb(180, 210, 255));
        lblTestDate.Text = "";
        lblTestDate.Font = new Font("Segoe UI", 9);
        lblTestDate.ForeColor = Color.White;
        lblTestDate.AutoSize = true;
        lblTestDate.Location = new Point(100, 74);

        var lblTSCaption = MakeCaption("Talent Signal:", 18, 96, Color.FromArgb(180, 210, 255));
        lblTalentSignal.Text = "";
        lblTalentSignal.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        lblTalentSignal.ForeColor = Color.White;
        lblTalentSignal.AutoSize = true;
        lblTalentSignal.Location = new Point(130, 96);

        // Recommendation badge (right side of header)
        var pnlBadge = new Panel
        {
            Size = new Size(260, 110),
            Location = new Point(460, 16),
            BackColor = Color.FromArgb(20, 60, 110)
        };

        var lblRecCaption = MakeCaption("Pathway Recommendation:", 8, 8, Color.FromArgb(180, 210, 255));
        lblRecCaption.Font = new Font("Segoe UI", 8);
        lblRecommendation.Text = "";
        lblRecommendation.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        lblRecommendation.ForeColor = Color.LightGreen;
        lblRecommendation.AutoSize = true;
        lblRecommendation.Location = new Point(8, 26);

        var lblReadCaption = MakeCaption("Readiness:", 8, 54, Color.FromArgb(180, 210, 255));
        lblReadCaption.Font = new Font("Segoe UI", 8);
        lblReadiness.Text = "";
        lblReadiness.Font = new Font("Segoe UI", 9);
        lblReadiness.ForeColor = Color.White;
        lblReadiness.AutoSize = true;
        lblReadiness.Location = new Point(92, 54);

        lblExplanation.Text = "";
        lblExplanation.Font = new Font("Segoe UI", 7.5f);
        lblExplanation.ForeColor = Color.FromArgb(200, 220, 255);
        lblExplanation.Size = new Size(244, 36);
        lblExplanation.Location = new Point(8, 74);

        pnlBadge.Controls.AddRange(new Control[]
            { lblRecCaption, lblRecommendation, lblReadCaption, lblReadiness, lblExplanation });

        pnlHeader.Controls.AddRange(new Control[]
        {
            lblName, lblEmail,
            lblTestDateCaption, lblTestDate,
            lblTSCaption, lblTalentSignal,
            pnlBadge
        });

        // ════════════════════════════════════════════════════════════
        //  Tab control
        // ════════════════════════════════════════════════════════════
        var tabs = new TabControl { Dock = DockStyle.Fill };
        tabs.Font = new Font("Segoe UI", 9);

        // ── Tab 1: Scores ────────────────────────────────────────────
        var tabScores = new TabPage("📊  Scores");
        tabScores.Padding = new Padding(6);

        var scoresScroll = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
        var scoresPanel = new Panel { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };

        int lx = 10, vx = 230, rh = 30, sy = 8;

        void AddSection(string text, ref int yp)
        {
            var l = new Label
            {
                Text = text,
                Location = new Point(lx, yp),
                Size = new Size(560, 26),
                Font = new Font("Segoe UI", 9, FontStyle.Bold | FontStyle.Underline),
                ForeColor = Color.SteelBlue
            };
            scoresPanel.Controls.Add(l);
            yp += 30;
        }

        void AddRow(string header, Label val, ref int yp)
        {
            var hdr = new Label
            {
                Text = header,
                Location = new Point(lx + 10, yp),
                Size = new Size(210, 24),
                Font = new Font("Segoe UI", 9)
            };
            val.Location = new Point(vx, yp);
            val.Size = new Size(200, 24);
            val.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            scoresPanel.Controls.Add(hdr);
            scoresPanel.Controls.Add(val);
            yp += rh;
        }

        AddSection("CCAT — Cognitive Aptitude Test", ref sy);
        AddRow("Raw Score:", lblCCATRaw, ref sy);
        AddRow("Overall Percentile:", lblCCATOverall, ref sy);
        AddRow("Math & Logic Percentile:", lblCCATMath, ref sy);
        AddRow("Verbal Ability Percentile:", lblCCATVerbal, ref sy);
        AddRow("Spatial Reasoning Percentile:", lblCCATSpatial, ref sy);
        sy += 4;

        AddSection("CMRA — Mechanical Reasoning", ref sy);
        AddRow("Overall Percentile:", lblCMRA, ref sy);
        sy += 4;

        AddSection("CBST — Basic Skills Test", ref sy);
        AddRow("Raw Score:", lblCBSTRaw, ref sy);
        AddRow("Overall Percentile:", lblCBSTOverall, ref sy);
        AddRow("Math Raw Score:", lblCBSTMath, ref sy);
        AddRow("Verbal Raw Score:", lblCBSTVerbal, ref sy);
        sy += 4;

        AddSection("CLIK — Computer Literacy", ref sy);
        AddRow("Raw Score:", lblCLIKRaw, ref sy);
        AddRow("Proficiency:", lblCLIKProf, ref sy);
        sy += 4;

        AddSection("TT — Typing Test", ref sy);
        AddRow("Words Per Minute:", lblTypingWPM, ref sy);
        AddRow("Errors:", lblTypingErrors, ref sy);
        AddRow("Overall Percentile:", lblTypingPercentile, ref sy);
        sy += 4;

        AddSection("CAST — Attention Skills Test", ref sy);
        AddRow("Overall Percentile:", lblCASTOverall, ref sy);
        AddRow("Divided Attention:", lblCASTDivided, ref sy);
        AddRow("Filtering:", lblCASTFiltering, ref sy);
        AddRow("Reaction Time:", lblCASTReaction, ref sy);
        AddRow("Vigilance:", lblCASTVigilance, ref sy);
        sy += 4;

        AddSection("Microsoft Office Skills", ref sy);
        AddRow("Word 365 Raw Score:", lblWordRaw, ref sy);
        AddRow("Word 365 Proficiency:", lblWordProf, ref sy);
        AddRow("Excel 365 Raw Score:", lblExcelRaw, ref sy);
        AddRow("Excel 365 Proficiency:", lblExcelProf, ref sy);
        sy += 4;

        AddSection("CSAP — Customer Service Aptitude Profile", ref sy);
        AddRow("Overall Recommendation:", lblCSAPRec, ref sy);
        AddRow("Achievement:", lblCSAPAchievement, ref sy);
        AddRow("Assertiveness:", lblCSAPAssertiveness, ref sy);
        AddRow("Cooperativeness:", lblCSAPCooperativeness, ref sy);
        AddRow("Goal Orientation:", lblCSAPGoal, ref sy);
        AddRow("Motivation:", lblCSAPMotivation, ref sy);
        AddRow("Team Player:", lblCSAPTeamPlayer, ref sy);

        scoresScroll.Controls.Add(scoresPanel);
        tabScores.Controls.Add(scoresScroll);

        // ── Tab 2: Certifications ─────────────────────────────────────
        var tabCerts = new TabPage("🎓  Certifications");
        tabCerts.Padding = new Padding(8);

        lblCertSummary.Text = "";
        lblCertSummary.Font = new Font("Segoe UI", 9);
        lblCertSummary.ForeColor = Color.DimGray;
        lblCertSummary.AutoSize = true;
        lblCertSummary.Location = new Point(8, 10);

        btnAddCertification.Text = "+ Add Certification";
        btnAddCertification.Size = new Size(150, 28);
        btnAddCertification.BackColor = Color.SteelBlue;
        btnAddCertification.ForeColor = Color.White;
        btnAddCertification.FlatStyle = FlatStyle.Flat;
        btnAddCertification.Font = new Font("Segoe UI", 8.5f);
        btnAddCertification.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAddCertification.Click += btnAddCertification_Click;

        var lblCertHint = new Label
        {
            Text = "Double-click a row to edit or delete.",
            Font = new Font("Segoe UI", 8),
            ForeColor = Color.Gray,
            AutoSize = true,
            Location = new Point(8, 36)
        };

        dgvCertifications.ReadOnly = true;
        dgvCertifications.AllowUserToAddRows = false;
        dgvCertifications.AllowUserToDeleteRows = false;
        dgvCertifications.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCertifications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        dgvCertifications.RowHeadersVisible = false;
        dgvCertifications.BorderStyle = BorderStyle.FixedSingle;
        dgvCertifications.BackgroundColor = SystemColors.Window;
        dgvCertifications.Anchor =
            AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvCertifications.CellDoubleClick += dgvCertifications_CellDoubleClick;

        tabCerts.Controls.AddRange(new Control[]
            { lblCertSummary, btnAddCertification, lblCertHint, dgvCertifications });

        // ── Tab 3: Placements ─────────────────────────────────────────
        var tabPlacements = new TabPage("💼  Job Placements");
        tabPlacements.Padding = new Padding(8);

        lblPlacementSummary.Text = "";
        lblPlacementSummary.Font = new Font("Segoe UI", 9);
        lblPlacementSummary.ForeColor = Color.DimGray;
        lblPlacementSummary.AutoSize = true;
        lblPlacementSummary.Location = new Point(8, 10);

        btnAddPlacement.Text = "+ Add Placement";
        btnAddPlacement.Size = new Size(130, 28);
        btnAddPlacement.BackColor = Color.SteelBlue;
        btnAddPlacement.ForeColor = Color.White;
        btnAddPlacement.FlatStyle = FlatStyle.Flat;
        btnAddPlacement.Font = new Font("Segoe UI", 8.5f);
        btnAddPlacement.Click += btnAddPlacement_Click;

        btnUpdatePlacement.Text = "Update";
        btnUpdatePlacement.Size = new Size(90, 28);
        btnUpdatePlacement.FlatStyle = FlatStyle.Flat;
        btnUpdatePlacement.Font = new Font("Segoe UI", 8.5f);
        btnUpdatePlacement.Click += btnUpdatePlacement_Click;

        btnDeletePlacement.Text = "Delete";
        btnDeletePlacement.Size = new Size(90, 28);
        btnDeletePlacement.BackColor = Color.IndianRed;
        btnDeletePlacement.ForeColor = Color.White;
        btnDeletePlacement.FlatStyle = FlatStyle.Flat;
        btnDeletePlacement.Font = new Font("Segoe UI", 8.5f);
        btnDeletePlacement.Click += btnDeletePlacement_Click;

        var lblPlacHint = new Label
        {
            Text = "Double-click a row to update it.",
            Font = new Font("Segoe UI", 8),
            ForeColor = Color.Gray,
            AutoSize = true,
            Location = new Point(8, 36)
        };

        var legendOrange = new Panel { Size = new Size(14, 14), BackColor = Color.FromArgb(255, 220, 150) };
        var legendYellow = new Panel { Size = new Size(14, 14), BackColor = Color.FromArgb(255, 255, 180) };
        var lblLegendO = new Label { Text = "Past 6 months", AutoSize = true, Font = new Font("Segoe UI", 8) };
        var lblLegendY = new Label { Text = "Approaching 6 months", AutoSize = true, Font = new Font("Segoe UI", 8) };

        dgvPlacements.ReadOnly = true;
        dgvPlacements.AllowUserToAddRows = false;
        dgvPlacements.AllowUserToDeleteRows = false;
        dgvPlacements.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPlacements.RowHeadersVisible = false;
        dgvPlacements.BorderStyle = BorderStyle.FixedSingle;
        dgvPlacements.BackgroundColor = SystemColors.Window;
        dgvPlacements.Anchor =
            AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvPlacements.CellDoubleClick += dgvPlacements_CellDoubleClick;

        tabPlacements.Controls.AddRange(new Control[]
        {
            lblPlacementSummary, btnAddPlacement, btnUpdatePlacement, btnDeletePlacement,
            lblPlacHint, legendOrange, lblLegendO, legendYellow, lblLegendY,
            dgvPlacements
        });

        tabs.TabPages.AddRange(new[] { tabScores, tabCerts, tabPlacements });

        // ════════════════════════════════════════════════════════════
        //  Layout on resize
        // ════════════════════════════════════════════════════════════
        tabs.SelectedIndexChanged += (s, e) => LayoutTab(tabs.SelectedTab);
        Resize += (s, e) => LayoutTab(tabs.SelectedTab);

        void LayoutTab(TabPage? tab)
        {
            if (tab == null) return;
            int w = tab.ClientSize.Width - 16;
            int h = tab.ClientSize.Height - 16;

            if (tab == tabScores)
            {
                scoresScroll.Size = new Size(w, h);
                scoresScroll.Location = new Point(8, 8);
                scoresPanel.Width = w - 20;
            }
            else if (tab == tabCerts)
            {
                btnAddCertification.Location = new Point(w - 158, 6);
                dgvCertifications.Location = new Point(8, 58);
                dgvCertifications.Size = new Size(w, h - 60);
            }
            else if (tab == tabPlacements)
            {
                btnAddPlacement.Location = new Point(w - 320, 6);
                btnUpdatePlacement.Location = new Point(w - 185, 6);
                btnDeletePlacement.Location = new Point(w - 92, 6);

                legendOrange.Location = new Point(8, 56);
                lblLegendO.Location = new Point(26, 54);
                legendYellow.Location = new Point(160, 56);
                lblLegendY.Location = new Point(178, 54);

                dgvPlacements.Location = new Point(8, 76);
                dgvPlacements.Size = new Size(w, h - 78);
            }
        }

        // First paint layout
        Load += (s, e) =>
        {
            foreach (TabPage tp in tabs.TabPages)
                LayoutTab(tp);
        };

        // ════════════════════════════════════════════════════════════
        //  Form setup
        // ════════════════════════════════════════════════════════════
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(760, 680);
        MinimumSize = new Size(680, 580);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.Sizable;
        Text = "Candidate Profile";

        Controls.Add(tabs);
        Controls.Add(pnlHeader);
    }

    private static Label MakeCaption(string text, int x, int y, Color color)
        => new Label
        {
            Text = text,
            Location = new Point(x, y),
            AutoSize = true,
            Font = new Font("Segoe UI", 8.5f),
            ForeColor = color
        };

    private Label lblName, lblEmail, lblTestDate, lblTalentSignal;
    private Label lblRecommendation, lblReadiness, lblExplanation;
    private Label lblCCATRaw, lblCCATOverall, lblCCATMath, lblCCATVerbal, lblCCATSpatial;
    private Label lblCMRA;
    private Label lblCBSTRaw, lblCBSTOverall, lblCBSTMath, lblCBSTVerbal;
    private Label lblCLIKRaw, lblCLIKProf;
    private Label lblTypingWPM, lblTypingErrors, lblTypingPercentile;
    private Label lblCASTOverall, lblCASTDivided, lblCASTFiltering, lblCASTReaction, lblCASTVigilance;
    private Label lblWordRaw, lblWordProf, lblExcelRaw, lblExcelProf;
    private Label lblCSAPRec, lblCSAPAchievement, lblCSAPAssertiveness, lblCSAPCooperativeness;
    private Label lblCSAPGoal, lblCSAPMotivation, lblCSAPTeamPlayer;
    private DataGridView dgvCertifications;
    private DataGridView dgvPlacements;
    private Button btnAddCertification;
    private Button btnAddPlacement, btnUpdatePlacement, btnDeletePlacement;
    private Label lblCertSummary, lblPlacementSummary;
}