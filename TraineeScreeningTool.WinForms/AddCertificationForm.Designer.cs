namespace TraineeScreeningTool.WinForms;

partial class AddCertificationForm
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
        Label lblHeader       = new Label();
        Label lblCandidateLbl = new Label();
        Label lblPathwayLbl   = new Label();
        Label lblCertLbl      = new Label();
        Label lblStatusLbl    = new Label();
        Label lblResultLbl    = new Label();
        Label lblDateLbl      = new Label();
        Label lblNotesLbl     = new Label();

        lblCandidateName = new Label();
        cmbPathway  = new ComboBox();
        cmbCertName = new ComboBox();
        cmbStatus   = new ComboBox();
        cmbResult   = new ComboBox();
        chkHasDate  = new CheckBox();
        dtpDate     = new DateTimePicker();
        txtNotes    = new TextBox();
        btnSave     = new Button();
        btnCancel   = new Button();

        int lx = 14, vx = 160, w = 330, y = 14, rh = 36;

        lblHeader.Text = "Add Certification";
        lblHeader.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        lblHeader.ForeColor = Color.SteelBlue;
        lblHeader.Location = new Point(lx, y);
        lblHeader.Size = new Size(480, 28);
        y += 36;

        lblCandidateLbl.Text = "Candidate:";
        lblCandidateLbl.Location = new Point(lx, y + 3);
        lblCandidateLbl.Size = new Size(140, 20);
        lblCandidateLbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        lblCandidateName.Location = new Point(vx, y + 3);
        lblCandidateName.Size = new Size(w, 20);
        y += rh;

        lblPathwayLbl.Text = "Career Pathway:";
        lblPathwayLbl.Location = new Point(lx, y + 3);
        lblPathwayLbl.Size = new Size(140, 20);
        lblPathwayLbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        cmbPathway.Location = new Point(vx, y);
        cmbPathway.Size = new Size(w, 25);
        cmbPathway.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbPathway.Name = "cmbPathway";
        cmbPathway.Items.Add("(All / General)");
        foreach (var p in Models.CareerPathways.All)
            cmbPathway.Items.Add(p);
        cmbPathway.SelectedIndex = 0;
        cmbPathway.SelectedIndexChanged += cmbPathway_SelectedIndexChanged;
        y += rh;

        lblCertLbl.Text = "Certification:";
        lblCertLbl.Location = new Point(lx, y + 3);
        lblCertLbl.Size = new Size(140, 20);
        lblCertLbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        cmbCertName.Location = new Point(vx, y);
        cmbCertName.Size = new Size(w, 25);
        cmbCertName.DropDownStyle = ComboBoxStyle.DropDown;
        cmbCertName.Name = "cmbCertName";
        y += rh;

        lblStatusLbl.Text = "Status:";
        lblStatusLbl.Location = new Point(lx, y + 3);
        lblStatusLbl.Size = new Size(140, 20);
        lblStatusLbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        cmbStatus.Location = new Point(vx, y);
        cmbStatus.Size = new Size(160, 25);
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbStatus.Name = "cmbStatus";
        y += rh;

        lblResultLbl.Text = "Result:";
        lblResultLbl.Location = new Point(lx, y + 3);
        lblResultLbl.Size = new Size(140, 20);
        lblResultLbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        cmbResult.Location = new Point(vx, y);
        cmbResult.Size = new Size(160, 25);
        cmbResult.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbResult.Name = "cmbResult";
        var lblResultHint = new Label();
        lblResultHint.Text = "(only if Completed)";
        lblResultHint.Location = new Point(vx + 168, y + 5);
        lblResultHint.Size = new Size(160, 18);
        lblResultHint.ForeColor = Color.Gray;
        lblResultHint.Font = new Font("Segoe UI", 8);
        y += rh;

        lblDateLbl.Text = "Date:";
        lblDateLbl.Location = new Point(lx, y + 3);
        lblDateLbl.Size = new Size(140, 20);
        lblDateLbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        chkHasDate.Text = "Set date";
        chkHasDate.Location = new Point(vx, y + 2);
        chkHasDate.Size = new Size(80, 22);
        chkHasDate.Name = "chkHasDate";
        dtpDate.Location = new Point(vx + 86, y);
        dtpDate.Size = new Size(160, 25);
        dtpDate.Name = "dtpDate";
        dtpDate.Format = DateTimePickerFormat.Short;
        y += rh;

        lblNotesLbl.Text = "Notes:";
        lblNotesLbl.Location = new Point(lx, y + 3);
        lblNotesLbl.Size = new Size(140, 20);
        lblNotesLbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        txtNotes.Location = new Point(vx, y);
        txtNotes.Size = new Size(w, 60);
        txtNotes.Multiline = true;
        txtNotes.Name = "txtNotes";
        y += 70;

        btnSave.Location = new Point(vx, y);
        btnSave.Size = new Size(130, 34);
        btnSave.Text = "Save Certification";
        btnSave.BackColor = Color.SteelBlue;
        btnSave.ForeColor = Color.White;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Click += btnSave_Click;

        btnCancel.Location = new Point(vx + 140, y);
        btnCancel.Size = new Size(80, 34);
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;

        Controls.AddRange(new Control[]
        {
            lblHeader,
            lblCandidateLbl, lblCandidateName,
            lblPathwayLbl,   cmbPathway,
            lblCertLbl,      cmbCertName,
            lblStatusLbl,    cmbStatus,
            lblResultLbl,    cmbResult, lblResultHint,
            lblDateLbl,      chkHasDate, dtpDate,
            lblNotesLbl,     txtNotes,
            btnSave,         btnCancel
        });

        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(520, y + 50);
        Text = "Add Certification";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
    }

    private Label    lblCandidateName;
    private ComboBox cmbPathway;
    private ComboBox cmbCertName;
    private ComboBox cmbStatus;
    private ComboBox cmbResult;
    private CheckBox chkHasDate;
    private DateTimePicker dtpDate;
    private TextBox  txtNotes;
    private Button   btnSave;
    private Button   btnCancel;
}