using Microsoft.EntityFrameworkCore;
using TraineeScreeningTool.WinForms.Data;
using TraineeScreeningTool.WinForms.Models;
using System.Linq;

namespace TraineeScreeningTool.WinForms;

public partial class MainForm : Form
{
    private readonly string _username;
    private bool _isDragging = false;
    private bool _dragCheckValue = true;
    private bool _allSelected = false;

    public MainForm(string username)
    {
        InitializeComponent();
        _username = username;
        LoadCandidates();
        LoadPlacementNotification();

        dataGridView1.MouseDown += dataGridView1_MouseDown;
        dataGridView1.MouseMove += dataGridView1_MouseMove;
        dataGridView1.MouseUp += dataGridView1_MouseUp;

        dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
        dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;
        dataGridView1.ColumnHeaderMouseClick += dataGridView1_ColumnHeaderMouseClick;
    }

    private void LoadPlacementNotification()
    {
        using var context = new ApplicationDbContext();
        var allPlacements = context.JobPlacements.AsEnumerable().ToList();
        int reviewCount = allPlacements.Count(p => p.NeedsReview);

        if (reviewCount > 0)
        {
            lblNotification.Text =
                $"⚠  {reviewCount} job placement{(reviewCount == 1 ? "" : "s")} " +
                $"need{(reviewCount == 1 ? "s" : "")} review (approaching or past 6 months) — Click here to view";
            pnlNotification.Visible = true;
        }
        else
        {
            pnlNotification.Visible = false;
        }
    }

    private void pnlNotification_Click(object sender, EventArgs e)
    {
        var form = new JobPlacementsForm(_username);
        form.ShowDialog();
        LoadPlacementNotification();
    }

    private void LoadCandidates()
    {
        _allSelected = false;
        using var context = new ApplicationDbContext();
        dataGridView1.DataSource = context.Candidates.ToList();
        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        if (!dataGridView1.Columns.Contains("Select"))
        {
            var checkboxCol = new DataGridViewCheckBoxColumn();
            checkboxCol.Name = "Select";
            checkboxCol.HeaderText = "☐ All";
            checkboxCol.Width = 50;
            checkboxCol.DisplayIndex = 0;
            dataGridView1.Columns.Insert(0, checkboxCol);
        }

        RenameColumns();
    }

    private void RenameColumns()
    {
        RenameColumn("FirstName", "First Name");
        RenameColumn("LastName", "Last Name");
        RenameColumn("Email", "Email");
        RenameColumn("TestDate", "Test Date");
        RenameColumn("TalentSignal", "Talent Signal");
        RenameColumn("CCATRawScore", "CCAT Raw");
        RenameColumn("CCATOverallPercentile", "CCAT %");
        RenameColumn("CCATMathPercentile", "CCAT Math %");
        RenameColumn("CCATVerbalPercentile", "CCAT Verbal %");
        RenameColumn("CCATSpatialPercentile", "CCAT Spatial %");
        RenameColumn("CMRAOverallPercentile", "CMRA %");
        RenameColumn("CBSTRawScore", "CBST Raw");
        RenameColumn("CBSTOverallPercentile", "CBST %");
        RenameColumn("CBSTMathRaw", "CBST Math");
        RenameColumn("CBSTVerbalRaw", "CBST Verbal");
        RenameColumn("CLIKRawScore", "CLIK Raw");
        RenameColumn("CLIKProficiency", "CLIK Proficiency");
        RenameColumn("TypingWordsPerMinute", "Typing WPM");
        RenameColumn("TypingErrors", "Typing Errors");
        RenameColumn("TypingOverallPercentile", "Typing %");
        RenameColumn("CASTOverallPercentile", "CAST %");
        RenameColumn("CASTDividedAttention", "CAST Divided");
        RenameColumn("CASTFiltering", "CAST Filtering");
        RenameColumn("CASTReactionTime", "CAST Reaction");
        RenameColumn("CASTVigilance", "CAST Vigilance");
        RenameColumn("WordRawScore", "Word Raw");
        RenameColumn("WordProficiency", "Word Proficiency");
        RenameColumn("ExcelRawScore", "Excel Raw");
        RenameColumn("ExcelProficiency", "Excel Proficiency");
        RenameColumn("CSAPRecommendation", "CSAP Recommendation");
        RenameColumn("CSAPAchievement", "CSAP Achievement");
        RenameColumn("CSAPAssertiveness", "CSAP Assertiveness");
        RenameColumn("CSAPCooperativeness", "CSAP Cooperativeness");
        RenameColumn("CSAPGoalOrientation", "CSAP Goal");
        RenameColumn("CSAPMotivation", "CSAP Motivation");
        RenameColumn("CSAPTeamPlayer", "CSAP Team Player");
        RenameColumn("Recommendation", "Pathway Recommendation");
        RenameColumn("ReadinessRating", "Readiness");
        RenameColumn("Explanation", "Explanation");
        RenameColumn("FullName", "Full Name");
    }

