using TraineeScreeningTool.WinForms.Data;
using TraineeScreeningTool.WinForms.Models;

namespace TraineeScreeningTool.WinForms;

public partial class AddCertificationForm : Form
{
    private readonly string _username;
    private readonly int _candidateId;
    private readonly string _candidateName;

    public AddCertificationForm(string username, int candidateId, string candidateName)
    {
        InitializeComponent();
        _username      = username;
        _candidateId   = candidateId;
        _candidateName = candidateName;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        lblCandidateName.Text = _candidateName;
        LoadCertificationSuggestions();

        cmbStatus.Items.Add("Pursuing");
        cmbStatus.Items.Add("Completed");
        cmbStatus.SelectedIndex = 0;

        cmbResult.Items.Add("(Not yet taken)");
        cmbResult.Items.Add("Pass");
        cmbResult.Items.Add("Fail");
        cmbResult.SelectedIndex = 0;
        cmbResult.Enabled = false;

        dtpDate.Value   = DateTime.Today;
        dtpDate.Enabled = false;
        chkHasDate.Checked = false;

        cmbStatus.SelectedIndexChanged += cmbStatus_SelectedIndexChanged;
        chkHasDate.CheckedChanged      += chkHasDate_CheckedChanged;
    }

    private void cmbPathway_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCertificationSuggestions();
    }

    private void LoadCertificationSuggestions()
    {
        string pathway = cmbPathway.SelectedItem?.ToString() ?? string.Empty;
        var certs = CareerPathways.GetCertificationsFor(pathway);

        cmbCertName.Items.Clear();
        foreach (var cert in certs)
            cmbCertName.Items.Add(cert);

        if (cmbCertName.Items.Count > 0)
            cmbCertName.SelectedIndex = 0;
    }

    private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool isCompleted = cmbStatus.SelectedItem?.ToString() == "Completed";
        cmbResult.Enabled = isCompleted;
        if (!isCompleted)
            cmbResult.SelectedIndex = 0;
    }

    private void chkHasDate_CheckedChanged(object sender, EventArgs e)
    {
        dtpDate.Enabled = chkHasDate.Checked;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        string certName = cmbCertName.Text.Trim();
        if (string.IsNullOrEmpty(certName))
        {
            MessageBox.Show("Please enter or select a certification name.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string status  = cmbStatus.SelectedItem?.ToString() ?? "Pursuing";
        string? result = null;
        if (status == "Completed" &&
            cmbResult.SelectedItem?.ToString() is string r &&
            r != "(Not yet taken)")
            result = r;

        DateTime? date = chkHasDate.Checked ? dtpDate.Value.Date : null;

        using var context = new ApplicationDbContext();

        var cert = new Certification
        {
            CandidateId = _candidateId,
            CertName    = certName,
            Status      = status,
            Result      = result,
            Date        = date,
            Notes       = txtNotes.Text.Trim()
        };

        context.Certifications.Add(cert);
        context.SaveChanges();

        string dateStr   = date?.ToString("yyyy-MM-dd") ?? "no date";
        string resultStr = result ?? "in progress";
        context.Log(_username, "Add Certification",
            $"Added certification '{certName}' for candidate ID {_candidateId} " +
            $"({_candidateName}) — Status: {status}, Result: {resultStr}, Date: {dateStr}");

        MessageBox.Show("Certification added successfully.", "Success",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e) => Close();
}