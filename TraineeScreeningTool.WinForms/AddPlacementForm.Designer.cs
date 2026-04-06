namespace TraineeScreeningTool.WinForms;

partial class AddPlacementForm
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
        Label lblCandidate = new Label();
        Label lblPathway = new Label();
        Label lblDate = new Label();
        cmbCandidate = new ComboBox();
        cmbCareerPathway = new ComboBox();
        dtpPlacementDate = new DateTimePicker();
        btnSave = new Button();
        btnCancel = new Button();

        // Candidate label
        lblCandidate.Text = "Candidate:";
        lblCandidate.Location = new Point(12, 20);
        lblCandidate.Size = new Size(110, 20);

        // Candidate dropdown
        cmbCandidate.Location = new Point(130, 17);
        cmbCandidate.Size = new Size(310, 25);
        cmbCandidate.Name = "cmbCandidate";
        cmbCandidate.DropDownStyle = ComboBoxStyle.DropDownList;

        // Career Pathway label
        lblPathway.Text = "Career Pathway:";
        lblPathway.Location = new Point(12, 60);
        lblPathway.Size = new Size(110, 20);

        // Career Pathway dropdown
        cmbCareerPathway.Location = new Point(130, 57);
        cmbCareerPathway.Size = new Size(310, 25);
        cmbCareerPathway.Name = "cmbCareerPathway";
        cmbCareerPathway.DropDownStyle = ComboBoxStyle.DropDownList;

        // Placement Date label
        lblDate.Text = "Placement Date:";
        lblDate.Location = new Point(12, 100);
        lblDate.Size = new Size(110, 20);

        // Placement Date picker
        dtpPlacementDate.Location = new Point(130, 97);
        dtpPlacementDate.Size = new Size(200, 25);
        dtpPlacementDate.Name = "dtpPlacementDate";
        dtpPlacementDate.Format = DateTimePickerFormat.Short;

        // Save button
        btnSave.Location = new Point(130, 140);
        btnSave.Size = new Size(120, 35);
        btnSave.Text = "Save Placement";
        btnSave.BackColor = Color.SteelBlue;
        btnSave.ForeColor = Color.White;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Click += btnSave_Click;

        // Cancel button
        btnCancel.Location = new Point(260, 140);
        btnCancel.Size = new Size(90, 35);
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;

        ClientSize = new Size(470, 200);
        Controls.Add(lblCandidate);
        Controls.Add(cmbCandidate);
        Controls.Add(lblPathway);
        Controls.Add(cmbCareerPathway);
        Controls.Add(lblDate);
        Controls.Add(dtpPlacementDate);
        Controls.Add(btnSave);
        Controls.Add(btnCancel);
        Text = "Add Job Placement";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
    }

    private ComboBox cmbCandidate;
    private ComboBox cmbCareerPathway;
    private DateTimePicker dtpPlacementDate;
    private Button btnSave;
    private Button btnCancel;
}
