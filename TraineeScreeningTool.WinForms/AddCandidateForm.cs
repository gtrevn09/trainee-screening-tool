using System.Text.RegularExpressions;
using TraineeScreeningTool.WinForms.Data;
using TraineeScreeningTool.WinForms.Models;

namespace TraineeScreeningTool.WinForms;

public partial class AddCandidateForm : Form
{
    // Stores the logged in username for logging purposes
    private readonly string _username;

    // Constructor - accepts the logged in username from MainForm
    public AddCandidateForm(string username)
    {
        InitializeComponent();
        _username = username; // Save username for logging
    }

    // Validates that the email address is in a proper format
    private bool IsValidEmail(string email)
    {
        // Check that email contains @ and a domain with a dot
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    // Saves the new candidate to the database when Save is clicked
    private void btnSave_Click(object sender, EventArgs e)
    {
        // Check that all fields are filled in
        if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
            string.IsNullOrWhiteSpace(txtLastName.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text))
        {
            MessageBox.Show("Please fill in all fields.", "Validation Error");
            return;
        }

        // Check that the email address is valid
        if (!IsValidEmail(txtEmail.Text.Trim()))
        {
            MessageBox.Show("Please enter a valid email address.", "Validation Error");
            txtEmail.Focus();
            return;
        }

        using var context = new ApplicationDbContext(); // Open database connection

        // Create a new candidate using the text box values
        var candidate = new Candidate
        {
            FirstName = txtFirstName.Text.Trim(),
            LastName = txtLastName.Text.Trim(),
            Email = txtEmail.Text.Trim()
        };

        context.Candidates.Add(candidate);
        context.SaveChanges(); // Save to database

        // Log the add action
        context.Log(_username, "Add Candidate",
            $"Added new candidate: {candidate.FirstName} {candidate.LastName} ({candidate.Email})");

        this.Close(); // Close the form after saving
    }
}