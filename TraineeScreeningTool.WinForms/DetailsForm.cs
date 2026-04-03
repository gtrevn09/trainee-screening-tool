using TraineeScreeningTool.WinForms.Data;

namespace TraineeScreeningTool.WinForms;

public partial class DetailsForm : Form
{
    // Constructor - accepts the candidate ID from MainForm
    public DetailsForm(int id)
    {
        InitializeComponent();

        using var context = new ApplicationDbContext();
        var c = context.Candidates.Find(id);
        if (c == null) return;

        // Basic info
        lblName.Text = c.FullName;
        lblEmail.Text = c.Email;
        lblTestDate.Text = c.TestDate ?? "N/A";
        lblTalentSignal.Text = c.TalentSignal?.ToString() ?? "N/A";

        // CCAT scores
        lblCCATRaw.Text = c.CCATRawScore?.ToString() ?? "N/A";
        lblCCATOverall.Text = c.CCATOverallPercentile?.ToString() ?? "N/A";
        lblCCATMath.Text = c.CCATMathPercentile?.ToString() ?? "N/A";
        lblCCATVerbal.Text = c.CCATVerbalPercentile?.ToString() ?? "N/A";
        lblCCATSpatial.Text = c.CCATSpatialPercentile?.ToString() ?? "N/A";

        // CMRA scores
        lblCMRA.Text = c.CMRAOverallPercentile?.ToString() ?? "N/A";

        // CBST scores
        lblCBSTRaw.Text = c.CBSTRawScore?.ToString() ?? "N/A";
        lblCBSTOverall.Text = c.CBSTOverallPercentile?.ToString() ?? "N/A";
        lblCBSTMath.Text = c.CBSTMathRaw?.ToString() ?? "N/A";
        lblCBSTVerbal.Text = c.CBSTVerbalRaw?.ToString() ?? "N/A";

        // CLIK scores
        lblCLIKRaw.Text = c.CLIKRawScore?.ToString() ?? "N/A";
        lblCLIKProf.Text = c.CLIKProficiency ?? "N/A";

        // Typing scores
        lblTypingWPM.Text = c.TypingWordsPerMinute?.ToString() ?? "N/A";
        lblTypingErrors.Text = c.TypingErrors?.ToString() ?? "N/A";
        lblTypingPercentile.Text = c.TypingOverallPercentile?.ToString() ?? "N/A";

        // CAST scores
        lblCASTOverall.Text = c.CASTOverallPercentile?.ToString() ?? "N/A";
        lblCASTDivided.Text = c.CASTDividedAttention?.ToString() ?? "N/A";
        lblCASTFiltering.Text = c.CASTFiltering?.ToString() ?? "N/A";
        lblCASTReaction.Text = c.CASTReactionTime?.ToString() ?? "N/A";
        lblCASTVigilance.Text = c.CASTVigilance?.ToString() ?? "N/A";

        // Word and Excel scores
        lblWordRaw.Text = c.WordRawScore?.ToString() ?? "N/A";
        lblWordProf.Text = c.WordProficiency ?? "N/A";
        lblExcelRaw.Text = c.ExcelRawScore?.ToString() ?? "N/A";
        lblExcelProf.Text = c.ExcelProficiency ?? "N/A";

        // CSAP scores
        lblCSAPRec.Text = c.CSAPRecommendation ?? "N/A";
        lblCSAPAchievement.Text = c.CSAPAchievement?.ToString() ?? "N/A";
        lblCSAPAssertiveness.Text = c.CSAPAssertiveness?.ToString() ?? "N/A";
        lblCSAPCooperativeness.Text = c.CSAPCooperativeness?.ToString() ?? "N/A";
        lblCSAPGoal.Text = c.CSAPGoalOrientation?.ToString() ?? "N/A";
        lblCSAPMotivation.Text = c.CSAPMotivation?.ToString() ?? "N/A";
        lblCSAPTeamPlayer.Text = c.CSAPTeamPlayer?.ToString() ?? "N/A";

        // Recommendation
        lblRecommendation.Text = c.Recommendation ?? "Pending";
        lblReadiness.Text = c.ReadinessRating ?? "N/A";
        lblExplanation.Text = c.Explanation ?? "N/A";
    }
}