namespace TraineeScreeningTool.WinForms;

/// <summary>
/// Simple dialog that lets the user pick which PDF to open when a candidate
/// has multiple PDF files linked to them.
/// </summary>
public partial class PdfSelectForm : Form
{
    private readonly List<string> _pdfFiles;

    public PdfSelectForm(string candidateName, List<string> pdfFiles)
    {
        _pdfFiles = pdfFiles;
        InitializeComponent();
        Text = $"Select PDF — {candidateName}";

        foreach (var file in _pdfFiles)
            lstPdfs.Items.Add(Path.GetFileName(file));

        if (lstPdfs.Items.Count > 0)
            lstPdfs.SelectedIndex = 0;
    }

    // Repositions the three buttons when the form is resized
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        if (lstPdfs == null) return;

        lstPdfs.Size = new Size(ClientSize.Width - 24, ClientSize.Height - 100);
        int y = ClientSize.Height - 42;
        btnOpen.Location = new Point(12, y);
        btnOpenAll.Location = new Point(120, y);
        btnCancel.Location = new Point(ClientSize.Width - 112, y);
    }

    private void lstPdfs_DoubleClick(object sender, EventArgs e)
    {
        OpenSelected();
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
        OpenSelected();
    }

    private void btnOpenAll_Click(object sender, EventArgs e)
    {
        foreach (var file in _pdfFiles)
            OpenFile(file);
    }

    private void OpenSelected()
    {
        if (lstPdfs.SelectedIndex < 0)
        {
            MessageBox.Show("Please select a PDF first.", "No Selection");
            return;
        }
        OpenFile(_pdfFiles[lstPdfs.SelectedIndex]);
    }

    private static void OpenFile(string path)
    {
        try
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Could not open file:\n{path}\n\n{ex.Message}", "Error");
        }
    }
}
