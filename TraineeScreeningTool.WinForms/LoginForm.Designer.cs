namespace TraineeScreeningTool.WinForms;

// This file handles the visual layout of the Login form
partial class LoginForm
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
        txtPassword = new TextBox();
        btnLogin = new Button();
        lblTitle = new Label();
        lblUsername = new Label();
        lblPassword = new Label();
        SuspendLayout();
        // 
        // txtUsername
        // 
        txtUsername.Location = new Point(100, 62);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(250, 31);
        txtUsername.TabIndex = 2;
        // 
        // txtPassword
        // 
        txtPassword.Location = new Point(100, 97);
        txtPassword.Name = "txtPassword";
        txtPassword.PasswordChar = '*';
        txtPassword.Size = new Size(250, 31);
        txtPassword.TabIndex = 4;
        txtPassword.KeyPress += txtPassword_KeyPress;
        // 
        // btnLogin
        // 
        btnLogin.Location = new Point(100, 135);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(250, 35);
        btnLogin.TabIndex = 5;
        btnLogin.Text = "Login";
        btnLogin.Click += btnLogin_Click;
        // 
        // lblTitle
        // 
        lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblTitle.Location = new Point(12, 20);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(407, 32);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "LIFE Works Trainee Screening Tool";
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblUsername
        // 
        lblUsername.Location = new Point(12, 65);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(92, 20);
        lblUsername.TabIndex = 1;
        lblUsername.Text = "Username:";
        // 
        // lblPassword
        // 
        lblPassword.Location = new Point(12, 100);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new Size(92, 28);
        lblPassword.TabIndex = 3;
        lblPassword.Text = "Password:";
        // 
        // LoginForm
        // 
        ClientSize = new Size(467, 200);
        Controls.Add(lblTitle);
        Controls.Add(lblUsername);
        Controls.Add(txtUsername);
        Controls.Add(lblPassword);
        Controls.Add(txtPassword);
        Controls.Add(btnLogin);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Login";
        ResumeLayout(false);
        PerformLayout();
    }

    // Declare controls
    private TextBox txtUsername;
    private TextBox txtPassword;
    private Button btnLogin;
    private Label lblTitle;
    private Label lblUsername;
    private Label lblPassword;
}