namespace TraineeScreeningTool.WinForms;

partial class EditCertificationForm
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
        Label lblHeader = new Label();
        Label lblCandidateLbl = new Label();
        Label lblCertLbl = new Label();
        Label lblStatusLbl = new Label();
        Label lblResultLbl = new Label();
        Label lblResultHint = new Label();
        Label lblDateLbl = new Label();
        Label lblNotesLbl = new Label();

        lblCandidateId = new Label();
        txtCertName = new TextBox();
        cmbStatus = new ComboBox();
        cmbResult = new ComboBox();
        chkHasDate = new CheckBox();
        dtpDate = new DateTimePicker();
        txtNotes = new TextBox();
        btnSave = new Button();
        btnDelete = new Button();
        btnCancel = new Button();

        // AutoScaleMode.None — exact pixel values, no DPI surprises.
        // vx=140, w=260, right edge=400 inside a 490px form = 90px right margin.
        int lx = 12, vx = 140, w = 260, y = 14, rh = 34;

        lblHeader.Text = "Edit Certification";
        lblHeader.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        lblHeader.ForeColor = Color.SteelBlue;
        lblHeader.Location = new Point(lx, y);
        lblHeader.Size = new Size(450, 28);
        y += 36;

        lblCandidateLbl.Text = "Candidate:";
        lblCandidateLbl.Location = new Point(lx, y + 4);
        lblCandidateLbl.Size = new Size(122, 20);
        lblCandidateLbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        lblCandidateId.Location = new Point(vx, y + 4);
        lblCandidateId.Size = new Size(w, 20);
        lblCandidateId.ForeColor = Color.DimGray;
        y += rh;

        lblCertLbl.Text = "Certification:";
        lblCertLbl.Location = new Point(lx, y + 4);
        lblCertLbl.Size = new Size(122, 20);
        lblCertLbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        txtCertName.Location = new Point(vx, y);
        txtCertName.Size = new Size(w, 25);
        txtCertName.Name = "txtCertName";
        y += rh;

        lblStatusLbl.Text = "Status:";
        lblStatusLbl.Location = new Point(lx, y + 4);
        lblStatusLbl.Size = new Size(122, 20);
        lblStatusLbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        cmbStatus.Location = new Point(vx, y);
        cmbStatus.Size = new Size(160, 25);
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbStatus.Name = "cmbStatus";
        y += rh;

        lblResultLbl.Text = "Result:";
        lblResultLbl.Location = new Point(lx, y + 4);
        lblResultLbl.Size = new Size(122, 20);
        lblResultLbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        cmbResult.Location = new Point(vx, y);
        cmbResult.Size = new Size(160, 25);
        cmbResult.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbResult.Name = "cmbResult";
        y += 28;

        lblResultHint.Text = "(only applies when Status is Completed)";
        lblResultHint.Location = new Point(vx, y);
        lblResultHint.Size = new Size(w, 16);
        lblResultHint.ForeColor = Color.Gray;
        lblResultHint.Font = new Font("Segoe UI", 7.5f);
        y += 22;

        lblDateLbl.Text = "Date:";
        lblDateLbl.Location = new Point(lx, y + 4);
        lblDateLbl.Size = new Size(122, 20);
        lblDateLbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        chkHasDate.Text = "Set date";
        chkHasDate.Location = new Point(vx, y + 2);
        chkHasDate.Size = new Size(82, 22);
        chkHasDate.Name = "chkHasDate";
        dtpDate.Location = new Point(vx + 88, y);
        dtpDate.Size = new Size(160, 25);
        dtpDate.Name = "dtpDate";
        dtpDate.Format = DateTimePickerFormat.Short;
        y += rh;

        lblNotesLbl.Text = "Notes:";
        lblNotesLbl.Location = new Point(lx, y + 4);
        lblNotesLbl.Size = new Size(122, 20);
        lblNotesLbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        txtNotes.Location = new Point(vx, y);
        txtNotes.Size = new Size(w, 44);
        txtNotes.Multiline = true;
        txtNotes.Name = "txtNotes";
        y += 52;

        btnSave.Location = new Point(vx, y);
        btnSave.Size = new Size(110, 30);
        btnSave.Text = "Save Changes";
        btnSave.BackColor = Color.SteelBlue;
        btnSave.ForeColor = Color.White;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Click += btnSave_Click;

        btnDelete.Location = new Point(vx + 118, y);
        btnDelete.Size = new Size(75, 30);
        btnDelete.Text = "Delete";
        btnDelete.BackColor = Color.IndianRed;
        btnDelete.ForeColor = Color.White;
        btnDelete.FlatStyle = FlatStyle.Flat;
        btnDelete.Click += btnDelete_Click;

        btnCancel.Location = new Point(vx + 202, y);
        btnCancel.Size = new Size(75, 30);
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;

        Controls.AddRange(new Control[]
        {
            lblHeader,
            lblCandidateLbl, lblCandidateId,
            lblCertLbl,      txtCertName,
            lblStatusLbl,    cmbStatus,
            lblResultLbl,    cmbResult, lblResultHint,
            lblDateLbl,      chkHasDate, dtpDate,
            lblNotesLbl,     txtNotes,
            btnSave, btnDelete, btnCancel
        });

        AutoScaleMode = AutoScaleMode.None;
        ClientSize = new Size(490, y + 46);
        Text = "Edit Certification";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
    }

    private Label lblCandidateId;
    private TextBox txtCertName;
    private ComboBox cmbStatus;
    private ComboBox cmbResult;
    private CheckBox chkHasDate;
    private DateTimePicker dtpDate;
    private TextBox txtNotes;
    private Button btnSave;
    private Button btnDelete;
    private Button btnCancel;
}
