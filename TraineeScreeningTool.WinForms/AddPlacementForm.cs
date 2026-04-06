using TraineeScreeningTool.WinForms.Data;
using TraineeScreeningTool.WinForms.Models;

namespace TraineeScreeningTool.WinForms;

public partial class AddPlacementForm : Form
{
    private readonly string _username;

    // Optional: if opened for a specific candidate, pre-select them
    private readonly int? _preselectedCandidateId;

    public AddPlacementForm(string username, int? candidateId = null)
    {
        InitializeComponent();
        _username = username;
        _preselectedCandidateId = candidateId;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        LoadCandidates();
        LoadPathways();
        dtpPlacementDate.Value = DateTime.Today;

        // Pre-select candidate if one was passed in
        if (_preselectedCandidateId.HasValue)
        {
            foreach (var item in cmbCandidate.Items)
            {
                if (item is CandidateItem ci && ci.Id == _preselectedCandidateId.Value)
                {
                    cmbCandidate.SelectedItem = item;
                    cmbCandidate.Enabled = false;
                    break;
                }
            }
        }
    }

    private void LoadCandidates()
    {
        using var context = new ApplicationDbContext();
        var candidates = context.Candidates
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .Select(c => new CandidateItem { Id = c.Id, DisplayName = c.FirstName + " " + c.LastName + " (" + c.Email + ")" })
            .ToList();

        cmbCandidate.DataSource = candidates;
        cmbCandidate.DisplayMember = "DisplayName";
        cmbCandidate.ValueMember = "Id";
    }

    private void LoadPathways()
    {
        cmbCareerPathway.Items.Clear();
        foreach (var pathway in CareerPathways.All)
            cmbCareerPathway.Items.Add(pathway);
        cmbCareerPathway.SelectedIndex = 0;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (cmbCandidate.SelectedItem == null)
        {
            MessageBox.Show("Please select a candidate.", "Validation Error");
            return;
        }

        if (cmbCareerPathway.SelectedItem == null)
        {
            MessageBox.Show("Please select a career pathway.", "Validation Error");
            return;
        }

        var candidateItem = (CandidateItem)cmbCandidate.SelectedItem;

        using var context = new ApplicationDbContext();

        // Check for an existing active placement in the same pathway for this candidate
        var existing = context.JobPlacements.FirstOrDefault(p =>
            p.CandidateId == candidateItem.Id &&
            p.CareerPathway == cmbCareerPathway.SelectedItem.ToString() &&
            p.ExitDate == null);

        if (existing != null)
        {
            MessageBox.Show(
                "This candidate already has an active placement in that pathway.",
                "Duplicate Placement",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        var placement = new JobPlacement
        {
            CandidateId = candidateItem.Id,
            CareerPathway = cmbCareerPathway.SelectedItem.ToString()!,
            PlacementDate = dtpPlacementDate.Value.Date,
            IsSuccessful = false
        };

        context.JobPlacements.Add(placement);
        context.SaveChanges();

        context.Log(_username, "Add Placement",
            $"Added placement for candidate ID {candidateItem.Id} in '{placement.CareerPathway}' starting {placement.PlacementDate:yyyy-MM-dd}");

        MessageBox.Show("Placement added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    // Simple container used for the candidate dropdown
    private class CandidateItem
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public override string ToString() => DisplayName;
    }
}
