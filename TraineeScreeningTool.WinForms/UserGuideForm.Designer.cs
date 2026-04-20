namespace TraineeScreeningTool.WinForms;

partial class UserGuideForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        txtSearch = new TextBox();
        btnClearSearch = new Button();
        lstSections = new ListBox();
        rtbContent = new RichTextBox();

        // ── Top search/header panel ───────────────────────────────────
        var pnlTop = new Panel
        {
            Dock = DockStyle.Top,
            Height = 56,
            BackColor = Color.FromArgb(30, 80, 140)
        };

        var lblSearch = new Label
        {
            Text = "Search guide:",
            AutoSize = false,
            Size = new Size(110, 30),
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            ForeColor = Color.White,
            Location = new Point(12, 13),
            TextAlign = ContentAlignment.MiddleLeft
        };

        txtSearch.Location = new Point(126, 13);
        txtSearch.Size = new Size(220, 30);
        txtSearch.Font = new Font("Segoe UI", 9);
        txtSearch.PlaceholderText = "e.g. import, delete, password...";
        txtSearch.TextChanged += txtSearch_TextChanged;

        btnClearSearch.Text = "Clear";
        btnClearSearch.Location = new Point(354, 13);
        btnClearSearch.Size = new Size(65, 30);
        btnClearSearch.FlatStyle = FlatStyle.Flat;
        btnClearSearch.Font = new Font("Segoe UI", 9);
        btnClearSearch.BackColor = Color.FromArgb(80, 110, 160);
        btnClearSearch.ForeColor = Color.White;
        btnClearSearch.Click += btnClearSearch_Click;

        var lblTitle = new Label
        {
            Text = "LIFE Works — User Guide",
            AutoSize = true,
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.White,
            Location = new Point(440, 17)
        };

        pnlTop.Controls.AddRange(new Control[]
            { lblSearch, txtSearch, btnClearSearch, lblTitle });

        // ── Left panel — fixed width, docked left ─────────────────────
        var pnlLeft = new Panel
        {
            Dock = DockStyle.Left,
            Width = 240,
            BackColor = Color.FromArgb(245, 247, 252)
        };

        // Thin right border on the left panel
        var pnlBorder = new Panel
        {
            Dock = DockStyle.Right,
            Width = 1,
            BackColor = Color.FromArgb(200, 210, 230)
        };

        var lblTopics = new Label
        {
            Text = "TOPICS",
            Dock = DockStyle.Top,
            Height = 30,
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(12, 0, 0, 0),
            Font = new Font("Segoe UI", 8, FontStyle.Bold),
            ForeColor = Color.Gray,
            BackColor = Color.FromArgb(235, 239, 248)
        };

        lstSections.Dock = DockStyle.Fill;
        lstSections.Font = new Font("Segoe UI", 9.5f);
        lstSections.BorderStyle = BorderStyle.None;
        lstSections.BackColor = Color.FromArgb(245, 247, 252);
        lstSections.ItemHeight = 38;
        lstSections.DrawMode = DrawMode.OwnerDrawFixed;
        lstSections.SelectedIndexChanged += lstSections_SelectedIndexChanged;

        lstSections.DrawItem += (s, e) =>
        {
            if (e.Index < 0) return;
            bool sel = (e.State & DrawItemState.Selected) != 0;

            Color bg = sel
                ? Color.FromArgb(30, 80, 140)
                : (e.Index % 2 == 0
                    ? Color.FromArgb(245, 247, 252)
                    : Color.FromArgb(235, 240, 250));

            using var bgBrush = new SolidBrush(bg);
            e.Graphics.FillRectangle(bgBrush, e.Bounds);

            if (sel)
            {
                using var accent = new SolidBrush(Color.FromArgb(90, 160, 230));
                e.Graphics.FillRectangle(accent,
                    new Rectangle(0, e.Bounds.Y, 5, e.Bounds.Height));
            }

            using var textBrush = new SolidBrush(sel ? Color.White : Color.FromArgb(25, 40, 65));
            var rect = new RectangleF(
                e.Bounds.X + 14, e.Bounds.Y + 9,
                e.Bounds.Width - 18, e.Bounds.Height - 9);
            e.Graphics.DrawString(
                lstSections.Items[e.Index].ToString(),
                lstSections.Font, textBrush, rect);
        };

        pnlLeft.Controls.Add(lstSections);
        pnlLeft.Controls.Add(lblTopics);
        pnlLeft.Controls.Add(pnlBorder);

        // ── Right panel — fills remaining space ───────────────────────
        var pnlRight = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            Padding = new Padding(16, 12, 16, 12)
        };

        rtbContent.Dock = DockStyle.Fill;
        rtbContent.Font = new Font("Segoe UI", 9.5f);
        rtbContent.ReadOnly = true;
        rtbContent.BorderStyle = BorderStyle.None;
        rtbContent.BackColor = Color.White;
        rtbContent.ScrollBars = RichTextBoxScrollBars.Vertical;

        pnlRight.Controls.Add(rtbContent);

        // ── Form setup ────────────────────────────────────────────────
        // Controls added in reverse dock order: Fill first, then Left, then Top
        AutoScaleMode = AutoScaleMode.None;
        ClientSize = new Size(940, 640);
        MinimumSize = new Size(780, 520);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.Sizable;
        Text = "User Guide — LIFE Works Trainee Screening Tool";
        BackColor = Color.White;

        Controls.Add(pnlRight);
        Controls.Add(pnlLeft);
        Controls.Add(pnlTop);
    }

    private TextBox txtSearch;
    private Button btnClearSearch;
    private ListBox lstSections;
    private RichTextBox rtbContent;
}
