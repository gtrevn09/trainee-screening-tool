using Microsoft.EntityFrameworkCore;
using TraineeScreeningTool.WinForms.Data;
using TraineeScreeningTool.WinForms.Models;

namespace TraineeScreeningTool.WinForms;

public partial class DetailsForm : Form
{
    private readonly int _candidateId;
    private readonly string _username;

    public DetailsForm(int id, string username = "system")
    {
        InitializeComponent();
        _candidateId = id;
        _username    = username;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        using var context = new ApplicationDbContext();
        var c = context.Candidates.Find(_candidateId);
        if (c == null) return;

        lblName.Text         = c.FullName;
        lblEmail.Text        = c.Email;
        lblTestDate.Text     = c.TestDate ?? "N/A";
        lblTalentSignal.Text = c.TalentSignal?.ToString() ?? "N/A";

        lblCCATRaw.Text     = c.CCATRawScore?.ToString()          ?? "N/A";
        lblCCATOverall.Text = c.CCATOverallPercentile?.ToString()  ?? "N/A";
        lblCCATMath.Text    = c.CCATMathPercentile?.ToString()     ?? "N/A";
        lblCCATVerbal.Text  = c.CCATVerbalPercentile?.ToString()   ?? "N/A";
        lblCCATSpatial.Text = c.CCATSpatialPercentile?.ToString()  ?? "N/A";

        lblCMRA.Text = c.CMRAOverallPercentile?.ToString() ?? "N/A";

        lblCBSTRaw.Text     = c.CBSTRawScore?.ToString()          ?? "N/A";
        lblCBSTOverall.Text = c.CBSTOverallPercentile?.ToString() ?? "N/A";
        lblCBSTMath.Text    = c.CBSTMathRaw?.ToString()           ?? "N/A";
        lblCBSTVerbal.Text  = c.CBSTVerbalRaw?.ToString()         ?? "N/A";

        lblCLIKRaw.Text  = c.CLIKRawScore?.ToString() ?? "N/A";
        lblCLIKProf.Text = c.CLIKProficiency          ?? "N/A";

        lblTypingWPM.Text        = c.TypingWordsPerMinute?.ToString()    ?? "N/A";
        lblTypingErrors.Text     = c.TypingErrors?.ToString()            ?? "N/A";
        lblTypingPercentile.Text = c.TypingOverallPercentile?.ToString() ?? "N/A";

        lblCASTOverall.Text   = c.CASTOverallPercentile?.ToString() ?? "N/A";
        lblCASTDivided.Text   = c.CASTDividedAttention?.ToString()  ?? "N/A";
        lblCASTFiltering.Text = c.CASTFiltering?.ToString()         ?? "N/A";
        lblCASTReaction.Text  = c.CASTReactionTime?.ToString()      ?? "N/A";
        lblCASTVigilance.Text = c.CASTVigilance?.ToString()         ?? "N/A";

        lblWordRaw.Text   = c.WordRawScore?.ToString()  ?? "N/A";
        lblWordProf.Text  = c.WordProficiency           ?? "N/A";
        lblExcelRaw.Text  = c.ExcelRawScore?.ToString() ?? "N/A";
        lblExcelProf.Text = c.ExcelProficiency          ?? "N/A";

        lblCSAPRec.Text             = c.CSAPRecommendation            ?? "N/A";
        lblCSAPAchievement.Text     = c.CSAPAchievement?.ToString()   ?? "N/A";
        lblCSAPAssertiveness.Text   = c.CSAPAssertiveness?.ToString() ?? "N/A";
        lblCSAPCooperativeness.Text = c.CSAPCooperativeness?.ToString() ?? "N/A";
        lblCSAPGoal.Text            = c.CSAPGoalOrientation?.ToString() ?? "N/A";
        lblCSAPMotivation.Text      = c.CSAPMotivation?.ToString()    ?? "N/A";
        lblCSAPTeamPlayer.Text      = c.CSAPTeamPlayer?.ToString()    ?? "N/A";

        lblRecommendation.Text = c.Recommendation ?? "Pending";
        lblReadiness.Text      = c.ReadinessRating ?? "N/A";
        lblExplanation.Text    = c.Explanation ?? "N/A";

        LoadCertifications();
    }

    private void LoadCertifications()
    {
        using var context = new ApplicationDbContext();
        var certs = context.Certifications
            .Where(c => c.CandidateId == _candidateId)
            .OrderBy(c => c.Status)
            .ThenBy(c => c.CertName)
            .Select(c => new
            {
                c.Id,
                c.CertName,
                c.Status,
                Result = c.Result ?? "—",
                Date   = c.Date != null ? c.Date.Value.ToString("MMM d, yyyy") : "—",
                c.Notes
            })
            .ToList();

        dgvCertifications.DataSource = certs;

        if (dgvCertifications.Columns.Contains("Id"))
            dgvCertifications.Columns["Id"]!.Visible = false;
        if (dgvCertifications.Columns.Contains("CertName"))
            dgvCertifications.Columns["CertName"]!.HeaderText = "Certification";
        if (dgvCertifications.Columns.Contains("Status"))
            dgvCertifications.Columns["Status"]!.Width = 90;
        if (dgvCertifications.Columns.Contains("Result"))
            dgvCertifications.Columns["Result"]!.Width = 70;
        if (dgvCertifications.Columns.Contains("Date"))
            dgvCertifications.Columns["Date"]!.Width = 110;
        if (dgvCertifications.Columns.Contains("Notes"))
            dgvCertifications.Columns["Notes"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        foreach (DataGridViewRow row in dgvCertifications.Rows)
        {
            var resultVal = row.Cells["Result"]?.Value?.ToString();
            if (resultVal == "Pass")
                row.DefaultCellStyle.BackColor = Color.FromArgb(220, 255, 220);
            else if (resultVal == "Fail")
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
        }
    }

    private void dgvCertifications_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var row = dgvCertifications.Rows[e.RowIndex];
        if (row.DataBoundItem == null) return;

        dynamic item = row.DataBoundItem;
        int certId = (int)item.Id;

        var editForm = new EditCertificationForm(_username, certId);
        if (editForm.ShowDialog() == DialogResult.OK)
            LoadCertifications();
    }

    private void btnAddCertification_Click(object sender, EventArgs e)
    {
        using var context = new ApplicationDbContext();
        var candidate = context.Candidates.Find(_candidateId);
        string name = candidate?.FullName ?? $"Candidate #{_candidateId}";

        var addForm = new AddCertificationForm(_username, _candidateId, name);
        if (addForm.ShowDialog() == DialogResult.OK)
            LoadCertifications();
    }
}