namespace TraineeScreeningTool.WinForms;

/// <summary>
/// Simple dialog that lets the user pick which PDF to open when a candidate
/// has multiple PDF files linked to them.
/// </summary>
public class PdfSelectForm : Form
{
    private readonly List<string> _pdfFiles;
    private ListBox lstPdfs = null!;
    private Button btnOpen = null!;
    private Button btnOpenAll = null!;
    private Button btnCancel = null!;

    public PdfSelectForm(string candidateName, List<string> pdfFiles)
    {
        _pdfFiles = pdfFiles;
        Text = $"Select PDF — {candidateName}";
        Size = new Size(500, 380);
        MinimumSize = new Size(400, 300);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.Sizable;

        BuildUI();
    }

    private void BuildUI()
    {
        var lblPrompt = new Label
        {
            Text = "Select a PDF to open:",
            Location = new Point(12, 12),
            AutoSize = true
        };

        lstPdfs = new ListBox
        {
            Location = new Point(12, 36),
            Size = new Size(460, 240),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
        };

        foreach (var file in _pdfFiles)
            lstPdfs.Items.Add(Path.GetFileName(file));

        if (lstPdfs.Items.Count > 0)
            lstPdfs.SelectedIndex = 0;

        lstPdfs.DoubleClick += (s, e) => OpenSelected();

        btnOpen = new Button
        {
            Text = "Open",
            Size = new Size(100, 32),
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left
        };
        btnOpen.Click += (s, e) => OpenSelected();

        btnOpenAll = new Button
        {
            Text = "Open All",
            Size = new Size(100, 32),
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left
        };
        btnOpenAll.Click += (s, e) => OpenAll();

        btnCancel = new Button
        {
            Text = "Cancel",
            Size = new Size(100, 32),
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
            DialogResult = DialogResult.Cancel
        };

        // Position buttons at bottom — adjusted in Resize
        PositionButtons();

        Controls.Add(lblPrompt);
        Controls.Add(lstPdfs);
        Controls.Add(btnOpen);
        Controls.Add(btnOpenAll);
        Controls.Add(btnCancel);

        CancelButton = btnCancel;
    }

    private void PositionButtons()
    {
        int y = ClientSize.Height - 42;
        btnOpen.Location = new Point(12, y);
        btnOpenAll.Location = new Point(120, y);
        btnCancel.Location = new Point(ClientSize.Width - 112, y);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        if (lstPdfs != null)
        {
            lstPdfs.Size = new Size(ClientSize.Width - 24, ClientSize.Height - 100);
            PositionButtons();
        }
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

    private void OpenAll()
    {
        foreach (var file in _pdfFiles)
            OpenFile(file);
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