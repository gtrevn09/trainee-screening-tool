using Microsoft.EntityFrameworkCore;
using TraineeScreeningTool.WinForms.Data;
using TraineeScreeningTool.WinForms.Models;
using System.Linq;

namespace TraineeScreeningTool.WinForms;

public partial class MainForm : Form
{
    // Stores the logged in username so we can pass it to other forms
    private readonly string _username;

    // Tracks whether the mouse is being held down for drag selection
    private bool _isDragging = false;

    // Tracks the check state to apply during drag
    private bool _dragCheckValue = true;

    // Constructor - accepts the logged in username from Program.cs
    public MainForm(string username)
    {
        InitializeComponent();
        _username = username;
        LoadCandidates();
        LoadPlacementNotification();

        // Wire up mouse events for drag-to-select
        dataGridView1.MouseDown += dataGridView1_MouseDown;
        dataGridView1.MouseMove += dataGridView1_MouseMove;
        dataGridView1.MouseUp += dataGridView1_MouseUp;

        // Wire up cell edit events to save changes to database
        dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
        dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;
    }

    // Checks for placements approaching or past 6 months and shows a notification banner
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

    // Clicking the notification banner opens the Job Placements form
    private void pnlNotification_Click(object sender, EventArgs e)
    {
        var form = new JobPlacementsForm(_username);
        form.ShowDialog();
        LoadPlacementNotification();
    }

    // Fetches all candidates from the database and displays them in the grid
    private void LoadCandidates()
    {
        using var context = new ApplicationDbContext();
        dataGridView1.DataSource = context.Candidates.ToList();
        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        // Add checkbox column if it doesn't already exist
        if (!dataGridView1.Columns.Contains("Select"))
        {
            var checkboxCol = new DataGridViewCheckBoxColumn();
            checkboxCol.Name = "Select";
            checkboxCol.HeaderText = "Select";
            checkboxCol.Width = 50;
            checkboxCol.DisplayIndex = 0;
            dataGridView1.Columns.Insert(0, checkboxCol);
        }

        // Rename columns to friendly display names
        RenameColumns();
    }

    // Renames all columns to friendly display names
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

    // Renames a grid column if it exists
    private void RenameColumn(string fieldName, string displayName)
    {
        if (dataGridView1.Columns.Contains(fieldName))
            dataGridView1.Columns[fieldName].HeaderText = displayName;
    }

    // Saves changes when a cell is edited directly in the grid
    private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        // Ignore header row and checkbox column
        if (e.RowIndex < 0) return;
        if (dataGridView1.Columns[e.ColumnIndex].Name == "Select") return;

        try
        {
            // Get the ID of the edited row
            var idCell = dataGridView1.Rows[e.RowIndex].Cells["Id"];
            if (idCell.Value == null) return;
            int id = (int)idCell.Value;

            // Get the column field name and new value
            var colName = dataGridView1.Columns[e.ColumnIndex].Name;
            var newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            using var context = new ApplicationDbContext();
            var candidate = context.Candidates.Find(id);
            if (candidate == null) return;

            // Use reflection to update the correct property
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
        catch { } // Silently ignore conversion errors
    }

    // Required to trigger CellValueChanged immediately after editing
    private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
        if (dataGridView1.IsCurrentCellDirty)
            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
    }

    // Filters candidates as the user types in the search box
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

        // Re-add checkbox column after filtering
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

    // Starts drag selection when mouse is held down with Shift
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

    // Applies check state to each row the mouse moves over while dragging
    private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_isDragging) return;

        var hitInfo = dataGridView1.HitTest(e.X, e.Y);
        if (hitInfo.RowIndex < 0) return;

        dataGridView1.Rows[hitInfo.RowIndex].Cells["Select"].Value = _dragCheckValue;
    }

    // Stops drag selection when mouse is released
    private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
    {
        _isDragging = false;
    }

    // Resizes controls when the form is resized or maximized
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        int margin = 12;
        int btnRow1Y = ClientSize.Height - 80;
        int btnRow2Y = ClientSize.Height - 40;
        int notificationY = margin + 32;
        int gridY = notificationY + (pnlNotification.Visible ? 34 : 4);

        txtSearch.Location = new Point(margin, margin);
        txtSearch.Size = new Size(ClientSize.Width - margin * 2, 25);

        pnlNotification.Location = new Point(margin, notificationY);
        pnlNotification.Size = new Size(ClientSize.Width - margin * 2, 28);

        dataGridView1.Location = new Point(margin, notificationY + 34);
        dataGridView1.Size = new Size(
            ClientSize.Width - margin * 2,
            ClientSize.Height - 155);

        // Row 1 buttons
        btnAddCandidate.Location = new Point(margin, btnRow1Y);
        btnImport.Location = new Point(margin + 120, btnRow1Y);
        btnImportPdf.Location = new Point(margin + 240, btnRow1Y);
        btnAssess.Location = new Point(margin + 360, btnRow1Y);
        btnDetails.Location = new Point(margin + 470, btnRow1Y);
        btnDelete.Location = new Point(margin + 580, btnRow1Y);

        // Row 2 buttons
        btnPlacements.Location = new Point(margin, btnRow2Y);
        btnUserDetails.Location = new Point(margin + 140, btnRow2Y);
        btnLogout.Location = new Point(margin + 280, btnRow2Y);
        btnViewLogs.Location = new Point(margin + 420, btnRow2Y);
        btnAnalytics.Location = new Point(margin + 560, btnRow2Y);
    }

    // Opens the Add Candidate form
    private void btnAddCandidate_Click(object sender, EventArgs e)
    {
        var form = new AddCandidateForm(_username);
        form.ShowDialog();
        LoadCandidates();
    }

    // Opens the Import CSV form
    private void btnImport_Click(object sender, EventArgs e)
    {
        var form = new ImportForm(_username);
        form.ShowDialog();
        LoadCandidates();
    }

    // Opens the Import PDFs form
    private void btnImportPdf_Click(object sender, EventArgs e)
    {
        var form = new PdfImportForm(_username);
        form.ShowDialog();
        LoadCandidates();
    }

    // Opens the Assessment form for the selected candidate
    private void btnAssess_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count == 0) return;
        int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
        var form = new AssessForm(id);
        form.ShowDialog();
        LoadCandidates();
    }

    // Opens the Details form for the selected candidate
    private void btnDetails_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count == 0) return;
        int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
        var form = new DetailsForm(id, _username);
        form.ShowDialog();
    }

    // Deletes all checked candidates
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

    // Opens the Job Placements form, pre-loading the selected candidate if one is highlighted
    private void btnPlacements_Click(object sender, EventArgs e)
    {
        int? selectedId = null;
        if (dataGridView1.SelectedRows.Count > 0)
            selectedId = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;

        var form = new JobPlacementsForm(_username, selectedId);
        form.ShowDialog();
        LoadPlacementNotification();
    }

    // Opens the User Details form
    private void btnUserDetails_Click(object sender, EventArgs e)
    {
        var form = new UserDetailsForm(_username);
        form.ShowDialog();
    }

    // Opens the Log Viewer form
    private void btnViewLogs_Click(object sender, EventArgs e)
    {
        var form = new LogViewerForm();
        form.ShowDialog();
    }

    // Opens the Analytics form
    private void btnAnalytics_Click(object sender, EventArgs e)
    {
        var form = new AnalyticsForm(_username);
        form.ShowDialog();
    }

    // Logs the user out and returns to the login screen
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