namespace TraineeScreeningTool.WinForms;

// This file handles the visual layout of the Assess form
partial class AssessForm
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
        // Initialize all text boxes
        txtCCATRaw = new TextBox();
        txtCCATMath = new TextBox();
        txtCCATVerbal = new TextBox();
        txtCCATSpatial = new TextBox();
        txtCBSTRaw = new TextBox();
        txtCBSTMath = new TextBox();
        txtCBSTVerbal = new TextBox();
        txtCMRA = new TextBox();
        txtTypingWPM = new TextBox();
        txtTalentSignal = new TextBox();
        btnSubmit = new Button();

        int labelX = 12;    // X position for labels
        int inputX = 180;   // X position for text boxes
        int inputW = 100;   // Width of text boxes
        int rowH = 35;      // Height between each row
        int startY = 15;    // Starting Y position

        // Helper labels and text boxes for each score
        AddRow("CCAT Raw Score:", txtCCATRaw, labelX, inputX, inputW, startY);
        AddRow("CCAT Math %:", txtCCATMath, labelX, inputX, inputW, startY + rowH);
        AddRow("CCAT Verbal %:", txtCCATVerbal, labelX, inputX, inputW, startY + rowH * 2);
        AddRow("CCAT Spatial %:", txtCCATSpatial, labelX, inputX, inputW, startY + rowH * 3);
        AddRow("CBST Raw Score:", txtCBSTRaw, labelX, inputX, inputW, startY + rowH * 4);
        AddRow("CBST Math:", txtCBSTMath, labelX, inputX, inputW, startY + rowH * 5);
        AddRow("CBST Verbal:", txtCBSTVerbal, labelX, inputX, inputW, startY + rowH * 6);
        AddRow("CMRA Percentile:", txtCMRA, labelX, inputX, inputW, startY + rowH * 7);
        AddRow("Typing WPM:", txtTypingWPM, labelX, inputX, inputW, startY + rowH * 8);
        AddRow("Talent Signal:", txtTalentSignal, labelX, inputX, inputW, startY + rowH * 9);

        // Submit button
        btnSubmit.Location = new Point(inputX, startY + rowH * 10);
        btnSubmit.Size = new Size(100, 30);
        btnSubmit.Text = "Submit";
        btnSubmit.Click += btnSubmit_Click;
        Controls.Add(btnSubmit);

        // Set form size
        ClientSize = new Size(320, startY + rowH * 11);
        Text = "Enter Assessment Scores";
    }

    // Helper method to add a label and text box row
    private void AddRow(string labelText, TextBox txt, int lx, int tx, int tw, int y)
    {
        var lbl = new Label();
        lbl.Text = labelText;
        lbl.Location = new Point(lx, y + 3);
        lbl.Size = new Size(160, 20);
        Controls.Add(lbl);

        txt.Location = new Point(tx, y);
        txt.Size = new Size(tw, 23);
        Controls.Add(txt);
    }

    // Declare all text box controls
    private TextBox txtCCATRaw;
    private TextBox txtCCATMath;
    private TextBox txtCCATVerbal;
    private TextBox txtCCATSpatial;
    private TextBox txtCBSTRaw;
    private TextBox txtCBSTMath;
    private TextBox txtCBSTVerbal;
    private TextBox txtCMRA;
    private TextBox txtTypingWPM;
    private TextBox txtTalentSignal;
    private Button btnSubmit;
}