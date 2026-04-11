namespace TraineeScreeningTool.WinForms;

// This file handles the visual layout of the Import form
partial class ImportForm
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
        txtFilePath = new TextBox();
        btnBrowse = new Button();
        btnImport = new Button();
        lblTitle = new Label();
        lblFile = new Label();
        lblNote = new Label();
        SuspendLayout();
        // 
        // txtFilePath
        // 
        txtFilePath.Location = new Point(90, 57);
        txtFilePath.Name = "txtFilePath";
        txtFilePath.ReadOnly = true;
        txtFilePath.Size = new Size(270, 31);
        txtFilePath.TabIndex = 2;
        // 
        // btnBrowse
        // 
        btnBrowse.Location = new Point(370, 57);
        btnBrowse.Name = "btnBrowse";
        btnBrowse.Size = new Size(100, 31);
        btnBrowse.TabIndex = 3;
        btnBrowse.Text = "Browse...";
        btnBrowse.Click += btnBrowse_Click;
        // 
        // btnImport
        // 
        btnImport.Location = new Point(90, 164);
        btnImport.Name = "btnImport";
        btnImport.Size = new Size(280, 35);
        btnImport.TabIndex = 5;
        btnImport.Text = "Import CSV";
        btnImport.Click += btnImport_Click;
        // 
        // lblTitle
        // 
        lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblTitle.Location = new Point(12, 3);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(460, 51);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Import Assessment CSV";
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblFile
        // 
        lblFile.Location = new Point(12, 60);
        lblFile.Name = "lblFile";
        lblFile.Size = new Size(70, 20);
        lblFile.TabIndex = 1;
        lblFile.Text = "CSV File:";
        // 
        // lblNote
        // 
        lblNote.ForeColor = Color.DimGray;
        lblNote.Location = new Point(12, 95);
        lblNote.Name = "lblNote";
        lblNote.Size = new Size(460, 66);
        lblNote.TabIndex = 4;
        lblNote.Text = "Note: Only rows with status 'Complete' will be imported.\nCandidates already in the database will be updated.";
        // 
        // ImportForm
        // 
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(531, 294);
        Controls.Add(lblTitle);
        Controls.Add(lblFile);
        Controls.Add(txtFilePath);
        Controls.Add(btnBrowse);
        Controls.Add(lblNote);
        Controls.Add(btnImport);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "ImportForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Import CSV";
        ResumeLayout(false);
        PerformLayout();
    }

    // Declare controls
    private TextBox txtFilePath;
    private Button btnBrowse;
    private Button btnImport;
    private Label lblTitle;
    private Label lblFile;
    private Label lblNote;
}