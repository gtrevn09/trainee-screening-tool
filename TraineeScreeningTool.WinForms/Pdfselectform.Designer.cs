namespace TraineeScreeningTool.WinForms;

partial class PdfSelectForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblPrompt = new Label();
        lstPdfs = new ListBox();
        btnOpen = new Button();
        btnOpenAll = new Button();
        btnCancel = new Button();

        // Prompt label
        lblPrompt.Text = "Select a PDF to open:";
        lblPrompt.Location = new Point(12, 12);
        lblPrompt.AutoSize = true;

        // PDF list
        lstPdfs.Location = new Point(12, 36);
        lstPdfs.Size = new Size(460, 240);
        lstPdfs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        lstPdfs.DoubleClick += lstPdfs_DoubleClick;

        // Open button
        btnOpen.Text = "Open";
        btnOpen.Size = new Size(100, 32);
        btnOpen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnOpen.Click += btnOpen_Click;

        // Open All button
        btnOpenAll.Text = "Open All";
        btnOpenAll.Size = new Size(100, 32);
        btnOpenAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnOpenAll.Click += btnOpenAll_Click;

        // Cancel button
        btnCancel.Text = "Cancel";
        btnCancel.Size = new Size(100, 32);
        btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancel.DialogResult = DialogResult.Cancel;

        // Form settings
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(484, 320);
        MinimumSize = new Size(400, 300);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.Sizable;
        CancelButton = btnCancel;
        Text = "Select PDF";

        Controls.Add(lblPrompt);
        Controls.Add(lstPdfs);
        Controls.Add(btnOpen);
        Controls.Add(btnOpenAll);
        Controls.Add(btnCancel);
    }

    private Label lblPrompt;
    private ListBox lstPdfs;
    private Button btnOpen;
    private Button btnOpenAll;
    private Button btnCancel;
}
