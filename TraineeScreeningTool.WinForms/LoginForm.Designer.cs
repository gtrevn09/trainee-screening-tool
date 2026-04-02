namespace TraineeScreeningTool.WinForms;

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
        btnForgotPassword = new Button();

        Label lblTitle = new Label();
        Label lblUsername = new Label();
        Label lblPassword = new Label();

        // Title label
        lblTitle.Text = "LIFE Works Trainee Screening Tool";
        lblTitle.Location = new Point(12, 20);
        lblTitle.Size = new Size(380, 30);
        lblTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;

        // Username label
        lblUsername.Text = "Username:";
        lblUsername.Location = new Point(12, 72);
        lblUsername.Size = new Size(100, 25);

        // Username text box
        txtUsername.Location = new Point(120, 69);
        txtUsername.Size = new Size(250, 25);
        txtUsername.Name = "txtUsername";

        // Password label
        lblPassword.Text = "Password:";
        lblPassword.Location = new Point(12, 108);
        lblPassword.Size = new Size(100, 25);

        // Password text box
        txtPassword.Location = new Point(120, 105);
        txtPassword.Size = new Size(250, 25);
        txtPassword.Name = "txtPassword";
        txtPassword.PasswordChar = '*';
        txtPassword.KeyPress += txtPassword_KeyPress;

        // Login button
        btnLogin.Location = new Point(120, 148);
        btnLogin.Size = new Size(250, 35);
        btnLogin.Text = "Login";
        btnLogin.Click += btnLogin_Click;

        // Forgot Password button - subtle link style
        btnForgotPassword.Location = new Point(120, 193);
        btnForgotPassword.Size = new Size(250, 35);
        btnForgotPassword.Text = "Forgot Password?";
        btnForgotPassword.FlatStyle = FlatStyle.Flat;
        btnForgotPassword.FlatAppearance.BorderSize = 0;
        btnForgotPassword.ForeColor = Color.SteelBlue;
        btnForgotPassword.Click += btnForgotPassword_Click;

        // Add controls to form
        ClientSize = new Size(400, 250);
        Controls.Add(lblTitle);
        Controls.Add(lblUsername);
        Controls.Add(txtUsername);
        Controls.Add(lblPassword);
        Controls.Add(txtPassword);
        Controls.Add(btnLogin);
        Controls.Add(btnForgotPassword);
        Text = "Login";
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
    }

    // Declare controls
    private TextBox txtUsername;
    private TextBox txtPassword;
    private Button btnLogin;
    private Button btnForgotPassword;
}