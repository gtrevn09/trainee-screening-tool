namespace TraineeScreeningTool.WinForms;

partial class FirstTimeSetupForm
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
        txtNewPassword = new TextBox();
        txtConfirmPassword = new TextBox();
        btnSave = new Button();

        Label lblTitle = new Label();
        Label lblSubtitle = new Label();
        Label lblFirstName = new Label();
        Label lblLastName = new Label();
        Label lblEmail = new Label();
        Label lblNewPassword = new Label();
        Label lblConfirmPassword = new Label();

        // Title label
        lblTitle.Text = "Welcome to LIFE Works Screening Tool";
        lblTitle.Location = new Point(12, 15);
        lblTitle.Size = new Size(460, 30);
        lblTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;

        // Subtitle label
        lblSubtitle.Text = "Please complete your account setup before continuing.";
        lblSubtitle.Location = new Point(12, 50);
        lblSubtitle.Size = new Size(460, 25);
        lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
        lblSubtitle.ForeColor = Color.DimGray;

        // First Name label
        lblFirstName.Text = "First Name:";
        lblFirstName.Location = new Point(12, 95);
        lblFirstName.Size = new Size(150, 25);

        // First Name text box
        txtFirstName.Location = new Point(170, 92);
        txtFirstName.Size = new Size(280, 25);
        txtFirstName.Name = "txtFirstName";

        // Last Name label
        lblLastName.Text = "Last Name:";
        lblLastName.Location = new Point(12, 132);
        lblLastName.Size = new Size(150, 25);

        // Last Name text box
        txtLastName.Location = new Point(170, 129);
        txtLastName.Size = new Size(280, 25);
        txtLastName.Name = "txtLastName";

        // Email label
        lblEmail.Text = "Email Address:";
        lblEmail.Location = new Point(12, 169);
        lblEmail.Size = new Size(150, 25);

        // Email text box
        txtEmail.Location = new Point(170, 166);
        txtEmail.Size = new Size(280, 25);
        txtEmail.Name = "txtEmail";

        // New Password label
        lblNewPassword.Text = "New Password:";
        lblNewPassword.Location = new Point(12, 206);
        lblNewPassword.Size = new Size(150, 25);

        // New Password text box
        txtNewPassword.Location = new Point(170, 203);
        txtNewPassword.Size = new Size(280, 25);
        txtNewPassword.Name = "txtNewPassword";
        txtNewPassword.PasswordChar = '*';

        // Confirm Password label
        lblConfirmPassword.Text = "Confirm Password:";
        lblConfirmPassword.Location = new Point(12, 243);
        lblConfirmPassword.Size = new Size(150, 25);

        // Confirm Password text box
        txtConfirmPassword.Location = new Point(170, 240);
        txtConfirmPassword.Size = new Size(280, 25);
        txtConfirmPassword.Name = "txtConfirmPassword";
        txtConfirmPassword.PasswordChar = '*';

        // Save button
        btnSave.Location = new Point(170, 285);
        btnSave.Size = new Size(280, 40);
        btnSave.Text = "Complete Setup";
        btnSave.Click += btnSave_Click;

        // Add all controls
        ClientSize = new Size(484, 345);
        Controls.Add(lblTitle);
        Controls.Add(lblSubtitle);
        Controls.Add(lblFirstName);
        Controls.Add(txtFirstName);
        Controls.Add(lblLastName);
        Controls.Add(txtLastName);
        Controls.Add(lblEmail);
        Controls.Add(txtEmail);
        Controls.Add(lblNewPassword);
        Controls.Add(txtNewPassword);
        Controls.Add(lblConfirmPassword);
        Controls.Add(txtConfirmPassword);
        Controls.Add(btnSave);
        Text = "Account Setup";
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
    }

    // Declare controls
    private TextBox txtFirstName;
    private TextBox txtLastName;
    private TextBox txtEmail;
    private TextBox txtNewPassword;
    private TextBox txtConfirmPassword;
    private Button btnSave;
}