using TraineeScreeningTool.WinForms.Data;

namespace TraineeScreeningTool.WinForms;

public partial class AssessForm : Form
{
    // Stores the ID of the candidate being assessed
    private readonly int _candidateId;

    // Constructor - accepts the candidate ID from MainForm
    public AssessForm(int id)
    {
        InitializeComponent();
        _candidateId = id;
        LoadExistingScores();
    }

    // Loads existing scores into the text boxes if they already exist
    private void LoadExistingScores()
    {
        using var context = new ApplicationDbContext();
        var candidate = context.Candidates.Find(_candidateId);
        if (candidate == null) return;

        // Set form title
        this.Text = $"Assess - {candidate.FirstName} {candidate.LastName}";

        // CCAT scores
        if (candidate.CCATRawScore.HasValue)
            txtCCATRaw.Text = candidate.CCATRawScore.ToString();
        if (candidate.CCATOverallPercentile.HasValue)
            txtCCATOverall.Text = candidate.CCATOverallPercentile.ToString();
        if (candidate.CCATMathPercentile.HasValue)
            txtCCATMath.Text = candidate.CCATMathPercentile.ToString();
        if (candidate.CCATVerbalPercentile.HasValue)
            txtCCATVerbal.Text = candidate.CCATVerbalPercentile.ToString();
        if (candidate.CCATSpatialPercentile.HasValue)
            txtCCATSpatial.Text = candidate.CCATSpatialPercentile.ToString();

        // CMRA
        if (candidate.CMRAOverallPercentile.HasValue)
            txtCMRA.Text = candidate.CMRAOverallPercentile.ToString();

        // CBST scores
        if (candidate.CBSTRawScore.HasValue)
            txtCBSTRaw.Text = candidate.CBSTRawScore.ToString();
        if (candidate.CBSTOverallPercentile.HasValue)
            txtCBSTOverall.Text = candidate.CBSTOverallPercentile.ToString();
        if (candidate.CBSTMathRaw.HasValue)
            txtCBSTMath.Text = candidate.CBSTMathRaw.ToString();
        if (candidate.CBSTVerbalRaw.HasValue)
            txtCBSTVerbal.Text = candidate.CBSTVerbalRaw.ToString();

        // CLIK
        if (candidate.CLIKRawScore.HasValue)
            txtCLIKRaw.Text = candidate.CLIKRawScore.ToString();
        txtCLIKProf.Text = candidate.CLIKProficiency ?? string.Empty;

        // Typing
        if (candidate.TypingWordsPerMinute.HasValue)
            txtTypingWPM.Text = candidate.TypingWordsPerMinute.ToString();
        if (candidate.TypingErrors.HasValue)
            txtTypingErrors.Text = candidate.TypingErrors.ToString();
        if (candidate.TypingOverallPercentile.HasValue)
            txtTypingPercentile.Text = candidate.TypingOverallPercentile.ToString();

        // CAST
        if (candidate.CASTOverallPercentile.HasValue)
            txtCASTOverall.Text = candidate.CASTOverallPercentile.ToString();
        if (candidate.CASTDividedAttention.HasValue)
            txtCASTDivided.Text = candidate.CASTDividedAttention.ToString();
        if (candidate.CASTFiltering.HasValue)
            txtCASTFiltering.Text = candidate.CASTFiltering.ToString();
        if (candidate.CASTReactionTime.HasValue)
            txtCASTReaction.Text = candidate.CASTReactionTime.ToString();
        if (candidate.CASTVigilance.HasValue)
            txtCASTVigilance.Text = candidate.CASTVigilance.ToString();

        // Word and Excel
        if (candidate.WordRawScore.HasValue)
            txtWordRaw.Text = candidate.WordRawScore.ToString();
        txtWordProf.Text = candidate.WordProficiency ?? string.Empty;
        if (candidate.ExcelRawScore.HasValue)
            txtExcelRaw.Text = candidate.ExcelRawScore.ToString();
        txtExcelProf.Text = candidate.ExcelProficiency ?? string.Empty;

        // CSAP
        txtCSAPRec.Text = candidate.CSAPRecommendation ?? string.Empty;
        if (candidate.CSAPAchievement.HasValue)
            txtCSAPAchievement.Text = candidate.CSAPAchievement.ToString();
        if (candidate.CSAPAssertiveness.HasValue)
            txtCSAPAssertiveness.Text = candidate.CSAPAssertiveness.ToString();
        if (candidate.CSAPCooperativeness.HasValue)
            txtCSAPCooperativeness.Text = candidate.CSAPCooperativeness.ToString();
        if (candidate.CSAPGoalOrientation.HasValue)
            txtCSAPGoal.Text = candidate.CSAPGoalOrientation.ToString();
        if (candidate.CSAPMotivation.HasValue)
            txtCSAPMotivation.Text = candidate.CSAPMotivation.ToString();
        if (candidate.CSAPTeamPlayer.HasValue)
            txtCSAPTeamPlayer.Text = candidate.CSAPTeamPlayer.ToString();

        // Talent Signal
        if (candidate.TalentSignal.HasValue)
            txtTalentSignal.Text = candidate.TalentSignal.ToString();
    }

