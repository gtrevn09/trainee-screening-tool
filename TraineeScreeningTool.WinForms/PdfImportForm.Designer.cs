namespace TraineeScreeningTool.WinForms;

partial class PdfImportForm
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
        txtFolderPath = new TextBox();
        btnBrowse = new Button();
        btnImport = new Button();
        lblPdfCount = new Label();

        Label lblTitle = new Label();
        Label lblFolder = new Label();
        Label lblNote = new Label();

        // Title label
        lblTitle.Text = "Import Candidate PDFs";
        lblTitle.Location = new Point(12, 15);
        lblTitle.Size = new Size(560, 30);
        lblTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;

        // Folder label
        lblFolder.Text = "PDF Files:";
        lblFolder.Location = new Point(12, 67);
        lblFolder.Size = new Size(75, 25);

        // File path text box
        txtFolderPath.Location = new Point(95, 64);
        txtFolderPath.Size = new Size(330, 25);
        txtFolderPath.Name = "txtFolderPath";
        txtFolderPath.ReadOnly = true;

        // Browse button - tall enough to show full text
        btnBrowse.Location = new Point(435, 62);
        btnBrowse.Size = new Size(110, 30);
        btnBrowse.Text = "Browse...";
        btnBrowse.Click += btnBrowse_Click;

        // PDF count label
        lblPdfCount.Location = new Point(12, 105);
        lblPdfCount.Size = new Size(540, 25);
        lblPdfCount.Name = "lblPdfCount";
        lblPdfCount.ForeColor = Color.DimGray;
        lblPdfCount.Text = "No files selected";

        // Note label - wide enough to fit on one line
        lblNote.Text = "Note: Select all 10 PDF reports for one candidate at a time. The app will automatically read each test report.";
        lblNote.Location = new Point(12, 135);
        lblNote.Size = new Size(540, 40);
        lblNote.ForeColor = Color.DimGray;

        // Import button
        btnImport.Location = new Point(150, 190);
        btnImport.Size = new Size(260, 40);
        btnImport.Text = "Import PDFs";
        btnImport.Click += btnImport_Click;

        // Add all controls
        ClientSize = new Size(584, 250);
        Controls.Add(lblTitle);
        Controls.Add(lblFolder);
        Controls.Add(txtFolderPath);
        Controls.Add(btnBrowse);
        Controls.Add(lblPdfCount);
        Controls.Add(lblNote);
        Controls.Add(btnImport);
        Text = "Import PDFs";
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
    }

    private TextBox txtFolderPath;
    private Button btnBrowse;
    private Button btnImport;
    private Label lblPdfCount;
}