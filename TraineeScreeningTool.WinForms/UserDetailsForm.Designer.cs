namespace TraineeScreeningTool.WinForms;

partial class UserDetailsForm
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
        lblUsernameValue = new Label();
        lblRoleValue = new Label();
        lblPasswordValue = new Label();
        txtFirstName = new TextBox();
        txtLastName = new TextBox();
        txtEmail = new TextBox();
        btnUpdateDetails = new Button();
        btnChangePassword = new Button();

        Label lblTitle = new Label();
        Label lblUsername = new Label();
        Label lblRole = new Label();
        Label lblPassword = new Label();
        Label lblFirstName = new Label();
        Label lblLastName = new Label();
        Label lblEmail = new Label();

        int labelX = 12;
        int valueX = 160;
        int rowH = 40;
        int startY = 55;

        // Title
        lblTitle.Text = "User Details";
        lblTitle.Location = new Point(12, 15);
        lblTitle.Size = new Size(400, 30);
        lblTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;

        // Username row - read only
        lblUsername.Text = "Username:";
        lblUsername.Location = new Point(labelX, startY);
        lblUsername.Size = new Size(140, 25);
        lblUsername.Font = new Font("Segoe UI", 9, FontStyle.Bold);

        lblUsernameValue.Location = new Point(valueX, startY);
        lblUsernameValue.Size = new Size(230, 25);

        // Role row - read only
        lblRole.Text = "Role:";
        lblRole.Location = new Point(labelX, startY + rowH);
        lblRole.Size = new Size(140, 25);
        lblRole.Font = new Font("Segoe UI", 9, FontStyle.Bold);

        lblRoleValue.Location = new Point(valueX, startY + rowH);
        lblRoleValue.Size = new Size(230, 25);

        // Password row - read only
        lblPassword.Text = "Password:";
        lblPassword.Location = new Point(labelX, startY + rowH * 2);
        lblPassword.Size = new Size(140, 25);
        lblPassword.Font = new Font("Segoe UI", 9, FontStyle.Bold);

        lblPasswordValue.Location = new Point(valueX, startY + rowH * 2);
        lblPasswordValue.Size = new Size(230, 25);

        // First Name row - editable
        lblFirstName.Text = "First Name:";
        lblFirstName.Location = new Point(labelX, startY + rowH * 3);
        lblFirstName.Size = new Size(140, 25);
        lblFirstName.Font = new Font("Segoe UI", 9, FontStyle.Bold);

        txtFirstName.Location = new Point(valueX, startY + rowH * 3);
        txtFirstName.Size = new Size(230, 25);
        txtFirstName.Name = "txtFirstName";

        // Last Name row - editable
        lblLastName.Text = "Last Name:";
        lblLastName.Location = new Point(labelX, startY + rowH * 4);
        lblLastName.Size = new Size(140, 25);
        lblLastName.Font = new Font("Segoe UI", 9, FontStyle.Bold);

        txtLastName.Location = new Point(valueX, startY + rowH * 4);
        txtLastName.Size = new Size(230, 25);
        txtLastName.Name = "txtLastName";

        // Email row - editable
        lblEmail.Text = "Email:";
        lblEmail.Location = new Point(labelX, startY + rowH * 5);
        lblEmail.Size = new Size(140, 25);
        lblEmail.Font = new Font("Segoe UI", 9, FontStyle.Bold);

        txtEmail.Location = new Point(valueX, startY + rowH * 5);
        txtEmail.Size = new Size(230, 25);
        txtEmail.Name = "txtEmail";

        // Update Details button
        btnUpdateDetails.Location = new Point(labelX, startY + rowH * 6 + 10);
        btnUpdateDetails.Size = new Size(185, 35);
        btnUpdateDetails.Text = "Update Details";
        btnUpdateDetails.Click += btnUpdateDetails_Click;

        // Change Password button
        btnChangePassword.Location = new Point(labelX + 195, startY + rowH * 6 + 10);
        btnChangePassword.Size = new Size(185, 35);
        btnChangePassword.Text = "Change Password";
        btnChangePassword.Click += btnChangePassword_Click;

        // Add all controls
        ClientSize = new Size(404, startY + rowH * 7 + 20);
        Controls.Add(lblTitle);
        Controls.Add(lblUsername);
        Controls.Add(lblUsernameValue);
        Controls.Add(lblRole);
        Controls.Add(lblRoleValue);
        Controls.Add(lblPassword);
        Controls.Add(lblPasswordValue);
        Controls.Add(lblFirstName);
        Controls.Add(txtFirstName);
        Controls.Add(lblLastName);
        Controls.Add(txtLastName);
        Controls.Add(lblEmail);
        Controls.Add(txtEmail);
        Controls.Add(btnUpdateDetails);
        Controls.Add(btnChangePassword);
        Text = "User Details";
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
    }

    // Declare controls
    private Label lblUsernameValue;
    private Label lblRoleValue;
    private Label lblPasswordValue;
    private TextBox txtFirstName;
    private TextBox txtLastName;
    private TextBox txtEmail;
    private Button btnUpdateDetails;
    private Button btnChangePassword;
}