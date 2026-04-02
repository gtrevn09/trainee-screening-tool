using TraineeScreeningTool.WinForms.Data;

namespace TraineeScreeningTool.WinForms;

public partial class DetailsForm : Form
{
    // Constructor - accepts the candidate ID from MainForm
    public DetailsForm(int id)
    {
        InitializeComponent(); // Builds the visual form from the Designer

        using var context = new ApplicationDbContext(); // Open database connection
        var c = context.Candidates.Find(id); // Find the candidate by ID
        if (c == null) return; // Safety check - exit if not found

        // Populate basic info labels
        lblName.Text = c.FullName;
        lblEmail.Text = c.Email;

        // Populate CCAT scores
        lblCCATRaw.Text = c.CCATRawScore?.ToString() ?? "N/A";
        lblCCATMath.Text = c.CCATMathPercentile?.ToString() ?? "N/A";
        lblCCATVerbal.Text = c.CCATVerbalPercentile?.ToString() ?? "N/A";
        lblCCATSpatial.Text = c.CCATSpatialPercentile?.ToString() ?? "N/A";

        // Populate CBST scores
        lblCBSTRaw.Text = c.CBSTRawScore?.ToString() ?? "N/A";
        lblCBSTMath.Text = c.CBSTMathRaw?.ToString() ?? "N/A";
        lblCBSTVerbal.Text = c.CBSTVerbalRaw?.ToString() ?? "N/A";

        // Populate CMRA score
        lblCMRA.Text = c.CMRAOverallPercentile?.ToString() ?? "N/A";

        // Populate recommendation results
        lblRecommendation.Text = c.Recommendation;
        lblReadiness.Text = c.ReadinessRating;
        lblExplanation.Text = c.Explanation;
    }
}