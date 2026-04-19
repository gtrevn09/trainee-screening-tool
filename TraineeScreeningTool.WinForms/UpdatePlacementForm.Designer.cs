namespace TraineeScreeningTool.WinForms;

partial class UpdatePlacementForm
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
        Label lblCandidateCaption = new Label();
        Label lblPathwayCaption = new Label();
        Label lblStartCaption = new Label();
        Label lblMonthsCaption = new Label();
        Label lblExitCaption = new Label();
        Label lblSuccessCaption = new Label();

        lblCandidateName = new Label();
        lblPathway = new Label();
        lblStartDate = new Label();
        lblMonths = new Label();
        chkHasExitDate = new CheckBox();
        dtpExitDate = new DateTimePicker();
        chkIsSuccessful = new CheckBox();
        lblSuggestion = new Label();
        btnSave = new Button();
        btnCancel = new Button();

        int col1 = 12, col2 = 150, rowH = 35;
        int row = 15;

        // Candidate row
        lblCandidateCaption.Text = "Candidate:";
        lblCandidateCaption.Location = new Point(col1, row);
        lblCandidateCaption.Size = new Size(130, 20);
        lblCandidateCaption.Font = new Font(lblCandidateCaption.Font, FontStyle.Bold);

        lblCandidateName.Text = "";
        lblCandidateName.Location = new Point(col2, row);
        lblCandidateName.Size = new Size(280, 20);
        row += rowH;

        // Pathway row
        lblPathwayCaption.Text = "Career Pathway:";
        lblPathwayCaption.Location = new Point(col1, row);
        lblPathwayCaption.Size = new Size(130, 20);
        lblPathwayCaption.Font = new Font(lblPathwayCaption.Font, FontStyle.Bold);

        lblPathway.Text = "";
        lblPathway.Location = new Point(col2, row);
        lblPathway.Size = new Size(280, 24);
        row += rowH;

        // Start date row
        lblStartCaption.Text = "Placement Date:";
        lblStartCaption.Location = new Point(col1, row);
        lblStartCaption.Size = new Size(130, 20);
        lblStartCaption.Font = new Font(lblStartCaption.Font, FontStyle.Bold);

        lblStartDate.Text = "";
        lblStartDate.Location = new Point(col2, row);
        lblStartDate.Size = new Size(280, 24);
        row += rowH;

        // Months placed row
        lblMonthsCaption.Text = "Time in Role:";
        lblMonthsCaption.Location = new Point(col1, row);
        lblMonthsCaption.Size = new Size(130, 20);
        lblMonthsCaption.Font = new Font(lblMonthsCaption.Font, FontStyle.Bold);

        lblMonths.Text = "";
        lblMonths.Location = new Point(col2, row);
        lblMonths.Size = new Size(280, 20);
        row += rowH;

        // Separator line visual spacing
        row += 5;

        // Exit date checkbox + picker
        lblExitCaption.Text = "Exit Date:";
        lblExitCaption.Location = new Point(col1, row);
        lblExitCaption.Size = new Size(130, 20);
        lblExitCaption.Font = new Font(lblExitCaption.Font, FontStyle.Bold);

        chkHasExitDate.Text = "Record exit date";
        chkHasExitDate.Location = new Point(col2, row - 2);
        chkHasExitDate.Size = new Size(140, 26);
        chkHasExitDate.CheckedChanged += chkHasExitDate_CheckedChanged;

        dtpExitDate.Location = new Point(col2 + 150, row - 2);
        dtpExitDate.Size = new Size(140, 25);
        dtpExitDate.Name = "dtpExitDate";
        dtpExitDate.Format = DateTimePickerFormat.Short;
        dtpExitDate.Enabled = false;
        row += rowH;

        // Successful checkbox
        lblSuccessCaption.Text = "Outcome:";
        lblSuccessCaption.Location = new Point(col1, row);
        lblSuccessCaption.Size = new Size(130, 20);
        lblSuccessCaption.Font = new Font(lblSuccessCaption.Font, FontStyle.Bold);

        chkIsSuccessful.Text = "Mark as Successful (stayed 6+ months)";
        chkIsSuccessful.Location = new Point(col2, row - 2);
        chkIsSuccessful.Size = new Size(290, 26);
        row += rowH;

        // Suggestion label
        lblSuggestion.Text = "";
        lblSuggestion.Location = new Point(col1, row);
        lblSuggestion.Size = new Size(440, 20);
        lblSuggestion.ForeColor = Color.DarkOrange;
        lblSuggestion.Visible = false;
        row += 30;

        // Save button
        btnSave.Location = new Point(col2, row);
        btnSave.Size = new Size(140, 35);
        btnSave.Text = "Save Changes";
        btnSave.BackColor = Color.SteelBlue;
        btnSave.ForeColor = Color.White;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Click += btnSave_Click;

        // Cancel button
        btnCancel.Location = new Point(col2 + 150, row);
        btnCancel.Size = new Size(100, 35);
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;

        row += 50;

        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(480, row);
        Controls.Add(lblCandidateCaption);
        Controls.Add(lblCandidateName);
        Controls.Add(lblPathwayCaption);
        Controls.Add(lblPathway);
        Controls.Add(lblStartCaption);
        Controls.Add(lblStartDate);
        Controls.Add(lblMonthsCaption);
        Controls.Add(lblMonths);
        Controls.Add(lblExitCaption);
        Controls.Add(chkHasExitDate);
        Controls.Add(dtpExitDate);
        Controls.Add(lblSuccessCaption);
        Controls.Add(chkIsSuccessful);
        Controls.Add(lblSuggestion);
        Controls.Add(btnSave);
        Controls.Add(btnCancel);

        Text = "Update Job Placement";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
    }

    private Label lblCandidateName;
    private Label lblPathway;
    private Label lblStartDate;
    private Label lblMonths;
    private CheckBox chkHasExitDate;
    private DateTimePicker dtpExitDate;
    private CheckBox chkIsSuccessful;
    private Label lblSuggestion;
    private Button btnSave;
    private Button btnCancel;
}
