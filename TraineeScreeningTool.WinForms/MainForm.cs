using Microsoft.EntityFrameworkCore;
using TraineeScreeningTool.WinForms.Data;
using TraineeScreeningTool.WinForms.Models;

namespace TraineeScreeningTool.WinForms;

public partial class MainForm : Form
{
    // Stores the logged in username so we can pass it to other forms
    private readonly string _username;

    // Constructor - accepts the logged in username from Program.cs
    public MainForm(string username)
    {
        InitializeComponent();
        _username = username;
        LoadCandidates();
    }

    // Fetches all candidates from the database and displays them in the grid
    private void LoadCandidates()
    {
        using var context = new ApplicationDbContext();
        dataGridView1.DataSource = context.Candidates.ToList();
        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
        var form = new AddCandidateForm(_username); // Pass username for logging
        form.ShowDialog();
        LoadCandidates();
    }

    // Opens the Import CSV form - passes username for logging
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

    // Deletes the selected candidate and logs the action
    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count == 0) return;
        int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;

        var confirm = MessageBox.Show(
            "Are you sure you want to delete this candidate?",
            "Confirm Delete",
            MessageBoxButtons.YesNo);

        if (confirm != DialogResult.Yes) return;

        using var context = new ApplicationDbContext();
        var candidate = context.Candidates.Find(id);

        if (candidate != null)
        {
            context.Log(_username, "Delete Candidate",
                $"Deleted candidate: {candidate.FullName} ({candidate.Email})");

            context.Candidates.Remove(candidate);
            context.SaveChanges();
        }

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