namespace TraineeScreeningTool.WinForms;

// This file handles the visual layout of the Add Candidate form
partial class AddCandidateForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        txtFirstName = new TextBox();
        txtLastName = new TextBox();
        txtEmail = new TextBox();
        btnSave = new Button();
        Label lblFirstName = new Label();
        Label lblLastName = new Label();
        Label lblEmail = new Label();

        // First Name label
        lblFirstName.Text = "First Name:";
        lblFirstName.Location = new Point(12, 20);
        lblFirstName.Size = new Size(80, 20);

        // First Name text box
        txtFirstName.Location = new Point(100, 17);
        txtFirstName.Size = new Size(200, 23);
        txtFirstName.Name = "txtFirstName";

        // Last Name label
        lblLastName.Text = "Last Name:";
        lblLastName.Location = new Point(12, 55);
        lblLastName.Size = new Size(80, 20);

        // Last Name text box
        txtLastName.Location = new Point(100, 52);
        txtLastName.Size = new Size(200, 23);
        txtLastName.Name = "txtLastName";

        // Email label
        lblEmail.Text = "Email:";
        lblEmail.Location = new Point(12, 90);
        lblEmail.Size = new Size(80, 20);

        // Email text box
        txtEmail.Location = new Point(100, 87);
        txtEmail.Size = new Size(200, 23);
        txtEmail.Name = "txtEmail";

        // Save button
        btnSave.Location = new Point(100, 125);
        btnSave.Size = new Size(120, 30);
        btnSave.Text = "Save";
        btnSave.Click += btnSave_Click;

        // Add controls to form
        ClientSize = new Size(350, 180);
        Controls.Add(lblFirstName);
        Controls.Add(txtFirstName);
        Controls.Add(lblLastName);
        Controls.Add(txtLastName);
        Controls.Add(lblEmail);
        Controls.Add(txtEmail);
        Controls.Add(btnSave);
        Text = "Add Candidate";
    }

    // Declare controls
    private TextBox txtFirstName;
    private TextBox txtLastName;
    private TextBox txtEmail;
    private Button btnSave;
}