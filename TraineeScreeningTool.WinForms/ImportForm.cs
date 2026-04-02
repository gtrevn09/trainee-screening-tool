using System.Globalization;
using TraineeScreeningTool.WinForms.Data;
using TraineeScreeningTool.WinForms.Models;

namespace TraineeScreeningTool.WinForms;

public partial class ImportForm : Form
{
    // Stores the logged in username for logging purposes
    private readonly string _username;

    // Constructor - accepts the logged in username from MainForm
    public ImportForm(string username)
    {
        InitializeComponent();
        _username = username; // Save username for logging
    }

    // Opens a file browser to select a CSV file
    private void btnBrowse_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog();
        dialog.Filter = "CSV Files (*.csv)|*.csv";
        dialog.Title = "Select Assessment CSV File";

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            txtFilePath.Text = dialog.FileName;
        }
    }

    // Imports the selected CSV file into the database
    private void btnImport_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtFilePath.Text))
        {
            MessageBox.Show("Please select a CSV file first.", "Validation Error");
            return;
        }

        if (!File.Exists(txtFilePath.Text))
        {
            MessageBox.Show("File not found. Please select a valid CSV file.", "Error");
            return;
        }

        int imported = 0;
        int skipped = 0;
        int errors = 0;
        var errorMessages = new List<string>();

        try
        {
            using var context = new ApplicationDbContext();
            var lines = File.ReadAllLines(txtFilePath.Text);

            if (lines.Length < 2)
            {
                MessageBox.Show("CSV file is empty or has no data rows.", "Error");
                return;
            }

            var headers = ParseCsvLine(lines[0]);

            int idxFirstName = FindColumn(headers, "First Name");
            int idxLastName = FindColumn(headers, "Last Name");
            int idxEmail = FindColumn(headers, "Email Address");
            int idxStatus = FindColumn(headers, "Event Status");
            int idxTalent = FindColumn(headers, "Talent Signal");
            int idxCCATRaw = FindColumn(headers, "CCAT Overall Raw Score");
            int idxCCATMath = FindColumn(headers, "CCAT Math & Logic Percentile");
            int idxCCATVerbal = FindColumn(headers, "CCAT Verbal Ability Percentile");
            int idxCCATSpatial = FindColumn(headers, "CCAT Spatial Reasoning Percentile");
            int idxCCATPercentile = FindColumn(headers, "CCAT Overall Percentile");
            int idxCBSTRaw = FindColumn(headers, "CBST Overall Raw Score");
            int idxCBSTMath = FindColumn(headers, "CBST Math Raw Score");
            int idxCBSTVerbal = FindColumn(headers, "CBST Verbal Raw Score");
            int idxCBSTPercentile = FindColumn(headers, "CBST Overall Percentile");
            int idxCMRA = FindColumn(headers, "CMRA Overall Percentile");
            int idxTypingWPM = FindColumn(headers, "TT Adjusted Words Per Minute");
            int idxTypingErrors = FindColumn(headers, "TT Number of Errors");
            int idxTypingPercentile = FindColumn(headers, "TT Overall Percentile");
            int idxWordRaw = FindColumn(headers, "Word 365 Overall Raw Score");
            int idxWordProf = FindColumn(headers, "Word 365 Proficiency");
            int idxExcelRaw = FindColumn(headers, "Excel 365 Overall Raw Score");
            int idxExcelProf = FindColumn(headers, "Excel 365 Overall Proficiency");
            int idxCASTPercentile = FindColumn(headers, "CAST Overall Percentile");
            int idxCASTDivided = FindColumn(headers, "CAST Divided Attention Percentile");
            int idxCASTFiltering = FindColumn(headers, "CAST Filtering Percentile");
            int idxCASTReaction = FindColumn(headers, "CAST Perceptual Reaction Time Percentile");
            int idxCASTVigilance = FindColumn(headers, "CAST Vigilance Percentile");
            int idxCSAPRec = FindColumn(headers, "CSAP Overall Recommendation");
            int idxCSAPAchieve = FindColumn(headers, "CSAP Achievement Raw Score");
            int idxCSAPAssert = FindColumn(headers, "CSAP Assertiveness Raw Score");
            int idxCSAPCoop = FindColumn(headers, "CSAP Cooperativeness Raw Score");
            int idxCSAPGoal = FindColumn(headers, "CSAP Goal Orientation Raw Score");
            int idxCSAPMotiv = FindColumn(headers, "CSAP Motivation Raw Score");
            int idxCSAPTeam = FindColumn(headers, "CSAP Team Player Raw Score");

            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    var cols = ParseCsvLine(lines[i]);

                    if (idxStatus >= 0 && GetValue(cols, idxStatus) != "Complete")
                    {
                        skipped++;
                        continue;
                    }

                    var firstName = GetValue(cols, idxFirstName);
                    var lastName = GetValue(cols, idxLastName);
                    var email = GetValue(cols, idxEmail);

                    if (string.IsNullOrWhiteSpace(firstName) ||
                        string.IsNullOrWhiteSpace(lastName) ||
                        string.IsNullOrWhiteSpace(email))
                    {
                        skipped++;
                        errorMessages.Add($"Row {i + 1}: Missing required fields - skipped");
                        continue;
                    }

                    var existing = context.Candidates
                        .FirstOrDefault(c => c.Email == email);

                    var candidate = existing ?? new Candidate();

                    candidate.FirstName = firstName;
                    candidate.LastName = lastName;
                    candidate.Email = email;
                    candidate.TalentSignal = ParseInt(cols, idxTalent);
                    candidate.CCATRawScore = ParseInt(cols, idxCCATRaw);
                    candidate.CCATMathPercentile = ParseInt(cols, idxCCATMath);
                    candidate.CCATVerbalPercentile = ParseInt(cols, idxCCATVerbal);
                    candidate.CCATSpatialPercentile = ParseInt(cols, idxCCATSpatial);
                    candidate.CCATOverallPercentile = ParseInt(cols, idxCCATPercentile);
                    candidate.CBSTRawScore = ParseInt(cols, idxCBSTRaw);
                    candidate.CBSTMathRaw = ParseInt(cols, idxCBSTMath);
                    candidate.CBSTVerbalRaw = ParseInt(cols, idxCBSTVerbal);
                    candidate.CBSTOverallPercentile = ParseInt(cols, idxCBSTPercentile);
                    candidate.CMRAOverallPercentile = ParseInt(cols, idxCMRA);
                    candidate.TypingWordsPerMinute = ParseInt(cols, idxTypingWPM);
                    candidate.TypingErrors = ParseInt(cols, idxTypingErrors);
                    candidate.TypingOverallPercentile = ParseInt(cols, idxTypingPercentile);
                    candidate.WordRawScore = ParseInt(cols, idxWordRaw);
                    candidate.WordProficiency = GetValue(cols, idxWordProf);
                    candidate.ExcelRawScore = ParseInt(cols, idxExcelRaw);
                    candidate.ExcelProficiency = GetValue(cols, idxExcelProf);
                    candidate.CASTOverallPercentile = ParseInt(cols, idxCASTPercentile);
                    candidate.CASTDividedAttention = ParseInt(cols, idxCASTDivided);
                    candidate.CASTFiltering = ParseInt(cols, idxCASTFiltering);
                    candidate.CASTReactionTime = ParseInt(cols, idxCASTReaction);
                    candidate.CASTVigilance = ParseInt(cols, idxCASTVigilance);
                    candidate.CSAPRecommendation = GetValue(cols, idxCSAPRec);
                    candidate.CSAPAchievement = ParseInt(cols, idxCSAPAchieve);
                    candidate.CSAPAssertiveness = ParseInt(cols, idxCSAPAssert);
                    candidate.CSAPCooperativeness = ParseInt(cols, idxCSAPCoop);
                    candidate.CSAPGoalOrientation = ParseInt(cols, idxCSAPGoal);
                    candidate.CSAPMotivation = ParseInt(cols, idxCSAPMotiv);
                    candidate.CSAPTeamPlayer = ParseInt(cols, idxCSAPTeam);

                    if (existing == null)
                        context.Candidates.Add(candidate);

                    imported++;
                }
                catch (Exception ex)
                {
                    errors++;
                    errorMessages.Add($"Row {i + 1}: {ex.Message}");
                }
            }

            context.SaveChanges();

            // Log the import action
            context.Log(_username, "Import CSV",
                $"Imported {imported} candidates, skipped {skipped}, errors {errors} from file: {Path.GetFileName(txtFilePath.Text)}");
        }
        catch (Exception ex)
        {
            using var context = new ApplicationDbContext();
            context.Log(_username, "Import CSV Failed", ex.Message, false);
            MessageBox.Show($"Failed to read file: {ex.Message}", "Error");
            return;
        }

        var summary = $"Import Complete!\n\n" +
                      $"Successfully imported: {imported}\n" +
                      $"Skipped (incomplete): {skipped}\n" +
                      $"Errors: {errors}";

        if (errorMessages.Count > 0)
        {
            summary += "\n\nRow Errors:\n" + string.Join("\n", errorMessages.Take(10));
            if (errorMessages.Count > 10)
                summary += $"\n...and {errorMessages.Count - 10} more errors";
        }

        MessageBox.Show(summary, "Import Results");
        this.Close();
    }

    // Parses a CSV line handling quoted fields with commas inside
    private List<string> ParseCsvLine(string line)
    {
        var result = new List<string>();
        bool inQuotes = false;
        var current = new System.Text.StringBuilder();

        foreach (char c in line)
        {
            if (c == '"')
                inQuotes = !inQuotes;
            else if (c == ',' && !inQuotes)
            {
                result.Add(current.ToString().Trim());
                current.Clear();
            }
            else
                current.Append(c);
        }

        result.Add(current.ToString().Trim());
        return result;
    }

    // Finds the index of a column by name in the header row
    private int FindColumn(List<string> headers, string name)
    {
        return headers.FindIndex(h =>
            h.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    // Safely gets a value from a row by column index
    private string GetValue(List<string> cols, int index)
    {
        if (index < 0 || index >= cols.Count) return string.Empty;
        return cols[index].Trim('"').Trim();
    }

    // Safely parses an integer from a row by column index
    private int? ParseInt(List<string> cols, int index)
    {
        var val = GetValue(cols, index);
        if (int.TryParse(val, out int result)) return result;
        return null;
    }
}