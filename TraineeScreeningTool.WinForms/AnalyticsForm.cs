using Microsoft.EntityFrameworkCore;
using System.Text;
using TraineeScreeningTool.WinForms.Data;
using TraineeScreeningTool.WinForms.Models;

namespace TraineeScreeningTool.WinForms;

public partial class AnalyticsForm : Form
{
    private readonly string _username;

    // Pass threshold applied to the composite percentile score.
    // All aptitude scores in this system are expressed as percentiles (0–100), so the
    // 50th percentile represents average performance — a reasonable default pass bar.
    private const int PassThreshold = 50;

    // Cached rows kept so the CSV export always matches what is displayed
    private List<PathwayRow> _pathwayRows = new();
    private List<CertRow> _certRows = new();
    private List<PassFailRow> _passFailRows = new();

    public AnalyticsForm(string username)
    {
        InitializeComponent();
        _username = username;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        LoadAnalytics();
    }

    // ── Entry point ────────────────────────────────────────────────────────────

    private void LoadAnalytics()
    {
        using var context = new ApplicationDbContext();

        var candidates = context.Candidates.ToList();
        var placements = context.JobPlacements.Include(p => p.Candidate).ToList();
        var certifications = context.Certifications.ToList();

        LoadSummaryStats(candidates, placements, certifications);
        LoadByPathway(placements, candidates);
        LoadByCertification(certifications);
        LoadPassFail(candidates);
    }

    // ── Summary stats bar ──────────────────────────────────────────────────────

    private void LoadSummaryStats(
        List<Candidate> candidates,
        List<JobPlacement> placements,
        List<Certification> certifications)
    {
        lblTotalCandidates.Text = $"Total Candidates: {candidates.Count}";
        lblTotalPlacements.Text = $"Successful Placements: {placements.Count(p => p.IsSuccessful)}";
        lblTotalCerts.Text = $"Certifications Earned: {certifications.Count(c => c.Result == "Pass")}";
    }

    // ── Tab 1 — Scores by Career Pathway ──────────────────────────────────────

    private void LoadByPathway(List<JobPlacement> placements, List<Candidate> allCandidates)
    {
        // Group placements by pathway; a candidate placed in multiple pathways counts once per pathway
        var grouped = placements
            .GroupBy(p => p.CareerPathway)
            .OrderBy(g => g.Key);

        _pathwayRows = grouped.Select(g =>
        {
            var candidateIds = g.Select(p => p.CandidateId).Distinct().ToList();
            var groupCandidates = allCandidates.Where(c => candidateIds.Contains(c.Id)).ToList();

            var ccatScores = NonNullDoubles(groupCandidates, c => c.CCATOverallPercentile);
            var cbstScores = NonNullDoubles(groupCandidates, c => c.CBSTOverallPercentile);
            var composites = groupCandidates
                .Select(ComputeCompositeScore)
                .Where(s => s.HasValue)
                .Select(s => s!.Value)
                .ToList();

            double? passRate = composites.Count > 0
                ? (double)composites.Count(s => s >= PassThreshold) / composites.Count * 100
                : null;

            return new PathwayRow
            {
                Pathway = g.Key,
                Candidates = candidateIds.Count,
                AvgCCAT = FormatAvg(ccatScores),
                AvgCBST = FormatAvg(cbstScores),
                AvgComposite = FormatAvg(composites),
                SuccessfulPlacements = g.Count(p => p.IsSuccessful),
                ScorePassRate = passRate.HasValue ? $"{passRate.Value:F0}%" : "—"
            };
        }).ToList();

        dgvByPathway.DataSource = null;
        dgvByPathway.DataSource = _pathwayRows;

        SetColHeader(dgvByPathway, "Pathway", "Career Pathway");
        SetColHeader(dgvByPathway, "Candidates", "# Candidates");
        SetColHeader(dgvByPathway, "AvgCCAT", "Avg CCAT %ile");
        SetColHeader(dgvByPathway, "AvgCBST", "Avg CBST %ile");
        SetColHeader(dgvByPathway, "AvgComposite", $"Avg Composite (pass ≥ {PassThreshold})");
        SetColHeader(dgvByPathway, "SuccessfulPlacements", "Successful Placements");
        SetColHeader(dgvByPathway, "ScorePassRate", "Score Pass Rate");

        dgvByPathway.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
    }

    // ── Tab 2 — Certification Outcomes ────────────────────────────────────────

