namespace TraineeScreeningTool.WinForms;

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
        txtCCATOverall = new TextBox();
        txtCCATMath = new TextBox();
        txtCCATVerbal = new TextBox();
        txtCCATSpatial = new TextBox();
        txtCMRA = new TextBox();
        txtCBSTRaw = new TextBox();
        txtCBSTOverall = new TextBox();
        txtCBSTMath = new TextBox();
        txtCBSTVerbal = new TextBox();
        txtCLIKRaw = new TextBox();
        txtCLIKProf = new TextBox();
        txtTypingWPM = new TextBox();
        txtTypingErrors = new TextBox();
        txtTypingPercentile = new TextBox();
        txtCASTOverall = new TextBox();
        txtCASTDivided = new TextBox();
        txtCASTFiltering = new TextBox();
        txtCASTReaction = new TextBox();
        txtCASTVigilance = new TextBox();
        txtWordRaw = new TextBox();
        txtWordProf = new TextBox();
        txtExcelRaw = new TextBox();
        txtExcelProf = new TextBox();
        txtCSAPRec = new TextBox();
        txtCSAPAchievement = new TextBox();
        txtCSAPAssertiveness = new TextBox();
        txtCSAPCooperativeness = new TextBox();
        txtCSAPGoal = new TextBox();
        txtCSAPMotivation = new TextBox();
        txtCSAPTeamPlayer = new TextBox();
        txtTalentSignal = new TextBox();
        btnSubmit = new Button();

        int lx = 30;
        int tx = 320;
        int tw = 180;
        int rh = 35;
        int y = 15;

        void AddSection(string text, ref int yPos)
        {
            var lbl = new Label();
            lbl.Text = text;
            lbl.Location = new Point(lx, yPos);
            lbl.Size = new Size(520, 22);
            lbl.Font = new Font("Segoe UI", 9, FontStyle.Bold | FontStyle.Underline);
            lbl.ForeColor = Color.SteelBlue;
            Controls.Add(lbl);
            yPos += 26;
        }

        void AddRow(string labelText, TextBox txt, ref int yPos)
        {
            var lbl = new Label();
            lbl.Text = labelText;
            lbl.Location = new Point(lx, yPos + 3);
            lbl.Size = new Size(270, 22);
            Controls.Add(lbl);

            txt.Location = new Point(tx, yPos);
            txt.Size = new Size(tw, 25);
            Controls.Add(txt);
            yPos += rh;
        }

        // CCAT section
        AddSection("CCAT — Cognitive Aptitude Test", ref y);
        AddRow("Raw Score:", txtCCATRaw, ref y);
        AddRow("Overall Percentile:", txtCCATOverall, ref y);
        AddRow("Math & Logic Percentile:", txtCCATMath, ref y);
        AddRow("Verbal Ability Percentile:", txtCCATVerbal, ref y);
        AddRow("Spatial Reasoning Percentile:", txtCCATSpatial, ref y);
        y += 5;

        // CMRA section
        AddSection("CMRA — Mechanical Reasoning", ref y);
        AddRow("Overall Percentile:", txtCMRA, ref y);
        y += 5;

        // CBST section
        AddSection("CBST — Basic Skills Test", ref y);
        AddRow("Raw Score:", txtCBSTRaw, ref y);
        AddRow("Overall Percentile:", txtCBSTOverall, ref y);
        AddRow("Math Raw Score:", txtCBSTMath, ref y);
        AddRow("Verbal Raw Score:", txtCBSTVerbal, ref y);
        y += 5;

        // CLIK section
        AddSection("CLIK — Computer Literacy", ref y);
        AddRow("Raw Score:", txtCLIKRaw, ref y);
        AddRow("Proficiency:", txtCLIKProf, ref y);
        y += 5;

        // Typing section
        AddSection("TT — Typing Test", ref y);
        AddRow("Words Per Minute:", txtTypingWPM, ref y);
        AddRow("Errors:", txtTypingErrors, ref y);
        AddRow("Overall Percentile:", txtTypingPercentile, ref y);
        y += 5;

        // CAST section
        AddSection("CAST — Attention Skills Test", ref y);
        AddRow("Overall Percentile:", txtCASTOverall, ref y);
        AddRow("Divided Attention:", txtCASTDivided, ref y);
        AddRow("Filtering:", txtCASTFiltering, ref y);
        AddRow("Reaction Time:", txtCASTReaction, ref y);
        AddRow("Vigilance:", txtCASTVigilance, ref y);
        y += 5;

        // Word and Excel section
        AddSection("Microsoft Office Skills", ref y);
        AddRow("Word 365 Raw Score:", txtWordRaw, ref y);
        AddRow("Word 365 Proficiency:", txtWordProf, ref y);
        AddRow("Excel 365 Raw Score:", txtExcelRaw, ref y);
        AddRow("Excel 365 Proficiency:", txtExcelProf, ref y);
        y += 5;

        // CSAP section
        AddSection("CSAP — Customer Service Profile", ref y);
        AddRow("Overall Recommendation:", txtCSAPRec, ref y);
        AddRow("Achievement:", txtCSAPAchievement, ref y);
        AddRow("Assertiveness:", txtCSAPAssertiveness, ref y);
        AddRow("Cooperativeness:", txtCSAPCooperativeness, ref y);
        AddRow("Goal Orientation:", txtCSAPGoal, ref y);
        AddRow("Motivation:", txtCSAPMotivation, ref y);
        AddRow("Team Player:", txtCSAPTeamPlayer, ref y);
        y += 5;

        // Talent Signal
        AddSection("Overall", ref y);
        AddRow("Talent Signal:", txtTalentSignal, ref y);
        y += 10;

        // Submit button
        btnSubmit.Location = new Point(tx, y);
        btnSubmit.Size = new Size(120, 35);
        btnSubmit.Text = "Submit";
        btnSubmit.Click += btnSubmit_Click;
        Controls.Add(btnSubmit);
        y += 50;

        // Put everything in a scrollable panel
        var panel = new Panel();
        panel.AutoScroll = true;
        panel.Dock = DockStyle.Fill;

        var controlsCopy = Controls.Cast<Control>().ToList();
        Controls.Clear();
        foreach (var ctrl in controlsCopy)
            panel.Controls.Add(ctrl);

        Controls.Add(panel);

        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(820, 700);
        MinimumSize = new Size(820, 650);
        Text = "Assess Candidate";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.Sizable;
    }

    // Declare all text boxes
    private TextBox txtCCATRaw, txtCCATOverall, txtCCATMath, txtCCATVerbal, txtCCATSpatial;
    private TextBox txtCMRA;
    private TextBox txtCBSTRaw, txtCBSTOverall, txtCBSTMath, txtCBSTVerbal;
    private TextBox txtCLIKRaw, txtCLIKProf;
    private TextBox txtTypingWPM, txtTypingErrors, txtTypingPercentile;
    private TextBox txtCASTOverall, txtCASTDivided, txtCASTFiltering, txtCASTReaction, txtCASTVigilance;
    private TextBox txtWordRaw, txtWordProf, txtExcelRaw, txtExcelProf;
    private TextBox txtCSAPRec, txtCSAPAchievement, txtCSAPAssertiveness, txtCSAPCooperativeness;
    private TextBox txtCSAPGoal, txtCSAPMotivation, txtCSAPTeamPlayer;
    private TextBox txtTalentSignal;
    private Button btnSubmit;
}