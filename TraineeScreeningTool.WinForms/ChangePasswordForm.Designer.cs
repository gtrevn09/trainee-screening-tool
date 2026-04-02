namespace TraineeScreeningTool.WinForms;

// This file handles the visual layout of the Change Password form
partial class ChangePasswordForm
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
        txtCurrentPassword = new TextBox();
        txtNewPassword = new TextBox();
        txtConfirmPassword = new TextBox();
        btnSave = new Button();
        Label lblTitle = new Label();
        Label lblCurrent = new Label();
        Label lblNew = new Label();
        Label lblConfirm = new Label();

        // Title label
        lblTitle.Text = "Change Password";
        lblTitle.Location = new Point(12, 15);
        lblTitle.Size = new Size(340, 25);
        lblTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;

        // Current Password label
        lblCurrent.Text = "Current Password:";
        lblCurrent.Location = new Point(12, 60);
        lblCurrent.Size = new Size(130, 20);

        // Current Password text box
        txtCurrentPassword.Location = new Point(150, 57);
        txtCurrentPassword.Size = new Size(200, 23);
        txtCurrentPassword.PasswordChar = '*';
        txtCurrentPassword.Name = "txtCurrentPassword";

        // New Password label
        lblNew.Text = "New Password:";
        lblNew.Location = new Point(12, 95);
        lblNew.Size = new Size(130, 20);

        // New Password text box
        txtNewPassword.Location = new Point(150, 92);
        txtNewPassword.Size = new Size(200, 23);
        txtNewPassword.PasswordChar = '*';
        txtNewPassword.Name = "txtNewPassword";

        // Confirm Password label
        lblConfirm.Text = "Confirm Password:";
        lblConfirm.Location = new Point(12, 130);
        lblConfirm.Size = new Size(130, 20);

        // Confirm Password text box
        txtConfirmPassword.Location = new Point(150, 127);
        txtConfirmPassword.Size = new Size(200, 23);
        txtConfirmPassword.PasswordChar = '*';
        txtConfirmPassword.Name = "txtConfirmPassword";

        // Save button
        btnSave.Location = new Point(150, 165);
        btnSave.Size = new Size(200, 35);
        btnSave.Text = "Save New Password";
        btnSave.Click += btnSave_Click;

        // Add controls to form
        ClientSize = new Size(380, 225);
        Controls.Add(lblTitle);
        Controls.Add(lblCurrent);
        Controls.Add(txtCurrentPassword);
        Controls.Add(lblNew);
        Controls.Add(txtNewPassword);
        Controls.Add(lblConfirm);
        Controls.Add(txtConfirmPassword);
        Controls.Add(btnSave);
        Text = "Change Password";
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
    }

    // Declare controls
    private TextBox txtCurrentPassword;
    private TextBox txtNewPassword;
    private TextBox txtConfirmPassword;
    private Button btnSave;
}