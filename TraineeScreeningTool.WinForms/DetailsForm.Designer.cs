namespace TraineeScreeningTool.WinForms;

// This file handles the visual layout of the Details form
partial class DetailsForm
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
        // Initialize all value labels
        lblName = new Label();
        lblEmail = new Label();
        lblCCATRaw = new Label();
        lblCCATMath = new Label();
        lblCCATVerbal = new Label();
        lblCCATSpatial = new Label();
        lblCBSTRaw = new Label();
        lblCBSTMath = new Label();
        lblCBSTVerbal = new Label();
        lblCMRA = new Label();
        lblRecommendation = new Label();
        lblReadiness = new Label();
        lblExplanation = new Label();

        int labelX = 12;   // X position for header labels
        int valueX = 200;  // X position for value labels
        int rowH = 30;     // Height between rows
        int startY = 15;   // Starting Y position

        // Add all rows
        AddRow("Name:", lblName, labelX, valueX, startY);
        AddRow("Email:", lblEmail, labelX, valueX, startY + rowH);
        AddRow("CCAT Raw Score:", lblCCATRaw, labelX, valueX, startY + rowH * 2);
        AddRow("CCAT Math %:", lblCCATMath, labelX, valueX, startY + rowH * 3);
        AddRow("CCAT Verbal %:", lblCCATVerbal, labelX, valueX, startY + rowH * 4);
        AddRow("CCAT Spatial %:", lblCCATSpatial, labelX, valueX, startY + rowH * 5);
        AddRow("CBST Raw Score:", lblCBSTRaw, labelX, valueX, startY + rowH * 6);
        AddRow("CBST Math:", lblCBSTMath, labelX, valueX, startY + rowH * 7);
        AddRow("CBST Verbal:", lblCBSTVerbal, labelX, valueX, startY + rowH * 8);
        AddRow("CMRA Percentile:", lblCMRA, labelX, valueX, startY + rowH * 9);
        AddRow("Recommendation:", lblRecommendation, labelX, valueX, startY + rowH * 10);
        AddRow("Readiness Rating:", lblReadiness, labelX, valueX, startY + rowH * 11);
        AddRow("Explanation:", lblExplanation, labelX, valueX, startY + rowH * 12);

        // Set form size
        ClientSize = new Size(500, startY + rowH * 13 + 20);
        Text = "Candidate Details";
    }

    // Helper method to add a header label and value label row
    private void AddRow(string headerText, Label valueLbl, int hx, int vx, int y)
    {
        var header = new Label();
        header.Text = headerText;
        header.Location = new Point(hx, y);
        header.Size = new Size(180, 20);
        Controls.Add(header);

        valueLbl.Location = new Point(vx, y);
        valueLbl.Size = new Size(280, 20);
        Controls.Add(valueLbl);
    }

    // Declare all value labels
    private Label lblName;
    private Label lblEmail;
    private Label lblCCATRaw;
    private Label lblCCATMath;
    private Label lblCCATVerbal;
    private Label lblCCATSpatial;
    private Label lblCBSTRaw;
    private Label lblCBSTMath;
    private Label lblCBSTVerbal;
    private Label lblCMRA;
    private Label lblRecommendation;
    private Label lblReadiness;
    private Label lblExplanation;
}