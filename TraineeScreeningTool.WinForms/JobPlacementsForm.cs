using Microsoft.EntityFrameworkCore;
using TraineeScreeningTool.WinForms.Data;
using TraineeScreeningTool.WinForms.Models;

namespace TraineeScreeningTool.WinForms;

public partial class JobPlacementsForm : Form
{
    private readonly string _username;
    private readonly int? _preselectedCandidateId;

    // Holds the display rows so we can read placement IDs for update/delete
    private List<PlacementRow> _rows = new();

    public JobPlacementsForm(string username, int? candidateId = null)
    {
        InitializeComponent();
        _username = username;
        _preselectedCandidateId = candidateId;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        LoadFilter();
        LoadPlacements();

        if (_preselectedCandidateId.HasValue)
        {
            var addForm = new AddPlacementForm(_username, _preselectedCandidateId.Value);
            if (addForm.ShowDialog() == DialogResult.OK)
                LoadPlacements();
        }
    }

    // Populates the filter dropdown with "All" + each pathway
    private void LoadFilter()
    {
        cmbFilter.Items.Clear();
        cmbFilter.Items.Add("All Pathways");
        foreach (var p in CareerPathways.All)
            cmbFilter.Items.Add(p);
        cmbFilter.SelectedIndex = 0;
    }

    // Queries the database and refreshes the grid
    private void LoadPlacements()
    {
        using var context = new ApplicationDbContext();

        var query = context.JobPlacements
            .Include(p => p.Candidate)
            .AsEnumerable();

        // Apply pathway filter
        if (cmbFilter.SelectedIndex > 0)
        {
            var selectedPathway = cmbFilter.SelectedItem!.ToString()!;
            query = query.Where(p => p.CareerPathway == selectedPathway);
        }

        // Apply "needs review" filter
        if (chkNeedsReview.Checked)
            query = query.Where(p => p.NeedsReview);

        _rows = query
            .OrderByDescending(p => p.NeedsReview)
            .ThenBy(p => p.PlacementDate)
            .Select(p => new PlacementRow
            {
                Id = p.Id,
                CandidateName = p.Candidate != null
                    ? p.Candidate.FirstName + " " + p.Candidate.LastName
                    : "(Unknown)",
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
        dgvPlacements.DataSource = _rows;

        // Hide the internal Id and NeedsReview/PastSixMonths helper columns
        if (dgvPlacements.Columns.Contains("Id")) dgvPlacements.Columns["Id"]!.Visible = false;
        if (dgvPlacements.Columns.Contains("NeedsReview")) dgvPlacements.Columns["NeedsReview"]!.Visible = false;
        if (dgvPlacements.Columns.Contains("PastSixMonths")) dgvPlacements.Columns["PastSixMonths"]!.Visible = false;

        // Friendly column headers
        SetHeader("CandidateName", "Candidate");
        SetHeader("CareerPathway", "Career Pathway");
        SetHeader("PlacementDate", "Placed On");
        SetHeader("ExitDate", "Exit Date");
        SetHeader("MonthsPlaced", "Months");
        SetHeader("Status", "Status");

        dgvPlacements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        // Color-code rows by urgency
        foreach (DataGridViewRow row in dgvPlacements.Rows)
        {
            if (row.DataBoundItem is PlacementRow pr)
            {
                if (pr.PastSixMonths)
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 150); // orange – past 6 months
                else if (pr.NeedsReview)
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 180); // yellow – approaching 6 months
            }
        }

        UpdateSummaryLabel();
    }

    private void SetHeader(string colName, string header)
    {
        if (dgvPlacements.Columns.Contains(colName))
            dgvPlacements.Columns[colName]!.HeaderText = header;
    }

    // Counts how many placements need review and updates the label
    private void UpdateSummaryLabel()
    {
        using var context = new ApplicationDbContext();
        var allPlacements = context.JobPlacements.AsEnumerable().ToList();
        int needsReview = allPlacements.Count(p => p.NeedsReview);
        int total = allPlacements.Count;

        lblSummary.Text = $"{total} total placement(s) — {needsReview} need review";
        lblSummary.ForeColor = needsReview > 0 ? Color.DarkOrange : Color.DimGray;
    }

    // Opens the Add Placement form
    private void btnAdd_Click(object sender, EventArgs e)
    {
        var form = new AddPlacementForm(_username);
        if (form.ShowDialog() == DialogResult.OK)
            LoadPlacements();
    }

    // Opens the Update Placement form for the selected row
    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (dgvPlacements.SelectedRows.Count == 0 || dgvPlacements.SelectedRows[0].DataBoundItem is not PlacementRow pr)
        {
            MessageBox.Show("Please select a placement to update.", "No Selection");
            return;
        }

        var form = new UpdatePlacementForm(_username, pr.Id);
        if (form.ShowDialog() == DialogResult.OK)
            LoadPlacements();
    }

    // Double-click a row to update it
    private void dgvPlacements_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (dgvPlacements.Rows[e.RowIndex].DataBoundItem is PlacementRow pr)
        {
            var form = new UpdatePlacementForm(_username, pr.Id);
            if (form.ShowDialog() == DialogResult.OK)
                LoadPlacements();
        }
    }

    // Deletes the selected placement after confirmation
    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (dgvPlacements.SelectedRows.Count == 0 || dgvPlacements.SelectedRows[0].DataBoundItem is not PlacementRow pr)
        {
            MessageBox.Show("Please select a placement to delete.", "No Selection");
            return;
        }

        var confirm = MessageBox.Show(
            $"Delete placement for {pr.CandidateName} in {pr.CareerPathway}?",
            "Confirm Delete",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (confirm != DialogResult.Yes) return;

        using var context = new ApplicationDbContext();
        var placement = context.JobPlacements.Find(pr.Id);
        if (placement != null)
        {
            context.JobPlacements.Remove(placement);
            context.SaveChanges();
            context.Log(_username, "Delete Placement",
                $"Deleted placement ID {pr.Id} for candidate ID {placement.CandidateId} in '{placement.CareerPathway}'");
        }

        LoadPlacements();
    }

    // Re-loads when the filter changes
    private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e) => LoadPlacements();
    private void chkNeedsReview_CheckedChanged(object sender, EventArgs e) => LoadPlacements();

    // Display model for the DataGridView (keeps internal Id hidden)
    private class PlacementRow
    {
        public int Id { get; set; }
        public string CandidateName { get; set; } = string.Empty;
        public string CareerPathway { get; set; } = string.Empty;
        public string PlacementDate { get; set; } = string.Empty;
        public string ExitDate { get; set; } = string.Empty;
        public string MonthsPlaced { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public bool NeedsReview { get; set; }
        public bool PastSixMonths { get; set; }
    }
}