    // Saves all assessment scores when Submit is clicked
    private void btnSubmit_Click(object sender, EventArgs e)
    {
        using var context = new ApplicationDbContext();
        var candidate = context.Candidates.Find(_candidateId);
        if (candidate == null) return;

        // Save CCAT scores
        if (int.TryParse(txtCCATRaw.Text, out int ccatRaw))
            candidate.CCATRawScore = ccatRaw;
        if (int.TryParse(txtCCATOverall.Text, out int ccatOverall))
            candidate.CCATOverallPercentile = ccatOverall;
        if (int.TryParse(txtCCATMath.Text, out int ccatMath))
            candidate.CCATMathPercentile = ccatMath;
        if (int.TryParse(txtCCATVerbal.Text, out int ccatVerbal))
            candidate.CCATVerbalPercentile = ccatVerbal;
        if (int.TryParse(txtCCATSpatial.Text, out int ccatSpatial))
            candidate.CCATSpatialPercentile = ccatSpatial;

        // Save CMRA
        if (int.TryParse(txtCMRA.Text, out int cmra))
            candidate.CMRAOverallPercentile = cmra;

        // Save CBST scores
        if (int.TryParse(txtCBSTRaw.Text, out int cbstRaw))
            candidate.CBSTRawScore = cbstRaw;
        if (int.TryParse(txtCBSTOverall.Text, out int cbstOverall))
            candidate.CBSTOverallPercentile = cbstOverall;
        if (int.TryParse(txtCBSTMath.Text, out int cbstMath))
            candidate.CBSTMathRaw = cbstMath;
        if (int.TryParse(txtCBSTVerbal.Text, out int cbstVerbal))
            candidate.CBSTVerbalRaw = cbstVerbal;

        // Save CLIK
        if (int.TryParse(txtCLIKRaw.Text, out int clikRaw))
            candidate.CLIKRawScore = clikRaw;
        candidate.CLIKProficiency = txtCLIKProf.Text.Trim();

        // Save Typing scores
        if (int.TryParse(txtTypingWPM.Text, out int wpm))
            candidate.TypingWordsPerMinute = wpm;
        if (int.TryParse(txtTypingErrors.Text, out int errors))
            candidate.TypingErrors = errors;
        if (int.TryParse(txtTypingPercentile.Text, out int typingPct))
            candidate.TypingOverallPercentile = typingPct;

        // Save CAST scores
        if (int.TryParse(txtCASTOverall.Text, out int castOverall))
            candidate.CASTOverallPercentile = castOverall;
        if (int.TryParse(txtCASTDivided.Text, out int castDivided))
            candidate.CASTDividedAttention = castDivided;
        if (int.TryParse(txtCASTFiltering.Text, out int castFiltering))
            candidate.CASTFiltering = castFiltering;
        if (int.TryParse(txtCASTReaction.Text, out int castReaction))
            candidate.CASTReactionTime = castReaction;
        if (int.TryParse(txtCASTVigilance.Text, out int castVigilance))
            candidate.CASTVigilance = castVigilance;

        // Save Word and Excel scores
        if (int.TryParse(txtWordRaw.Text, out int wordRaw))
            candidate.WordRawScore = wordRaw;
        candidate.WordProficiency = txtWordProf.Text.Trim();
        if (int.TryParse(txtExcelRaw.Text, out int excelRaw))
            candidate.ExcelRawScore = excelRaw;
        candidate.ExcelProficiency = txtExcelProf.Text.Trim();

        // Save CSAP scores
        candidate.CSAPRecommendation = txtCSAPRec.Text.Trim();
        if (int.TryParse(txtCSAPAchievement.Text, out int csapAchieve))
            candidate.CSAPAchievement = csapAchieve;
        if (int.TryParse(txtCSAPAssertiveness.Text, out int csapAssert))
            candidate.CSAPAssertiveness = csapAssert;
        if (int.TryParse(txtCSAPCooperativeness.Text, out int csapCoop))
            candidate.CSAPCooperativeness = csapCoop;
        if (int.TryParse(txtCSAPGoal.Text, out int csapGoal))
            candidate.CSAPGoalOrientation = csapGoal;
        if (int.TryParse(txtCSAPMotivation.Text, out int csapMotiv))
            candidate.CSAPMotivation = csapMotiv;
        if (int.TryParse(txtCSAPTeamPlayer.Text, out int csapTeam))
            candidate.CSAPTeamPlayer = csapTeam;

        // Save Talent Signal
        if (int.TryParse(txtTalentSignal.Text, out int talent))
            candidate.TalentSignal = talent;

        context.SaveChanges();
        this.Close();
    }
}