    private void LoadByCertification(List<Certification> certifications)
    {
        _certRows = certifications
            .GroupBy(c => c.CertName)
            .OrderBy(g => g.Key)
            .Select(g =>
            {
                int passed = g.Count(c => c.Result == "Pass");
                int failed = g.Count(c => c.Result == "Fail");
                double? passRate = (passed + failed) > 0
                    ? (double)passed / (passed + failed) * 100
                    : null;

                return new CertRow
                {
                    CertName = g.Key,
                    Total = g.Count(),
                    Pursuing = g.Count(c => c.Status == "Pursuing"),
                    Passed = passed,
                    Failed = failed,
                    PassRate = passRate.HasValue ? $"{passRate.Value:F0}%" : "—"
                };
            }).ToList();

        dgvByCertification.DataSource = null;
        dgvByCertification.DataSource = _certRows;

        SetColHeader(dgvByCertification, "CertName", "Certification");
        SetColHeader(dgvByCertification, "Total", "Total");
        SetColHeader(dgvByCertification, "Pursuing", "Pursuing");
        SetColHeader(dgvByCertification, "Passed", "Passed");
        SetColHeader(dgvByCertification, "Failed", "Failed");
        SetColHeader(dgvByCertification, "PassRate", "Exam Pass Rate");

        dgvByCertification.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        foreach (DataGridViewRow row in dgvByCertification.Rows)
        {
            if (row.DataBoundItem is CertRow cr)
            {
                if (cr.Passed > 0 && cr.Passed >= cr.Failed)
                    row.DefaultCellStyle.BackColor = Color.FromArgb(220, 255, 220);
                else if (cr.Failed > cr.Passed)
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
            }
        }
    }

    // ── Tab 3 — Pass vs. Fail Analysis ────────────────────────────────────────

    private void LoadPassFail(List<Candidate> candidates)
    {
        // Only include candidates that have at least one percentile score recorded
        var scored = candidates
            .Select(c => (Candidate: c, Composite: ComputeCompositeScore(c)))
            .Where(x => x.Composite.HasValue)
            .ToList();

        var passCandidates = scored.Where(x => x.Composite!.Value >= PassThreshold).Select(x => x.Candidate).ToList();
        var failCandidates = scored.Where(x => x.Composite!.Value < PassThreshold).Select(x => x.Candidate).ToList();

        _passFailRows = new List<PassFailRow>
        {
            BuildPassFailRow($"Pass (composite ≥ {PassThreshold})", passCandidates),
            BuildPassFailRow($"Fail (composite < {PassThreshold})", failCandidates)
        };

        dgvPassFail.DataSource = null;
        dgvPassFail.DataSource = _passFailRows;

        SetColHeader(dgvPassFail, "Group", "Group");
        SetColHeader(dgvPassFail, "Count", "# Candidates");
        SetColHeader(dgvPassFail, "AvgComposite", "Avg Composite");
        SetColHeader(dgvPassFail, "AvgCCAT", "Avg CCAT %ile");
        SetColHeader(dgvPassFail, "AvgCBST", "Avg CBST %ile");
        SetColHeader(dgvPassFail, "AvgCMRA", "Avg CMRA %ile");
        SetColHeader(dgvPassFail, "AvgTyping", "Avg Typing %ile");
        SetColHeader(dgvPassFail, "AvgCAST", "Avg CAST %ile");

        dgvPassFail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        foreach (DataGridViewRow row in dgvPassFail.Rows)
        {
            if (row.DataBoundItem is PassFailRow pfr)
            {
                row.DefaultCellStyle.BackColor = pfr.Group.StartsWith("Pass")
                    ? Color.FromArgb(220, 255, 220)
                    : Color.FromArgb(255, 220, 220);
            }
        }
    }

    private PassFailRow BuildPassFailRow(string group, List<Candidate> candidates)
    {
        var composites = candidates
            .Select(ComputeCompositeScore)
            .Where(s => s.HasValue)
            .Select(s => s!.Value)
            .ToList();

        return new PassFailRow
        {
            Group = group,
            Count = candidates.Count,
            AvgComposite = FormatAvg(composites),
            AvgCCAT = FormatAvg(NonNullDoubles(candidates, c => c.CCATOverallPercentile)),
            AvgCBST = FormatAvg(NonNullDoubles(candidates, c => c.CBSTOverallPercentile)),
            AvgCMRA = FormatAvg(NonNullDoubles(candidates, c => c.CMRAOverallPercentile)),
            AvgTyping = FormatAvg(NonNullDoubles(candidates, c => c.TypingOverallPercentile)),
            AvgCAST = FormatAvg(NonNullDoubles(candidates, c => c.CASTOverallPercentile))
        };
    }

    // ── CSV Export ─────────────────────────────────────────────────────────────

