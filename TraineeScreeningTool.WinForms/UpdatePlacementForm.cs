using TraineeScreeningTool.WinForms.Data;
using TraineeScreeningTool.WinForms.Models;

namespace TraineeScreeningTool.WinForms;

public partial class UpdatePlacementForm : Form
{
    private readonly string _username;
    private readonly int _placementId;

    public UpdatePlacementForm(string username, int placementId)
    {
        InitializeComponent();
        _username = username;
        _placementId = placementId;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        LoadPlacement();
    }

    private void LoadPlacement()
    {
        using var context = new ApplicationDbContext();
        var placement = context.JobPlacements
            .Where(p => p.Id == _placementId)
            .Select(p => new
            {
                p.Id,
                p.CareerPathway,
                p.PlacementDate,
                p.ExitDate,
                p.IsSuccessful,
                CandidateName = p.Candidate != null
                    ? p.Candidate.FirstName + " " + p.Candidate.LastName
                    : "(Unknown)"
            })
            .FirstOrDefault();

        if (placement == null)
        {
            MessageBox.Show("Placement record not found.", "Error");
            this.Close();
            return;
        }

        lblCandidateName.Text = placement.CandidateName;
        lblPathway.Text = placement.CareerPathway;
        lblStartDate.Text = placement.PlacementDate.ToString("MMM d, yyyy");

        double months = (DateTime.Today - placement.PlacementDate).TotalDays / 30.44;
        lblMonths.Text = $"{months:F1} months";

        if (placement.ExitDate.HasValue)
        {
            chkHasExitDate.Checked = true;
            dtpExitDate.Value = placement.ExitDate.Value;
            dtpExitDate.Enabled = true;
        }
        else
        {
            chkHasExitDate.Checked = false;
            dtpExitDate.Enabled = false;
            dtpExitDate.Value = DateTime.Today;
        }

        chkIsSuccessful.Checked = placement.IsSuccessful;

        // Auto-suggest "successful" if past 6 months with no exit date
        if (!placement.ExitDate.HasValue && months >= 6 && !placement.IsSuccessful)
        {
            lblSuggestion.Text = "This placement is past 6 months — consider marking it as successful.";
            lblSuggestion.ForeColor = Color.DarkOrange;
            lblSuggestion.Visible = true;
        }
        else
        {
            lblSuggestion.Visible = false;
        }
    }

    private void chkHasExitDate_CheckedChanged(object sender, EventArgs e)
    {
        dtpExitDate.Enabled = chkHasExitDate.Checked;
        if (chkHasExitDate.Checked)
        {
            // If an exit date is set, it can't be a successful 6-month placement
            chkIsSuccessful.Checked = false;
            chkIsSuccessful.Enabled = false;
        }
        else
        {
            chkIsSuccessful.Enabled = true;
        }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        using var context = new ApplicationDbContext();
        var placement = context.JobPlacements.Find(_placementId);
        if (placement == null)
        {
            MessageBox.Show("Placement record not found.", "Error");
            return;
        }

        bool wasSuccessful = placement.IsSuccessful;
        DateTime? wasExitDate = placement.ExitDate;

        DateTime? newExitDate = chkHasExitDate.Checked ? dtpExitDate.Value.Date : (DateTime?)null;
        bool newIsSuccessful = chkIsSuccessful.Checked;

        // Validate: can't be successful if there's an exit date before 6 months
        if (newExitDate.HasValue && newIsSuccessful)
        {
            double months = (newExitDate.Value - placement.PlacementDate).TotalDays / 30.44;
            if (months < 6)
            {
                MessageBox.Show(
                    "A placement cannot be marked successful if the exit date is before 6 months.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
        }

        if (newExitDate.HasValue && newExitDate.Value < placement.PlacementDate)
        {
            MessageBox.Show("Exit date cannot be before the placement start date.", "Validation Error");
            return;
        }

        placement.ExitDate = newExitDate;
        placement.IsSuccessful = newIsSuccessful;
        context.SaveChanges();

        // Build a descriptive log entry of what changed
        var changes = new List<string>();
        if (wasExitDate != newExitDate)
            changes.Add($"ExitDate: {wasExitDate?.ToString("yyyy-MM-dd") ?? "none"} → {newExitDate?.ToString("yyyy-MM-dd") ?? "none"}");
        if (wasSuccessful != newIsSuccessful)
            changes.Add($"IsSuccessful: {wasSuccessful} → {newIsSuccessful}");

        if (changes.Count > 0)
            context.Log(_username, "Update Placement",
                $"Updated placement ID {_placementId}: {string.Join("; ", changes)}");

        MessageBox.Show("Placement updated successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.Close();
    }
}
