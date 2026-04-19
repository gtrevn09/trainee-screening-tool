using Microsoft.EntityFrameworkCore;
using TraineeScreeningTool.WinForms.Data;
using TraineeScreeningTool.WinForms.Models;

namespace TraineeScreeningTool.WinForms;

/// <summary>
/// Expanded candidate profile showing scores, certifications and placements
/// in a single tabbed window.
/// </summary>
public partial class CandidateProfileForm : Form
{
    private readonly int _candidateId;
    private readonly string _username;

    public CandidateProfileForm(int candidateId, string username = "system")
    {
        _candidateId = candidateId;
        _username = username;
        InitializeComponent();
    }

    // ─────────────────────────────────────────────
    //  Load
    // ─────────────────────────────────────────────

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        LoadProfile();
        LoadCertifications();
        LoadPlacements();
    }

    private void LoadProfile()
    {
        using var ctx = new ApplicationDbContext();
        var c = ctx.Candidates.Find(_candidateId);
        if (c == null) return;

        Text = $"Candidate Profile — {c.FullName}";

        // Header
        lblName.Text = c.FullName;
        lblEmail.Text = c.Email;
        lblTestDate.Text = string.IsNullOrWhiteSpace(c.TestDate) ? "N/A" : c.TestDate;
        lblTalentSignal.Text = c.TalentSignal?.ToString() ?? "N/A";

        // Recommendation banner
        lblRecommendation.Text = c.Recommendation ?? "Pending";
        lblReadiness.Text = c.ReadinessRating ?? "N/A";
        lblExplanation.Text = c.Explanation ?? "N/A";

        lblRecommendation.ForeColor = (c.Recommendation ?? "") switch
        {
            var r when r.Contains("High") => Color.DarkGreen,
            var r when r.Contains("Medium") => Color.DarkOrange,
            var r when r.Contains("Low") => Color.Firebrick,
            _ => Color.DimGray
        };

        // ── Scores tab ──────────────────────────────
        SetScore(lblCCATRaw, c.CCATRawScore);
        SetScore(lblCCATOverall, c.CCATOverallPercentile);
        SetScore(lblCCATMath, c.CCATMathPercentile);
        SetScore(lblCCATVerbal, c.CCATVerbalPercentile);
        SetScore(lblCCATSpatial, c.CCATSpatialPercentile);

        SetScore(lblCMRA, c.CMRAOverallPercentile);

        SetScore(lblCBSTRaw, c.CBSTRawScore);
        SetScore(lblCBSTOverall, c.CBSTOverallPercentile);
        SetScore(lblCBSTMath, c.CBSTMathRaw);
        SetScore(lblCBSTVerbal, c.CBSTVerbalRaw);

        SetScore(lblCLIKRaw, c.CLIKRawScore);
        lblCLIKProf.Text = string.IsNullOrWhiteSpace(c.CLIKProficiency) ? "N/A" : c.CLIKProficiency;

        SetScore(lblTypingWPM, c.TypingWordsPerMinute);
        SetScore(lblTypingErrors, c.TypingErrors);
        SetScore(lblTypingPercentile, c.TypingOverallPercentile);

        SetScore(lblCASTOverall, c.CASTOverallPercentile);
        SetScore(lblCASTDivided, c.CASTDividedAttention);
        SetScore(lblCASTFiltering, c.CASTFiltering);
        SetScore(lblCASTReaction, c.CASTReactionTime);
        SetScore(lblCASTVigilance, c.CASTVigilance);

        SetScore(lblWordRaw, c.WordRawScore);
        lblWordProf.Text = string.IsNullOrWhiteSpace(c.WordProficiency) ? "N/A" : c.WordProficiency;
        SetScore(lblExcelRaw, c.ExcelRawScore);
        lblExcelProf.Text = string.IsNullOrWhiteSpace(c.ExcelProficiency) ? "N/A" : c.ExcelProficiency;

        lblCSAPRec.Text = string.IsNullOrWhiteSpace(c.CSAPRecommendation) ? "N/A" : c.CSAPRecommendation;
        SetScore(lblCSAPAchievement, c.CSAPAchievement);
        SetScore(lblCSAPAssertiveness, c.CSAPAssertiveness);
        SetScore(lblCSAPCooperativeness, c.CSAPCooperativeness);
        SetScore(lblCSAPGoal, c.CSAPGoalOrientation);
        SetScore(lblCSAPMotivation, c.CSAPMotivation);
        SetScore(lblCSAPTeamPlayer, c.CSAPTeamPlayer);
    }

    private static void SetScore(Label lbl, int? value)
        => lbl.Text = value?.ToString() ?? "N/A";

    // ─────────────────────────────────────────────
    //  Certifications tab
    // ─────────────────────────────────────────────

    private void LoadCertifications()
    {
        using var ctx = new ApplicationDbContext();

        // FIX: use named CertDisplayRow instead of anonymous type so the
        // double-click handler can pattern-match safely without dynamic
        var certs = ctx.Certifications
            .Where(c => c.CandidateId == _candidateId)
            .OrderBy(c => c.Status)
            .ThenBy(c => c.CertName)
            .AsEnumerable()
            .Select(c => new CertDisplayRow
            {
                Id = c.Id,
                CertName = c.CertName,
                Status = c.Status,
                Result = c.Result ?? "—",
                Date = c.Date != null ? c.Date.Value.ToString("MMM d, yyyy") : "—",
                Notes = c.Notes
            })
            .ToList();

        dgvCertifications.DataSource = certs;

        HideCol(dgvCertifications, "Id");
        SetColHeader(dgvCertifications, "CertName", "Certification");
        SetColWidth(dgvCertifications, "Status", 90);
        SetColWidth(dgvCertifications, "Result", 70);
        SetColWidth(dgvCertifications, "Date", 110);
        FillCol(dgvCertifications, "Notes");

        foreach (DataGridViewRow row in dgvCertifications.Rows)
        {
            var result = row.Cells["Result"]?.Value?.ToString();
            row.DefaultCellStyle.BackColor = result switch
            {
                "Pass" => Color.FromArgb(220, 255, 220),
                "Fail" => Color.FromArgb(255, 220, 220),
                _ => SystemColors.Window
            };
        }

        UpdateCertSummary();
    }

    private void UpdateCertSummary()
    {
        using var ctx = new ApplicationDbContext();
        var all = ctx.Certifications.Where(c => c.CandidateId == _candidateId).ToList();
        int done = all.Count(c => c.Status == "Completed" && c.Result == "Pass");
        lblCertSummary.Text = $"{all.Count} certification(s) — {done} passed";
    }

    private void dgvCertifications_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        // FIX: pattern match on named type instead of using dynamic
        if (dgvCertifications.Rows[e.RowIndex].DataBoundItem is not CertDisplayRow row) return;
        if (new EditCertificationForm(_username, row.Id).ShowDialog() == DialogResult.OK)
            LoadCertifications();
    }

    private void btnAddCertification_Click(object sender, EventArgs e)
    {
        using var ctx = new ApplicationDbContext();
        var candidate = ctx.Candidates.Find(_candidateId);
        string name = candidate?.FullName ?? $"Candidate #{_candidateId}";
        if (new AddCertificationForm(_username, _candidateId, name).ShowDialog() == DialogResult.OK)
            LoadCertifications();
    }

    // ─────────────────────────────────────────────
    //  Placements tab
    // ─────────────────────────────────────────────

    private List<PlacementRow> _placementRows = new();

    private void LoadPlacements()
    {
        using var ctx = new ApplicationDbContext();

        _placementRows = ctx.JobPlacements
            .Include(p => p.Candidate)
            .Where(p => p.CandidateId == _candidateId)
            .AsEnumerable()
            .OrderByDescending(p => p.PlacementDate)
            .Select(p => new PlacementRow
            {
                Id = p.Id,
                CareerPathway = p.CareerPathway,
                PlacementDate = p.PlacementDate.ToString("MMM d, yyyy"),
                ExitDate = p.ExitDate.HasValue ? p.ExitDate.Value.ToString("MMM d, yyyy") : "—",
                MonthsPlaced = $"{p.MonthsPlaced:F1}",
                Status = p.Status,
                NeedsReview = p.NeedsReview,
                PastSixMonths = p.IsActive && p.MonthsPlaced >= 6
            })
            .ToList();

        dgvPlacements.DataSource = null;
        dgvPlacements.DataSource = _placementRows;

        HideCol(dgvPlacements, "Id");
        HideCol(dgvPlacements, "NeedsReview");
        HideCol(dgvPlacements, "PastSixMonths");

        SetColHeader(dgvPlacements, "CareerPathway", "Career Pathway");
        SetColHeader(dgvPlacements, "PlacementDate", "Placed On");
        SetColHeader(dgvPlacements, "ExitDate", "Exit Date");
        SetColHeader(dgvPlacements, "MonthsPlaced", "Months");
        SetColHeader(dgvPlacements, "Status", "Status");

        dgvPlacements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        foreach (DataGridViewRow row in dgvPlacements.Rows)
        {
            if (row.DataBoundItem is PlacementRow pr)
                row.DefaultCellStyle.BackColor = pr switch
                {
                    { PastSixMonths: true } => Color.FromArgb(255, 220, 150),
                    { NeedsReview: true } => Color.FromArgb(255, 255, 180),
                    _ => SystemColors.Window
                };
        }

        UpdatePlacementSummary();
    }

    private void UpdatePlacementSummary()
    {
        using var ctx = new ApplicationDbContext();
        var all = ctx.JobPlacements.Where(p => p.CandidateId == _candidateId).AsEnumerable().ToList();
        int successful = all.Count(p => p.IsSuccessful);
        int active = all.Count(p => p.IsActive);
        lblPlacementSummary.Text =
            $"{all.Count} placement(s) — {active} active, {successful} successful";
    }

    private void btnAddPlacement_Click(object sender, EventArgs e)
    {
        var form = new AddPlacementForm(_username, _candidateId);
        if (form.ShowDialog() == DialogResult.OK)
            LoadPlacements();
    }

    private void btnUpdatePlacement_Click(object sender, EventArgs e)
    {
        if (dgvPlacements.SelectedRows.Count == 0 ||
            dgvPlacements.SelectedRows[0].DataBoundItem is not PlacementRow pr)
        {
            MessageBox.Show("Please select a placement to update.", "No Selection");
            return;
        }
        if (new UpdatePlacementForm(_username, pr.Id).ShowDialog() == DialogResult.OK)
            LoadPlacements();
    }

    private void dgvPlacements_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (dgvPlacements.Rows[e.RowIndex].DataBoundItem is PlacementRow pr)
            if (new UpdatePlacementForm(_username, pr.Id).ShowDialog() == DialogResult.OK)
                LoadPlacements();
    }

    private void btnDeletePlacement_Click(object sender, EventArgs e)
    {
        if (dgvPlacements.SelectedRows.Count == 0 ||
            dgvPlacements.SelectedRows[0].DataBoundItem is not PlacementRow pr)
        {
            MessageBox.Show("Please select a placement to delete.", "No Selection");
            return;
        }

        var confirm = MessageBox.Show(
            $"Delete the placement in '{pr.CareerPathway}'?",
            "Confirm Delete",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (confirm != DialogResult.Yes) return;

        using var ctx = new ApplicationDbContext();
        var placement = ctx.JobPlacements.Find(pr.Id);
        if (placement != null)
        {
            ctx.JobPlacements.Remove(placement);
            ctx.SaveChanges();
            ctx.Log(_username, "Delete Placement",
                $"Deleted placement ID {pr.Id} for candidate ID {_candidateId}");
        }

        LoadPlacements();
    }

    // ─────────────────────────────────────────────
    //  Helpers
    // ─────────────────────────────────────────────

    private static void HideCol(DataGridView dgv, string name)
    {
        if (dgv.Columns.Contains(name)) dgv.Columns[name]!.Visible = false;
    }

    private static void SetColHeader(DataGridView dgv, string name, string header)
    {
        if (dgv.Columns.Contains(name)) dgv.Columns[name]!.HeaderText = header;
    }

    private static void SetColWidth(DataGridView dgv, string name, int width)
    {
        if (dgv.Columns.Contains(name)) dgv.Columns[name]!.Width = width;
    }

    private static void FillCol(DataGridView dgv, string name)
    {
        if (dgv.Columns.Contains(name))
            dgv.Columns[name]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    }

    // ─────────────────────────────────────────────
    //  Certification display row
    //  Named class replaces the anonymous type that was here before,
    //  allowing safe pattern-matching in the double-click handler.
    // ─────────────────────────────────────────────

    private class CertDisplayRow
    {
        public int Id { get; set; }
        public string CertName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }

    // ─────────────────────────────────────────────
    //  Placement display row
    // ─────────────────────────────────────────────

    private class PlacementRow
    {
        public int Id { get; set; }
        public string CareerPathway { get; set; } = string.Empty;
        public string PlacementDate { get; set; } = string.Empty;
        public string ExitDate { get; set; } = string.Empty;
        public string MonthsPlaced { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public bool NeedsReview { get; set; }
        public bool PastSixMonths { get; set; }
    }
}