    private void RenameColumn(string fieldName, string displayName)
    {
        if (dataGridView1.Columns.Contains(fieldName))
            dataGridView1.Columns[fieldName].HeaderText = displayName;
    }

    private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (dataGridView1.Columns[e.ColumnIndex].Name == "Select") return;

        try
        {
            var idCell = dataGridView1.Rows[e.RowIndex].Cells["Id"];
            if (idCell.Value == null) return;
            int id = (int)idCell.Value;

            var colName = dataGridView1.Columns[e.ColumnIndex].Name;
            var newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            using var context = new ApplicationDbContext();
            var candidate = context.Candidates.Find(id);
            if (candidate == null) return;

            var prop = typeof(Candidate).GetProperty(colName);
            if (prop != null)
            {
                if (newValue == null || newValue is DBNull)
                    prop.SetValue(candidate, null);
                else if (prop.PropertyType == typeof(string))
                    prop.SetValue(candidate, newValue.ToString());
                else if (prop.PropertyType == typeof(int?))
                    prop.SetValue(candidate, int.TryParse(newValue.ToString(), out int i) ? i : (int?)null);
                else if (prop.PropertyType == typeof(int))
                    prop.SetValue(candidate, int.TryParse(newValue.ToString(), out int i2) ? i2 : 0);

                context.SaveChanges();
            }
        }
        catch { }
    }

    private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
        if (dataGridView1.IsCurrentCellDirty)
            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        var search = txtSearch.Text.Trim().ToLower();

        using var context = new ApplicationDbContext();

        var results = context.Candidates
            .Where(c =>
                c.FirstName.ToLower().Contains(search) ||
                c.LastName.ToLower().Contains(search) ||
                c.Email.ToLower().Contains(search))
            .ToList();

        dataGridView1.DataSource = results;
        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        if (!dataGridView1.Columns.Contains("Select"))
        {
            var checkboxCol = new DataGridViewCheckBoxColumn();
            checkboxCol.Name = "Select";
            checkboxCol.HeaderText = "Select";
            checkboxCol.Width = 50;
            checkboxCol.DisplayIndex = 0;
            dataGridView1.Columns.Insert(0, checkboxCol);
        }

        RenameColumns();
    }

    private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
    {
        if ((ModifierKeys & Keys.Shift) == 0) return;

        var hitInfo = dataGridView1.HitTest(e.X, e.Y);
        if (hitInfo.RowIndex < 0) return;

        var row = dataGridView1.Rows[hitInfo.RowIndex];
        var currentValue = row.Cells["Select"].Value as bool? ?? false;

        _isDragging = true;
        _dragCheckValue = !currentValue;
        row.Cells["Select"].Value = _dragCheckValue;
    }

    private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_isDragging) return;

        var hitInfo = dataGridView1.HitTest(e.X, e.Y);
        if (hitInfo.RowIndex < 0) return;

        dataGridView1.Rows[hitInfo.RowIndex].Cells["Select"].Value = _dragCheckValue;
    }

    private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
    {
        _isDragging = false;
    }

    private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (dataGridView1.Columns[e.ColumnIndex].Name != "Select") return;

        _allSelected = !_allSelected;

        foreach (DataGridViewRow row in dataGridView1.Rows)
            row.Cells["Select"].Value = _allSelected;

        dataGridView1.RefreshEdit();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        int margin = 12;
        int btnRow1Y = ClientSize.Height - 80;
        int btnRow2Y = ClientSize.Height - 40;
        int notificationY = margin + 32;

        txtSearch.Location = new Point(margin, margin);
        txtSearch.Size = new Size(ClientSize.Width - margin * 2, 25);

        pnlNotification.Location = new Point(margin, notificationY);
        pnlNotification.Size = new Size(ClientSize.Width - margin * 2, 28);

        dataGridView1.Location = new Point(margin, notificationY + 34);
        dataGridView1.Size = new Size(
            ClientSize.Width - margin * 2,
            ClientSize.Height - 155);

        btnAddCandidate.Location = new Point(margin, btnRow1Y);
        btnImport.Location = new Point(margin + 96, btnRow1Y);
        btnImportPdf.Location = new Point(margin + 222, btnRow1Y);
        btnViewPdf.Location = new Point(margin + 353, btnRow1Y);
        btnAssess.Location = new Point(margin + 469, btnRow1Y);
        btnDelete.Location = new Point(margin + 565, btnRow1Y);

        btnUserDetails.Location = new Point(margin, btnRow2Y);
        btnLogout.Location = new Point(margin + 126, btnRow2Y);
        btnViewLogs.Location = new Point(margin + 232, btnRow2Y);
        btnAnalytics.Location = new Point(margin + 338, btnRow2Y);
        btnProfile.Location = new Point(margin + 445, btnRow2Y);
        btnUserGuide.Location = new Point(margin + 621, btnRow2Y);
    }

    private void btnAddCandidate_Click(object sender, EventArgs e)
    {
        var form = new AddCandidateForm(_username);
        form.ShowDialog();
        LoadCandidates();
    }

    private void btnImport_Click(object sender, EventArgs e)
    {
        var form = new ImportForm(_username);
        form.ShowDialog();
        LoadCandidates();
    }

    private void btnImportPdf_Click(object sender, EventArgs e)
    {
        var form = new PdfImportForm(_username);
        form.ShowDialog();
        LoadCandidates();
    }

    private void btnViewPdf_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count == 0)
        {
            MessageBox.Show("Please select a candidate first.", "No Selection");
            return;
        }

        int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;

        using var context = new ApplicationDbContext();
        var candidate = context.Candidates.Find(id);

        if (candidate == null)
        {
            MessageBox.Show("Candidate not found.", "Error");
            return;
        }

        if (string.IsNullOrEmpty(candidate.PdfFolderPath) || !Directory.Exists(candidate.PdfFolderPath))
        {
            MessageBox.Show(
                "No PDF folder is linked to this candidate.\n\n" +
                "PDFs are linked automatically when you use 'Import PDFs'. " +
                "If the folder was moved or deleted, please re-import the PDFs.",
                "No PDFs Found");
            return;
        }

        var pdfFiles = Directory.GetFiles(candidate.PdfFolderPath, "*.pdf")
            .Where(f => Path.GetFileNameWithoutExtension(f)
                .StartsWith($"{candidate.FirstName}-{candidate.LastName}-",
                    StringComparison.OrdinalIgnoreCase))
            .OrderBy(f => f)
            .ToList();

        if (pdfFiles.Count == 0)
        {
            pdfFiles = Directory.GetFiles(candidate.PdfFolderPath, "*.pdf")
                .Where(f => Path.GetFileNameWithoutExtension(f)
                    .Contains(candidate.FirstName, StringComparison.OrdinalIgnoreCase))
                .OrderBy(f => f)
                .ToList();
        }

        if (pdfFiles.Count == 0)
        {
            var result = MessageBox.Show(
                $"No PDF files matching '{candidate.FullName}' were found in:\n{candidate.PdfFolderPath}\n\n" +
                "Would you like to open the folder instead?",
                "PDFs Not Found",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                System.Diagnostics.Process.Start("explorer.exe", candidate.PdfFolderPath);

            return;
        }

        if (pdfFiles.Count == 1)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = pdfFiles[0],
                UseShellExecute = true
            });
        }
        else
        {
            var form = new PdfSelectForm(candidate.FullName, pdfFiles);
            form.ShowDialog();
        }
    }

    private void btnAssess_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count == 0) return;
        int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
        var form = new AssessForm(id);
        form.ShowDialog();
        LoadCandidates();
    }

    private void btnProfile_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count == 0)
        {
            MessageBox.Show("Please select a candidate first.", "No Selection");
            return;
        }

        int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
        var form = new CandidateProfileForm(id, _username);
        form.ShowDialog();
        LoadCandidates();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        var checkedIds = new List<int>();

        foreach (DataGridViewRow row in dataGridView1.Rows)
        {
            var checkValue = row.Cells["Select"].Value;
            if (checkValue != null && (bool)checkValue)
                checkedIds.Add((int)row.Cells["Id"].Value);
        }

        if (checkedIds.Count == 0)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please check or select a candidate to delete.", "No Selection");
                return;
            }
            checkedIds.Add((int)dataGridView1.SelectedRows[0].Cells["Id"].Value);
        }

        var confirm = MessageBox.Show(
            $"Are you sure you want to delete {checkedIds.Count} candidate(s)?",
            "Confirm Delete",
            MessageBoxButtons.YesNo);

        if (confirm != DialogResult.Yes) return;

        using var context = new ApplicationDbContext();

        foreach (var id in checkedIds)
        {
            var candidate = context.Candidates.Find(id);
            if (candidate != null)
            {
                context.Log(_username, "Delete Candidate",
                    $"Deleted candidate: {candidate.FullName} ({candidate.Email})");
                context.Candidates.Remove(candidate);
            }
        }

        context.SaveChanges();
        LoadCandidates();
    }

    private void btnUserDetails_Click(object sender, EventArgs e)
    {
        var form = new UserDetailsForm(_username);
        form.ShowDialog();
    }

    private void btnViewLogs_Click(object sender, EventArgs e)
    {
        var form = new LogViewerForm();
        form.ShowDialog();
    }

    private void btnAnalytics_Click(object sender, EventArgs e)
    {
        var form = new AnalyticsForm(_username);
        form.ShowDialog();
    }

    private void btnUserGuide_Click(object sender, EventArgs e)
    {
        var form = new UserGuideForm();
        form.ShowDialog();
    }

    private void btnLogout_Click(object sender, EventArgs e)
    {
        var confirm = MessageBox.Show(
            "Are you sure you want to log out?",
            "Confirm Logout",
            MessageBoxButtons.YesNo);

        if (confirm != DialogResult.Yes) return;

        using var context = new ApplicationDbContext();
        context.Log(_username, "Logout", "User logged out");

        Application.Restart();
    }
}