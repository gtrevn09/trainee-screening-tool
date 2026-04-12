namespace TraineeScreeningTool.WinForms;

// This file handles the visual layout of the Forgot Password form
partial class ForgotPasswordForm
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
        txtUsername = new TextBox();
        txtFullName = new TextBox();
        txtEmail = new TextBox();
        btnSubmit = new Button();

        Label lblTitle = new Label();
        Label lblSubtitle = new Label();
        Label lblUsername = new Label();
        Label lblFullName = new Label();
        Label lblEmail = new Label();

        // Title label
        lblTitle.Text = "Forgot Password";
        lblTitle.Location = new Point(12, 15);
        lblTitle.Size = new Size(400, 30);
        lblTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;

        // Subtitle
        lblSubtitle.Text = "Enter your details below to reset your password.";
        lblSubtitle.Location = new Point(12, 50);
        lblSubtitle.Size = new Size(400, 25);
        lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
        lblSubtitle.ForeColor = Color.DimGray;

        // Username label
        lblUsername.Text = "Username:";
        lblUsername.Location = new Point(12, 95);
        lblUsername.Size = new Size(120, 25);

        // Username text box
        txtUsername.Location = new Point(140, 92);
        txtUsername.Size = new Size(270, 25);
        txtUsername.Name = "txtUsername";

        // Full Name label
        lblFullName.Text = "Full Name:";
        lblFullName.Location = new Point(12, 135);
        lblFullName.Size = new Size(120, 25);

        // Full Name text box
        txtFullName.Location = new Point(140, 132);
        txtFullName.Size = new Size(270, 25);
        txtFullName.Name = "txtFullName";

        // Email label
        lblEmail.Text = "Email Address:";
        lblEmail.Location = new Point(12, 175);
        lblEmail.Size = new Size(120, 25);

        // Email text box
        txtEmail.Location = new Point(140, 172);
        txtEmail.Size = new Size(270, 25);
        txtEmail.Name = "txtEmail";

        // Submit button
        btnSubmit.Location = new Point(140, 215);
        btnSubmit.Size = new Size(270, 40);
        btnSubmit.Text = "Get Temporary Password";
        btnSubmit.Click += btnSubmit_Click;

        // Add all controls
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(424, 275);
        Controls.Add(lblTitle);
        Controls.Add(lblSubtitle);
        Controls.Add(lblUsername);
        Controls.Add(txtUsername);
        Controls.Add(lblFullName);
        Controls.Add(txtFullName);
        Controls.Add(lblEmail);
        Controls.Add(txtEmail);
        Controls.Add(btnSubmit);
        Text = "Forgot Password";
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
    }

    // Declare controls
    private TextBox txtUsername;
    private TextBox txtFullName;
    private TextBox txtEmail;
    private Button btnSubmit;
}