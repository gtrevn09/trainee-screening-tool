using TraineeScreeningTool.WinForms.Data;
using TraineeScreeningTool.WinForms.Models;

namespace TraineeScreeningTool.WinForms;

public partial class EditCertificationForm : Form
{
    private readonly string _username;
    private readonly int _certificationId;

    private string    _origCertName = string.Empty;
    private string    _origStatus   = string.Empty;
    private string?   _origResult;
    private DateTime? _origDate;

    public EditCertificationForm(string username, int certificationId)
    {
        InitializeComponent();
        _username        = username;
        _certificationId = certificationId;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        cmbStatus.Items.Add("Pursuing");
        cmbStatus.Items.Add("Completed");

        cmbResult.Items.Add("(Not yet taken)");
        cmbResult.Items.Add("Pass");
        cmbResult.Items.Add("Fail");

        cmbStatus.SelectedIndexChanged += cmbStatus_SelectedIndexChanged;
        chkHasDate.CheckedChanged      += chkHasDate_CheckedChanged;

        LoadCertification();
    }

    private void LoadCertification()
    {
        using var context = new ApplicationDbContext();
        var cert = context.Certifications.Find(_certificationId);
        if (cert == null)
        {
            MessageBox.Show("Certification not found.", "Error");
            Close();
            return;
        }

        _origCertName = cert.CertName;
        _origStatus   = cert.Status;
        _origResult   = cert.Result;
        _origDate     = cert.Date;

        lblCandidateId.Text    = $"Candidate ID: {cert.CandidateId}";
        txtCertName.Text       = cert.CertName;
        cmbStatus.SelectedItem = cert.Status;

        cmbResult.SelectedItem = cert.Result ?? "(Not yet taken)";
        cmbResult.Enabled      = cert.Status == "Completed";

        if (cert.Date.HasValue)
        {
            chkHasDate.Checked = true;
            dtpDate.Value      = cert.Date.Value;
        }
        else
        {
            chkHasDate.Checked = false;
            dtpDate.Value      = DateTime.Today;
            dtpDate.Enabled    = false;
        }

        txtNotes.Text = cert.Notes;
    }

    private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool isCompleted  = cmbStatus.SelectedItem?.ToString() == "Completed";
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
        string certName = txtCertName.Text.Trim();
        if (string.IsNullOrEmpty(certName))
        {
            MessageBox.Show("Certification name cannot be empty.", "Validation Error",
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
        var cert = context.Certifications.Find(_certificationId);
        if (cert == null) { MessageBox.Show("Certification not found.", "Error"); Close(); return; }

        cert.CertName = certName;
        cert.Status   = status;
        cert.Result   = result;
        cert.Date     = date;
        cert.Notes    = txtNotes.Text.Trim();
        context.SaveChanges();

        var changes = new List<string>();
        if (_origCertName != certName) changes.Add($"Name: '{_origCertName}' → '{certName}'");
        if (_origStatus   != status)   changes.Add($"Status: {_origStatus} → {status}");
        if (_origResult   != result)   changes.Add($"Result: {_origResult ?? "none"} → {result ?? "none"}");
        if (_origDate     != date)     changes.Add($"Date: {_origDate?.ToString("yyyy-MM-dd") ?? "none"} → {date?.ToString("yyyy-MM-dd") ?? "none"}");

        string summary = changes.Count > 0 ? string.Join("; ", changes) : "No field changes";
        context.Log(_username, "Edit Certification",
            $"Updated certification ID {_certificationId} ('{certName}') for candidate ID {cert.CandidateId} — {summary}");

        MessageBox.Show("Certification updated successfully.", "Success",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        var confirm = MessageBox.Show(
            $"Are you sure you want to delete '{_origCertName}'?",
            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        if (confirm != DialogResult.Yes) return;

        using var context = new ApplicationDbContext();
        var cert = context.Certifications.Find(_certificationId);
        if (cert != null)
        {
            context.Certifications.Remove(cert);
            context.SaveChanges();
            context.Log(_username, "Delete Certification",
                $"Deleted certification '{_origCertName}' (ID {_certificationId}) for candidate ID {cert.CandidateId}");
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e) => Close();
}