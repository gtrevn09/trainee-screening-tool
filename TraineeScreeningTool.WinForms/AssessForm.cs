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
        LoadExistingScores(); // Auto populate scores if they already exist
    }

    // Loads existing scores into the text boxes if they are already in the database
    private void LoadExistingScores()
    {
        using var context = new ApplicationDbContext(); // Open database connection
        var candidate = context.Candidates.Find(_candidateId); // Find candidate by ID
        if (candidate == null) return; // Safety check - exit if not found

        // Set the form title to show which candidate is being assessed
        this.Text = $"Assess - {candidate.FirstName} {candidate.LastName}";

        // Auto populate CCAT scores if they exist
        if (candidate.CCATRawScore.HasValue)
            txtCCATRaw.Text = candidate.CCATRawScore.ToString();
        if (candidate.CCATMathPercentile.HasValue)
            txtCCATMath.Text = candidate.CCATMathPercentile.ToString();
        if (candidate.CCATVerbalPercentile.HasValue)
            txtCCATVerbal.Text = candidate.CCATVerbalPercentile.ToString();
        if (candidate.CCATSpatialPercentile.HasValue)
            txtCCATSpatial.Text = candidate.CCATSpatialPercentile.ToString();

        // Auto populate CBST scores if they exist
        if (candidate.CBSTRawScore.HasValue)
            txtCBSTRaw.Text = candidate.CBSTRawScore.ToString();
        if (candidate.CBSTMathRaw.HasValue)
            txtCBSTMath.Text = candidate.CBSTMathRaw.ToString();
        if (candidate.CBSTVerbalRaw.HasValue)
            txtCBSTVerbal.Text = candidate.CBSTVerbalRaw.ToString();

        // Auto populate CMRA score if it exists
        if (candidate.CMRAOverallPercentile.HasValue)
            txtCMRA.Text = candidate.CMRAOverallPercentile.ToString();

        // Auto populate Typing score if it exists
        if (candidate.TypingWordsPerMinute.HasValue)
            txtTypingWPM.Text = candidate.TypingWordsPerMinute.ToString();

        // Auto populate Talent Signal if it exists
        if (candidate.TalentSignal.HasValue)
            txtTalentSignal.Text = candidate.TalentSignal.ToString();
    }

    // Saves the assessment scores when Submit is clicked
    private void btnSubmit_Click(object sender, EventArgs e)
    {
        using var context = new ApplicationDbContext(); // Open database connection
        var candidate = context.Candidates.Find(_candidateId); // Find candidate by ID
        if (candidate == null) return; // Safety check - exit if not found

        // Save CCAT scores if entered
        if (int.TryParse(txtCCATRaw.Text, out int ccatRaw))
            candidate.CCATRawScore = ccatRaw;
        if (int.TryParse(txtCCATMath.Text, out int ccatMath))
            candidate.CCATMathPercentile = ccatMath;
        if (int.TryParse(txtCCATVerbal.Text, out int ccatVerbal))
            candidate.CCATVerbalPercentile = ccatVerbal;
        if (int.TryParse(txtCCATSpatial.Text, out int ccatSpatial))
            candidate.CCATSpatialPercentile = ccatSpatial;

        // Save CBST scores if entered
        if (int.TryParse(txtCBSTRaw.Text, out int cbstRaw))
            candidate.CBSTRawScore = cbstRaw;
        if (int.TryParse(txtCBSTMath.Text, out int cbstMath))
            candidate.CBSTMathRaw = cbstMath;
        if (int.TryParse(txtCBSTVerbal.Text, out int cbstVerbal))
            candidate.CBSTVerbalRaw = cbstVerbal;

        // Save CMRA score if entered
        if (int.TryParse(txtCMRA.Text, out int cmra))
            candidate.CMRAOverallPercentile = cmra;

        // Save Typing score if entered
        if (int.TryParse(txtTypingWPM.Text, out int wpm))
            candidate.TypingWordsPerMinute = wpm;

        // Save Talent Signal if entered
        if (int.TryParse(txtTalentSignal.Text, out int talent))
            candidate.TalentSignal = talent;

        context.SaveChanges(); // Save results to database
        this.Close();          // Close the form after submitting
    }
}