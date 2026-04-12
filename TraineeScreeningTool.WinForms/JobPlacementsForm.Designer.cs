namespace TraineeScreeningTool.WinForms;

partial class JobPlacementsForm
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
        pnlLegend = new Panel();
        lblLegendTitle = new Label();
        lblLegendOrange = new Label();
        lblLegendYellow = new Label();
        pnlFilters = new Panel();
        Label lblFilterPathway = new Label();
        cmbFilter = new ComboBox();
        chkNeedsReview = new CheckBox();
        lblSummary = new Label();
        dgvPlacements = new DataGridView();
        pnlButtons = new Panel();
        btnAdd = new Button();
        btnUpdate = new Button();
        btnDelete = new Button();
        btnClose = new Button();

        // --- Legend panel ---
        pnlLegend.Location = new Point(12, 12);
        pnlLegend.Size = new Size(500, 28);
        pnlLegend.BorderStyle = BorderStyle.FixedSingle;
        pnlLegend.Anchor = AnchorStyles.Top | AnchorStyles.Left;

        lblLegendTitle.Text = "Key:";
        lblLegendTitle.Location = new Point(4, 5);
        lblLegendTitle.Size = new Size(30, 18);
        lblLegendTitle.Font = new Font(lblLegendTitle.Font, FontStyle.Bold);

        var swatchOrange = new Panel
        {
            BackColor = Color.FromArgb(255, 220, 150),
            Location = new Point(38, 6),
            Size = new Size(16, 16),
            BorderStyle = BorderStyle.FixedSingle
        };

        lblLegendOrange.Text = "Past 6 months — needs review";
        lblLegendOrange.Location = new Point(58, 5);
        lblLegendOrange.Size = new Size(180, 18);

        var swatchYellow = new Panel
        {
            BackColor = Color.FromArgb(255, 255, 180),
            Location = new Point(248, 6),
            Size = new Size(16, 16),
            BorderStyle = BorderStyle.FixedSingle
        };

        lblLegendYellow.Text = "Approaching 6 months";
        lblLegendYellow.Location = new Point(268, 5);
        lblLegendYellow.Size = new Size(160, 18);

        pnlLegend.Controls.Add(lblLegendTitle);
        pnlLegend.Controls.Add(swatchOrange);
        pnlLegend.Controls.Add(lblLegendOrange);
        pnlLegend.Controls.Add(swatchYellow);
        pnlLegend.Controls.Add(lblLegendYellow);

        // --- Filters panel ---
        pnlFilters.Location = new Point(12, 48);
        pnlFilters.Size = new Size(760, 32);
        pnlFilters.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        lblFilterPathway.Text = "Filter by Pathway:";
        lblFilterPathway.Location = new Point(0, 6);
        lblFilterPathway.Size = new Size(120, 20);

        cmbFilter.Location = new Point(124, 3);
        cmbFilter.Size = new Size(220, 25);
        cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbFilter.SelectedIndexChanged += cmbFilter_SelectedIndexChanged;

        chkNeedsReview.Text = "Show only placements needing review";
        chkNeedsReview.Location = new Point(360, 5);
        chkNeedsReview.Size = new Size(260, 22);
        chkNeedsReview.CheckedChanged += chkNeedsReview_CheckedChanged;

        pnlFilters.Controls.Add(lblFilterPathway);
        pnlFilters.Controls.Add(cmbFilter);
        pnlFilters.Controls.Add(chkNeedsReview);

        // --- Summary label ---
        lblSummary.Location = new Point(12, 86);
        lblSummary.Size = new Size(500, 20);
        lblSummary.Text = "";
        lblSummary.ForeColor = Color.DimGray;

        // --- DataGridView ---
        dgvPlacements.Location = new Point(12, 110);
        dgvPlacements.Size = new Size(960, 380);
        dgvPlacements.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        dgvPlacements.Name = "dgvPlacements";
        dgvPlacements.ReadOnly = true;
        dgvPlacements.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPlacements.MultiSelect = false;
        dgvPlacements.AllowUserToAddRows = false;
        dgvPlacements.AllowUserToDeleteRows = false;
        dgvPlacements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        dgvPlacements.CellDoubleClick += dgvPlacements_CellDoubleClick;

        // --- Buttons panel ---
        pnlButtons.Location = new Point(12, 500);
        pnlButtons.Size = new Size(960, 45);
        pnlButtons.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

        btnAdd.Location = new Point(0, 5);
        btnAdd.Size = new Size(150, 35);
        btnAdd.Text = "Add New Placement";
        btnAdd.BackColor = Color.SteelBlue;
        btnAdd.ForeColor = Color.White;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.Click += btnAdd_Click;

        btnUpdate.Location = new Point(160, 5);
        btnUpdate.Size = new Size(150, 35);
        btnUpdate.Text = "Update Selected";
        btnUpdate.Click += btnUpdate_Click;

        btnDelete.Location = new Point(320, 5);
        btnDelete.Size = new Size(120, 35);
        btnDelete.Text = "Delete Selected";
        btnDelete.BackColor = Color.IndianRed;
        btnDelete.ForeColor = Color.White;
        btnDelete.FlatStyle = FlatStyle.Flat;
        btnDelete.Click += btnDelete_Click;

        btnClose.Location = new Point(460, 5);
        btnClose.Size = new Size(100, 35);
        btnClose.Text = "Close";
        btnClose.Click += (s, e) => this.Close();

        pnlButtons.Controls.Add(btnAdd);
        pnlButtons.Controls.Add(btnUpdate);
        pnlButtons.Controls.Add(btnDelete);
        pnlButtons.Controls.Add(btnClose);

        // Form setup
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(984, 560);
        Controls.Add(pnlLegend);
        Controls.Add(pnlFilters);
        Controls.Add(lblSummary);
        Controls.Add(dgvPlacements);
        Controls.Add(pnlButtons);
        Text = "Job Placements";
        StartPosition = FormStartPosition.CenterParent;
        MinimumSize = new Size(800, 560);
    }

    // Control declarations
    private Panel pnlLegend;
    private Label lblLegendTitle;
    private Label lblLegendOrange;
    private Label lblLegendYellow;
    private Panel pnlFilters;
    private ComboBox cmbFilter;
    private CheckBox chkNeedsReview;
    private Label lblSummary;
    private DataGridView dgvPlacements;
    private Panel pnlButtons;
    private Button btnAdd;
    private Button btnUpdate;
    private Button btnDelete;
    private Button btnClose;
}
