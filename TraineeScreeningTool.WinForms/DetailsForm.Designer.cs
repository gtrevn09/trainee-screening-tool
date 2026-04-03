namespace TraineeScreeningTool.WinForms;

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
        lblName = new Label();
        lblEmail = new Label();
        lblTestDate = new Label();
        lblTalentSignal = new Label();
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
        lblRecommendation = new Label();
        lblReadiness = new Label();
        lblExplanation = new Label();

        int lx = 12;
        int vx = 230;
        int rh = 32;  // row height - increased to prevent text clipping
        int y = 15;

        void AddSection(string text, ref int yPos)
        {
            var lbl = new Label();
            lbl.Text = text;
            lbl.Location = new Point(lx, yPos);
            lbl.Size = new Size(580, 25);
            lbl.Font = new Font("Segoe UI", 9, FontStyle.Bold | FontStyle.Underline);
            lbl.ForeColor = Color.SteelBlue;
            Controls.Add(lbl);
            yPos += 28;
        }

        void AddRow(string header, Label valueLbl, ref int yPos)
        {
            var hdr = new Label();
            hdr.Text = header;
            hdr.Location = new Point(lx, yPos);
            hdr.Size = new Size(210, 25);
            hdr.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            Controls.Add(hdr);

            valueLbl.Location = new Point(vx, yPos);
            valueLbl.Size = new Size(370, 25);
            Controls.Add(valueLbl);
            yPos += rh;
        }

        // Basic Info
        AddSection("Basic Information", ref y);
        AddRow("Name:", lblName, ref y);
        AddRow("Email:", lblEmail, ref y);
        AddRow("Test Date:", lblTestDate, ref y);
        AddRow("Talent Signal:", lblTalentSignal, ref y);
        y += 5;

        // CCAT
        AddSection("CCAT — Cognitive Aptitude Test", ref y);
        AddRow("Raw Score:", lblCCATRaw, ref y);
        AddRow("Overall Percentile:", lblCCATOverall, ref y);
        AddRow("Math & Logic Percentile:", lblCCATMath, ref y);
        AddRow("Verbal Ability Percentile:", lblCCATVerbal, ref y);
        AddRow("Spatial Reasoning Percentile:", lblCCATSpatial, ref y);
        y += 5;

        // CMRA
        AddSection("CMRA — Mechanical Reasoning Assessment", ref y);
        AddRow("Overall Percentile:", lblCMRA, ref y);
        y += 5;

        // CBST
        AddSection("CBST — Basic Skills Test", ref y);
        AddRow("Raw Score:", lblCBSTRaw, ref y);
        AddRow("Overall Percentile:", lblCBSTOverall, ref y);
        AddRow("Math Raw Score:", lblCBSTMath, ref y);
        AddRow("Verbal Raw Score:", lblCBSTVerbal, ref y);
        y += 5;

        // CLIK
        AddSection("CLIK — Computer Literacy Test", ref y);
        AddRow("Raw Score:", lblCLIKRaw, ref y);
        AddRow("Proficiency:", lblCLIKProf, ref y);
        y += 5;

        // Typing
        AddSection("TT — Typing Test", ref y);
        AddRow("Words Per Minute:", lblTypingWPM, ref y);
        AddRow("Errors:", lblTypingErrors, ref y);
        AddRow("Overall Percentile:", lblTypingPercentile, ref y);
        y += 5;

        // CAST
        AddSection("CAST — Attention Skills Test", ref y);
        AddRow("Overall Percentile:", lblCASTOverall, ref y);
        AddRow("Divided Attention:", lblCASTDivided, ref y);
        AddRow("Filtering:", lblCASTFiltering, ref y);
        AddRow("Reaction Time:", lblCASTReaction, ref y);
        AddRow("Vigilance:", lblCASTVigilance, ref y);
        y += 5;

        // Word & Excel
        AddSection("Microsoft Office Skills", ref y);
        AddRow("Word 365 Raw Score:", lblWordRaw, ref y);
        AddRow("Word 365 Proficiency:", lblWordProf, ref y);
        AddRow("Excel 365 Raw Score:", lblExcelRaw, ref y);
        AddRow("Excel 365 Proficiency:", lblExcelProf, ref y);
        y += 5;

        // CSAP
        AddSection("CSAP — Customer Service Aptitude Profile", ref y);
        AddRow("Overall Recommendation:", lblCSAPRec, ref y);
        AddRow("Achievement:", lblCSAPAchievement, ref y);
        AddRow("Assertiveness:", lblCSAPAssertiveness, ref y);
        AddRow("Cooperativeness:", lblCSAPCooperativeness, ref y);
        AddRow("Goal Orientation:", lblCSAPGoal, ref y);
        AddRow("Motivation:", lblCSAPMotivation, ref y);
        AddRow("Team Player:", lblCSAPTeamPlayer, ref y);
        y += 5;

        // Pathway Recommendation
        AddSection("Career Pathway Recommendation", ref y);
        AddRow("Recommendation:", lblRecommendation, ref y);
        AddRow("Readiness Rating:", lblReadiness, ref y);
        AddRow("Explanation:", lblExplanation, ref y);

        // Put everything in a scrollable panel
        var panel = new Panel();
        panel.AutoScroll = true;
        panel.Dock = DockStyle.Fill;

        var controlsCopy = Controls.Cast<Control>().ToList();
        Controls.Clear();
        foreach (var ctrl in controlsCopy)
            panel.Controls.Add(ctrl);

        Controls.Add(panel);

        ClientSize = new Size(640, 700);
        Text = "Candidate Details";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.Sizable;
    }

    private Label lblName, lblEmail, lblTestDate, lblTalentSignal;
    private Label lblCCATRaw, lblCCATOverall, lblCCATMath, lblCCATVerbal, lblCCATSpatial;
    private Label lblCMRA;
    private Label lblCBSTRaw, lblCBSTOverall, lblCBSTMath, lblCBSTVerbal;
    private Label lblCLIKRaw, lblCLIKProf;
    private Label lblTypingWPM, lblTypingErrors, lblTypingPercentile;
    private Label lblCASTOverall, lblCASTDivided, lblCASTFiltering, lblCASTReaction, lblCASTVigilance;
    private Label lblWordRaw, lblWordProf, lblExcelRaw, lblExcelProf;
    private Label lblCSAPRec, lblCSAPAchievement, lblCSAPAssertiveness, lblCSAPCooperativeness;
    private Label lblCSAPGoal, lblCSAPMotivation, lblCSAPTeamPlayer;
    private Label lblRecommendation, lblReadiness, lblExplanation;
}