using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Text.RegularExpressions;
using TraineeScreeningTool.WinForms.Data;
using TraineeScreeningTool.WinForms.Models;

namespace TraineeScreeningTool.WinForms;

public partial class PdfImportForm : Form
{
    private readonly string _username;
    private string[] _selectedFiles = Array.Empty<string>();

    public PdfImportForm(string username)
    {
        InitializeComponent();
        _username = username;
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog();
        dialog.Title = "Select all PDF reports for a candidate";
        dialog.Filter = "PDF Files (*.pdf)|*.pdf";
        dialog.Multiselect = true;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            _selectedFiles = dialog.FileNames;
            txtFolderPath.Text = Path.GetDirectoryName(_selectedFiles[0]) ?? string.Empty;
            lblPdfCount.Text = $"Selected {_selectedFiles.Length} PDF file(s)";
        }
    }

    private void btnImport_Click(object sender, EventArgs e)
    {
        if (_selectedFiles.Length == 0)
        {
            MessageBox.Show("Please select PDF files first.", "Validation Error");
            return;
        }

        try
        {
            using var context = new ApplicationDbContext();

            var summaryPdf = _selectedFiles.FirstOrDefault(f =>
                Path.GetFileNameWithoutExtension(f).Contains("-Summary-"));

            if (summaryPdf == null)
            {
                MessageBox.Show("No Summary PDF found. Make sure you select all 10 PDFs.", "Error");
                return;
            }

            var candidate = ParseSummaryPdf(summaryPdf);
            if (candidate == null)
            {
                MessageBox.Show("Could not read the Summary PDF.", "Error");
                return;
            }

            var csapFile = _selectedFiles.FirstOrDefault(f =>
                Path.GetFileNameWithoutExtension(f).Contains("-CSAP-"));
            if (csapFile != null) ParseCsapPdf(csapFile, candidate);

            var existing = context.Candidates
                .FirstOrDefault(c => c.Email == candidate.Email);

            if (existing != null)
            {
                UpdateCandidate(existing, candidate);
                context.SaveChanges();
                context.Log(_username, "PDF Import",
                    $"Updated candidate {candidate.FirstName} {candidate.LastName} from PDFs");
                MessageBox.Show(
                    $"Updated existing record for {candidate.FirstName} {candidate.LastName}!",
                    "Import Complete");
            }
            else
            {
                context.Candidates.Add(candidate);
                context.SaveChanges();
                context.Log(_username, "PDF Import",
                    $"Imported new candidate {candidate.FirstName} {candidate.LastName} from PDFs");
                MessageBox.Show(
                    $"Successfully imported {candidate.FirstName} {candidate.LastName}!\n\n" +
                    $"Note: Email was set to a placeholder. Please update it in the candidate record.",
                    "Import Complete");
            }

            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Import failed: {ex.Message}\n\n{ex.StackTrace}", "Error");
        }
    }

    // Extracts all lines from all pages as one flat list
    private List<string> ExtractAllLines(string filePath)
    {
        var allLines = new List<string>();
        using var reader = new PdfReader(filePath);
        using var doc = new PdfDocument(reader);

        for (int i = 1; i <= doc.GetNumberOfPages(); i++)
        {
            var strategy = new SimpleTextExtractionStrategy();
            var text = PdfTextExtractor.GetTextFromPage(doc.GetPage(i), strategy);
            foreach (var line in text.Split('\n'))
            {
                var trimmed = line.Trim();
                if (!string.IsNullOrWhiteSpace(trimmed))
                    allLines.Add(trimmed);
            }
        }

        return allLines;
    }

    // Finds first index where line contains the search text
    private int FindLine(List<string> lines, string contains, int start = 0)
    {
        for (int i = start; i < lines.Count; i++)
            if (lines[i].Contains(contains)) return i;
        return -1;
    }

    // Finds first index where line exactly equals the search text
    private int FindExact(List<string> lines, string exact, int start = 0)
    {
        for (int i = start; i < lines.Count; i++)
            if (lines[i] == exact) return i;
        return -1;
    }

    private Candidate? ParseSummaryPdf(string filePath)
    {
        var lines = ExtractAllLines(filePath);
        var candidate = new Candidate();

        // Line 0: "Branson McLaughlin"
        if (lines.Count > 0)
        {
            var parts = lines[0].Split(' ');
            candidate.FirstName = parts[0];
            candidate.LastName = string.Join(" ", parts.Skip(1));
        }

        // Test date
        var dateLine = lines.FirstOrDefault(l => l.StartsWith("Test Date:"));
        if (dateLine != null)
            candidate.TestDate = dateLine.Replace("Test Date:", "").Trim();

        // Talent Signal - iText splits into two lines:
        // Line N: "10"
        // Line N+1: "Talent Signal"
        int tsIdx = FindExact(lines, "Talent Signal");
        if (tsIdx > 0 && int.TryParse(lines[tsIdx - 1], out int ts))
            candidate.TalentSignal = ts;

        // CCAT - iText splits header into two lines:
        // Line N: "CCAT"
        // Line N+1: "Criteria Cognitive Aptitude Test"
        // Line N+2: description
        // Line N+3: "9 3"
        int ccatIdx = FindExact(lines, "CCAT");
        if (ccatIdx >= 0 && ccatIdx + 3 < lines.Count)
        {
            var nums = lines[ccatIdx + 3].Split(' ')
                .Where(x => int.TryParse(x, out _))
                .Select(int.Parse).ToList();
            if (nums.Count >= 2)
            {
                candidate.CCATRawScore = nums[0];
                candidate.CCATOverallPercentile = nums[1];
            }
        }

        // CCAT sub scores - single numbers after bar chart lines
        int spatialIdx = FindLine(lines, "Spatial Reasoning Percentile", ccatIdx >= 0 ? ccatIdx : 0);
        if (spatialIdx >= 0)
        {
            var subs = new List<int>();
            for (int i = spatialIdx + 1; i < lines.Count && subs.Count < 3; i++)
                if (Regex.IsMatch(lines[i], @"^\d+$")) subs.Add(int.Parse(lines[i]));
            if (subs.Count >= 1) candidate.CCATSpatialPercentile = subs[0];
            if (subs.Count >= 2) candidate.CCATVerbalPercentile = subs[1];
            if (subs.Count >= 3) candidate.CCATMathPercentile = subs[2];
        }

        // CMRA - same split pattern
        // Line N: "CMRA"
        // Line N+1: "Criteria Mechanical Reasoning Assessment"
        // Line N+2: description
        // Line N+3: "7 5"
        int cmraIdx = FindExact(lines, "CMRA");
        if (cmraIdx >= 0 && cmraIdx + 3 < lines.Count)
        {
            var nums = lines[cmraIdx + 3].Split(' ')
                .Where(x => int.TryParse(x, out _))
                .Select(int.Parse).ToList();
            if (nums.Count >= 2)
                candidate.CMRAOverallPercentile = nums[1];
        }

        // CBST - same split pattern
        // Line N: "CBST"
        // Line N+1: "Criteria Basic Skills Test"
        // Line N+2: description
        // Line N+3: "9 2"
        int cbstIdx = FindExact(lines, "CBST");
        if (cbstIdx >= 0 && cbstIdx + 3 < lines.Count)
        {
            var nums = lines[cbstIdx + 3].Split(' ')
                .Where(x => int.TryParse(x, out _))
                .Select(int.Parse).ToList();
            if (nums.Count >= 2)
            {
                candidate.CBSTRawScore = nums[0];
                candidate.CBSTOverallPercentile = nums[1];
            }
        }

        // CBST sub scores
        int verbalRawIdx = FindExact(lines, "Verbal Raw Score", cbstIdx >= 0 ? cbstIdx : 0);
        if (verbalRawIdx >= 0)
        {
            var subs = new List<int>();
            for (int i = verbalRawIdx + 1; i < lines.Count && subs.Count < 2; i++)
                if (Regex.IsMatch(lines[i], @"^\d+$")) subs.Add(int.Parse(lines[i]));
            if (subs.Count >= 1) candidate.CBSTVerbalRaw = subs[0];
            if (subs.Count >= 2) candidate.CBSTMathRaw = subs[1];
        }

        // CLIK - proficiency and raw score
        int clikIdx = FindExact(lines, "CLIK");
        if (clikIdx >= 0)
        {
            // Proficiency within 6 lines of CLIK
            for (int i = clikIdx + 1; i < Math.Min(clikIdx + 7, lines.Count); i++)
            {
                if (lines[i].Contains("Proficient") ||
                    lines[i] == "Basic" || lines[i] == "Advanced")
                {
                    candidate.CLIKProficiency = lines[i];
                    break;
                }
            }

            // Raw score follows "Overall Score"
            int overallIdx = FindExact(lines, "Overall Score", clikIdx);
            if (overallIdx >= 0 && overallIdx + 1 < lines.Count)
                if (int.TryParse(lines[overallIdx + 1], out int raw))
                    candidate.CLIKRawScore = raw;
        }

        // Typing - numbers appear BEFORE their labels
        // Line 57: 42, Line 58: WPM
        // Line 59: 2,  Line 60: Errors
        // Line 63: 68, Line 64: Percentile
        int ttIdx = FindExact(lines, "TT");
        if (ttIdx >= 0)
        {
            int wpmIdx = FindExact(lines, "WPM", ttIdx);
            if (wpmIdx > 0 && int.TryParse(lines[wpmIdx - 1], out int wpm))
                candidate.TypingWordsPerMinute = wpm;

            int errIdx = FindExact(lines, "Errors", ttIdx);
            if (errIdx > 0 && int.TryParse(lines[errIdx - 1], out int err))
                candidate.TypingErrors = err;

            int pctIdx = FindExact(lines, "Percentile", ttIdx);
            if (pctIdx > 0 && int.TryParse(lines[pctIdx - 1], out int pct))
                candidate.TypingOverallPercentile = pct;
        }

        // CAST - overall percentile is "23rd"
        int castIdx = FindExact(lines, "CAST");
        if (castIdx >= 0)
        {
            for (int i = castIdx + 1; i < Math.Min(castIdx + 5, lines.Count); i++)
            {
                var m = Regex.Match(lines[i], @"^(\d+)(st|nd|rd|th)$");
                if (m.Success)
                {
                    candidate.CASTOverallPercentile = int.Parse(m.Groups[1].Value);
                    break;
                }
            }

            // Sub scores - number appears 2 lines after label
            int divIdx = FindExact(lines, "Divided Attention", castIdx);
            if (divIdx >= 0)
                for (int i = divIdx + 1; i < Math.Min(divIdx + 3, lines.Count); i++)
                    if (int.TryParse(lines[i], out int v)) { candidate.CASTDividedAttention = v; break; }

            int vigIdx = FindExact(lines, "Vigilance", castIdx);
            if (vigIdx >= 0)
                for (int i = vigIdx + 1; i < Math.Min(vigIdx + 3, lines.Count); i++)
                    if (int.TryParse(lines[i], out int v)) { candidate.CASTVigilance = v; break; }

            int filtIdx = FindExact(lines, "Filtering", castIdx);
            if (filtIdx >= 0)
                for (int i = filtIdx + 1; i < Math.Min(filtIdx + 3, lines.Count); i++)
                    if (int.TryParse(lines[i], out int v)) { candidate.CASTFiltering = v; break; }

            int reactIdx = FindExact(lines, "Reaction Time", castIdx);
            if (reactIdx >= 0)
                for (int i = reactIdx + 1; i < Math.Min(reactIdx + 3, lines.Count); i++)
                    if (int.TryParse(lines[i], out int v)) { candidate.CASTReactionTime = v; break; }
        }

        // Word365 - iText splits into separate lines:
        // Line N:   "WORD365"
        // Line N+1: "Word 365"
        // Line N+2: description
        // Line N+3: "Score Proficiency"
        // Line N+4: "1"    <- raw score
        // Line N+5: "Beginner" <- proficiency
        int wordIdx = FindExact(lines, "WORD365");
        if (wordIdx >= 0)
        {
            int spIdx = FindExact(lines, "Score Proficiency", wordIdx);
            if (spIdx >= 0 && spIdx + 2 < lines.Count)
            {
                if (int.TryParse(lines[spIdx + 1], out int ws))
                    candidate.WordRawScore = ws;
                candidate.WordProficiency = lines[spIdx + 2];
            }
        }

        // Excel365 - same split pattern
        int excelIdx = FindExact(lines, "EXCEL365");
        if (excelIdx >= 0)
        {
            int spIdx = FindExact(lines, "Score Proficiency", excelIdx);
            if (spIdx >= 0 && spIdx + 2 < lines.Count)
            {
                if (int.TryParse(lines[spIdx + 1], out int es))
                    candidate.ExcelRawScore = es;
                candidate.ExcelProficiency = lines[spIdx + 2];
            }
        }

        // CSAP recommendation from summary
        // Line 104: "CSAP"
        // Line 105: "Customer Service Aptitude Profile"
        // Line 106: description
        // Line 107: "Not Recommended"
        int csapSumIdx = FindExact(lines, "CSAP", 90); // start from line 90 to skip earlier CSAP mention
        if (csapSumIdx >= 0)
        {
            for (int i = csapSumIdx + 1; i < Math.Min(csapSumIdx + 5, lines.Count); i++)
            {
                if (lines[i] == "Not Recommended" ||
                    lines[i] == "Recommended" ||
                    lines[i].Contains("Highly Recommended"))
                {
                    candidate.CSAPRecommendation = lines[i];
                    break;
                }
            }
        }

        // Placeholder email from filename
        var filename = Path.GetFileNameWithoutExtension(filePath);
        var nameParts = filename.Split('-');
        candidate.Email = $"{nameParts[0].ToLower()}.{nameParts[1].ToLower()}@lifeworks.placeholder";

        return candidate;
    }

    private void ParseCsapPdf(string filePath, Candidate candidate)
    {
        var lines = ExtractAllLines(filePath);

        // Recommendation
        var recLine = lines.FirstOrDefault(l =>
            l == "Not Recommended" ||
            l == "Recommended" ||
            l.Contains("Highly Recommended"));
        if (recLine != null)
            candidate.CSAPRecommendation = recLine.Trim();

        int recIdx = -1;
        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i] == "Not Recommended" ||
                lines[i] == "Recommended" ||
                lines[i].Contains("Highly Recommended"))
            {
                recIdx = i;
                break;
            }
        }

        if (recIdx >= 0)
        {
            var scores = new List<int>();
            for (int i = recIdx + 1; i < lines.Count && scores.Count < 18; i++)
                if (int.TryParse(lines[i], out int s)) scores.Add(s);

            if (scores.Count >= 7) candidate.CSAPAchievement = scores[6];
            if (scores.Count >= 15) candidate.CSAPAssertiveness = scores[14];
            if (scores.Count >= 18) candidate.CSAPCooperativeness = scores[17];
            if (scores.Count >= 10) candidate.CSAPGoalOrientation = scores[9];
            if (scores.Count >= 9) candidate.CSAPMotivation = scores[8];
            if (scores.Count >= 13) candidate.CSAPTeamPlayer = scores[12];
        }
    }

    private void UpdateCandidate(Candidate existing, Candidate parsed)
    {
        existing.FirstName = parsed.FirstName;
        existing.LastName = parsed.LastName;
        existing.TestDate = parsed.TestDate;
        existing.TalentSignal = parsed.TalentSignal;
        existing.CCATRawScore = parsed.CCATRawScore;
        existing.CCATOverallPercentile = parsed.CCATOverallPercentile;
        existing.CCATMathPercentile = parsed.CCATMathPercentile;
        existing.CCATVerbalPercentile = parsed.CCATVerbalPercentile;
        existing.CCATSpatialPercentile = parsed.CCATSpatialPercentile;
        existing.CMRAOverallPercentile = parsed.CMRAOverallPercentile;
        existing.CBSTRawScore = parsed.CBSTRawScore;
        existing.CBSTOverallPercentile = parsed.CBSTOverallPercentile;
        existing.CBSTMathRaw = parsed.CBSTMathRaw;
        existing.CBSTVerbalRaw = parsed.CBSTVerbalRaw;
        existing.CLIKRawScore = parsed.CLIKRawScore;
        existing.CLIKProficiency = parsed.CLIKProficiency;
        existing.TypingWordsPerMinute = parsed.TypingWordsPerMinute;
        existing.TypingErrors = parsed.TypingErrors;
        existing.TypingOverallPercentile = parsed.TypingOverallPercentile;
        existing.CASTOverallPercentile = parsed.CASTOverallPercentile;
        existing.CASTDividedAttention = parsed.CASTDividedAttention;
        existing.CASTVigilance = parsed.CASTVigilance;
        existing.CASTFiltering = parsed.CASTFiltering;
        existing.CASTReactionTime = parsed.CASTReactionTime;
        existing.WordRawScore = parsed.WordRawScore;
        existing.WordProficiency = parsed.WordProficiency;
        existing.ExcelRawScore = parsed.ExcelRawScore;
        existing.ExcelProficiency = parsed.ExcelProficiency;
        existing.CSAPRecommendation = parsed.CSAPRecommendation;
        existing.CSAPAchievement = parsed.CSAPAchievement;
        existing.CSAPAssertiveness = parsed.CSAPAssertiveness;
        existing.CSAPCooperativeness = parsed.CSAPCooperativeness;
        existing.CSAPGoalOrientation = parsed.CSAPGoalOrientation;
        existing.CSAPMotivation = parsed.CSAPMotivation;
        existing.CSAPTeamPlayer = parsed.CSAPTeamPlayer;
    }
}