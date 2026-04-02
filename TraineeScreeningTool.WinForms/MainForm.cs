using Microsoft.EntityFrameworkCore;
using TraineeScreeningTool.WinForms.Data;
using TraineeScreeningTool.WinForms.Models;

namespace TraineeScreeningTool.WinForms;

public partial class MainForm : Form
{
    // Stores the logged in username so we can pass it to other forms
    private readonly string _username;

    // Tracks whether the mouse is being held down for drag selection
    private bool _isDragging = false;

    // Tracks the check state to apply during drag (true = check, false = uncheck)
    private bool _dragCheckValue = true;

    // Constructor - accepts the logged in username from Program.cs
    public MainForm(string username)
    {
        InitializeComponent();
        _username = username;
        LoadCandidates();

        // Wire up mouse events for drag-to-select
        dataGridView1.MouseDown += dataGridView1_MouseDown;
        dataGridView1.MouseMove += dataGridView1_MouseMove;
        dataGridView1.MouseUp += dataGridView1_MouseUp;
    }

    // Fetches all candidates from the database and displays them in the grid
    private void LoadCandidates()
    {
        using var context = new ApplicationDbContext();
        var candidates = context.Candidates.ToList();

        dataGridView1.DataSource = candidates;
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
    }

    // Starts drag selection when mouse is held down on a checkbox cell
    private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
    {
        // Only activate drag when Shift is held
        if ((ModifierKeys & Keys.Shift) == 0) return;

        var hitInfo = dataGridView1.HitTest(e.X, e.Y);
        if (hitInfo.RowIndex < 0) return;

        var row = dataGridView1.Rows[hitInfo.RowIndex];
        var currentValue = row.Cells["Select"].Value as bool? ?? false;

        // Start dragging and toggle the opposite of current value
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

        // Apply the drag check value to the row being hovered over
        dataGridView1.Rows[hitInfo.RowIndex].Cells["Select"].Value = _dragCheckValue;
    }

    // Stops drag selection when mouse is released
    private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
    {
        _isDragging = false; // Stop dragging
    }

    // Resizes controls when the form is resized or maximized
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        int margin = 12;
        int btnRow1Y = ClientSize.Height - 80;
        int btnRow2Y = ClientSize.Height - 40;

        txtSearch.Location = new Point(margin, margin);
        txtSearch.Size = new Size(ClientSize.Width - margin * 2, 25);

        dataGridView1.Location = new Point(margin, margin + 35);
        dataGridView1.Size = new Size(
            ClientSize.Width - margin * 2,
            ClientSize.Height - 135);

        btnAddCandidate.Location = new Point(margin, btnRow1Y);
        btnImport.Location = new Point(margin + 140, btnRow1Y);
        btnAssess.Location = new Point(margin + 280, btnRow1Y);
        btnDetails.Location = new Point(margin + 420, btnRow1Y);
        btnDelete.Location = new Point(margin + 560, btnRow1Y);

        btnChangePassword.Location = new Point(margin, btnRow2Y);
        btnLogout.Location = new Point(margin + 170, btnRow2Y);
        btnViewLogs.Location = new Point(margin + 310, btnRow2Y);
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
        var form = new DetailsForm(id);
        form.ShowDialog();
    }

    // Deletes all checked candidates - works for single or multiple
    private void btnDelete_Click(object sender, EventArgs e)
    {
        // Collect IDs of all checked rows
        var checkedIds = new List<int>();

        foreach (DataGridViewRow row in dataGridView1.Rows)
        {
            var checkValue = row.Cells["Select"].Value;
            if (checkValue != null && (bool)checkValue)
            {
                checkedIds.Add((int)row.Cells["Id"].Value);
            }
        }

        // If nothing is checked fall back to the selected row
        if (checkedIds.Count == 0)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please check or select a candidate to delete.",
                    "No Selection");
                return;
            }
            checkedIds.Add((int)dataGridView1.SelectedRows[0].Cells["Id"].Value);
        }

        // Confirm deletion
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

    // Opens the Change Password form
    private void btnChangePassword_Click(object sender, EventArgs e)
    {
        var form = new ChangePasswordForm(_username);
        form.ShowDialog();
    }

    // Opens the Log Viewer form
    private void btnViewLogs_Click(object sender, EventArgs e)
    {
        var form = new LogViewerForm();
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