    private void btnExportCsv_Click(object sender, EventArgs e)
    {
        string tabName;
        string csvContent;

        switch (tabMain.SelectedIndex)
        {
            case 0:
                tabName = "ByPathway";
                csvContent = BuildCsvPathway();
                break;
            case 1:
                tabName = "ByCertification";
                csvContent = BuildCsvCertification();
                break;
            default:
                tabName = "PassVsFail";
                csvContent = BuildCsvPassFail();
                break;
        }

        using var dialog = new SaveFileDialog
        {
            Title = "Export Analytics",
            Filter = "CSV Files (*.csv)|*.csv",
            FileName = $"Analytics_{tabName}_{DateTime.Today:yyyy-MM-dd}.csv"
        };

        if (dialog.ShowDialog() != DialogResult.OK) return;

        try
        {
            File.WriteAllText(dialog.FileName, csvContent, Encoding.UTF8);
            MessageBox.Show($"Exported successfully to:\n{dialog.FileName}", "Export Complete");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Export failed: {ex.Message}", "Export Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private string BuildCsvPathway()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Career Pathway,# Candidates,Avg CCAT %ile,Avg CBST %ile," +
                      $"Avg Composite (pass >= {PassThreshold}),Successful Placements,Score Pass Rate");
        foreach (var r in _pathwayRows)
            sb.AppendLine($"{CsvEscape(r.Pathway)},{r.Candidates},{r.AvgCCAT},{r.AvgCBST}," +
                          $"{r.AvgComposite},{r.SuccessfulPlacements},{r.ScorePassRate}");
        return sb.ToString();
    }

    private string BuildCsvCertification()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Certification,Total,Pursuing,Passed,Failed,Exam Pass Rate");
        foreach (var r in _certRows)
            sb.AppendLine($"{CsvEscape(r.CertName)},{r.Total},{r.Pursuing},{r.Passed},{r.Failed},{r.PassRate}");
        return sb.ToString();
    }

    private string BuildCsvPassFail()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Group,# Candidates,Avg Composite,Avg CCAT %ile,Avg CBST %ile," +
                      "Avg CMRA %ile,Avg Typing %ile,Avg CAST %ile");
        foreach (var r in _passFailRows)
            sb.AppendLine($"{CsvEscape(r.Group)},{r.Count},{r.AvgComposite},{r.AvgCCAT}," +
                          $"{r.AvgCBST},{r.AvgCMRA},{r.AvgTyping},{r.AvgCAST}");
        return sb.ToString();
    }

    // ── Helpers ────────────────────────────────────────────────────────────────

    // Mean of the five available overall-percentile scores; null when none are recorded.
    private static double? ComputeCompositeScore(Candidate c)
    {
        var scores = new int?[]
        {
            c.CCATOverallPercentile,
            c.CBSTOverallPercentile,
            c.CMRAOverallPercentile,
            c.TypingOverallPercentile,
            c.CASTOverallPercentile
        }
        .Where(s => s.HasValue)
        .Select(s => (double)s!.Value)
        .ToList();

        return scores.Count > 0 ? scores.Average() : null;
    }

    private static List<double> NonNullDoubles(List<Candidate> candidates, Func<Candidate, int?> selector)
        => candidates.Where(c => selector(c).HasValue).Select(c => (double)selector(c)!.Value).ToList();

    private static string FormatAvg(List<double> values)
        => values.Count > 0 ? $"{values.Average():F1}" : "—";

    private static void SetColHeader(DataGridView dgv, string colName, string header)
    {
        if (dgv.Columns.Contains(colName))
            dgv.Columns[colName]!.HeaderText = header;
    }

    private static string CsvEscape(string value)
    {
        if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
            return $"\"{value.Replace("\"", "\"\"")}\"";
        return value;
    }

    // ── Display row models ─────────────────────────────────────────────────────

    private class PathwayRow
    {
        public string Pathway { get; set; } = string.Empty;
        public int Candidates { get; set; }
        public string AvgCCAT { get; set; } = string.Empty;
        public string AvgCBST { get; set; } = string.Empty;
        public string AvgComposite { get; set; } = string.Empty;
        public int SuccessfulPlacements { get; set; }
        public string ScorePassRate { get; set; } = string.Empty;
    }

    private class CertRow
    {
        public string CertName { get; set; } = string.Empty;
        public int Total { get; set; }
        public int Pursuing { get; set; }
        public int Passed { get; set; }
        public int Failed { get; set; }
        public string PassRate { get; set; } = string.Empty;
    }

    private class PassFailRow
    {
        public string Group { get; set; } = string.Empty;
        public int Count { get; set; }
        public string AvgComposite { get; set; } = string.Empty;
        public string AvgCCAT { get; set; } = string.Empty;
        public string AvgCBST { get; set; } = string.Empty;
        public string AvgCMRA { get; set; } = string.Empty;
        public string AvgTyping { get; set; } = string.Empty;
        public string AvgCAST { get; set; } = string.Empty;
    }
}
