namespace TraineeScreeningTool.WinForms;

/// <summary>
/// Interactive user guide with searchable sections and plain-English instructions.
/// </summary>
public partial class UserGuideForm : Form
{
    // All guide sections: Title -> Content
    private readonly List<(string Title, string Content)> _sections = new()
    {
        (
            "Getting Started",
            """
WELCOME TO THE LIFE WORKS TRAINEE SCREENING TOOL
─────────────────────────────────────────────────

This app helps LIFE Works staff manage candidates who have completed Criteria
assessments. You can import their test results, track certifications they are
pursuing, record job placements, and view analytics over time.

FIRST-TIME LOGIN
────────────────
1. Open the app. The login screen will appear.
2. Enter the default credentials:
      Username:  admin
      Password:  lifeworks123
3. On your very first login you will be taken to an account setup screen.
4. Fill in your First Name, Last Name, Email, and choose a new password.
5. Click "Complete Setup." You will go straight to the dashboard on every
   future login.

FORGOT YOUR PASSWORD?
─────────────────────
1. On the login screen click "Forgot Password?"
2. Enter your Username, Full Name, and Email — all three must match exactly.
3. A temporary password will be shown on screen.
4. Log in with the temporary password. You will be required to set a new one
   before you can access the dashboard.

THE MAIN DASHBOARD
──────────────────
After logging in you will see the main dashboard. It has:
  • A search bar at the top to find candidates quickly.
  • A grid showing all candidates and their scores.
  • Two rows of buttons at the bottom for all actions.
  • A yellow notification banner (if any job placements need review).
"""
        ),

        (
            "Importing Candidates",
            """
IMPORTING CANDIDATE DATA
─────────────────────────

There are two ways to bring candidates into the system.

──────────────────────────────────────────────
METHOD 1 — IMPORT PDFs  (recommended, per candidate)
──────────────────────────────────────────────
Use this when you receive the 10 Criteria assessment PDF reports for a candidate.

Steps:
1. Click "Import PDF's" on the dashboard.
2. Click "Browse" and select all 10 PDF files for ONE candidate at a time.
   (All 10 should come from the same Criteria assessment batch.)
3. Click "Import PDFs."
4. The app reads the Summary PDF and the CSAP PDF automatically and fills in
   all scores.
5. A placeholder email is assigned — find the candidate in the grid afterwards
   and update their email address.
6. Repeat for each candidate.

TIPS:
  • Make sure the Summary PDF is included — it is required.
  • The filename must follow the Criteria naming format
    (e.g. FirstName-LastName-Summary-...).
  • If a candidate with the same email already exists they will be UPDATED,
    not duplicated.

──────────────────────────────────────────────
METHOD 2 — IMPORT CSV  (bulk historical data)
──────────────────────────────────────────────
Use this to load a large batch of candidates from a Criteria CSV export.

Steps:
1. Click "Import CSV" on the dashboard.
2. Click "Browse" and select the CSV file.
3. Click "Import CSV."
4. Only rows with status "Complete" are imported.
5. Existing candidates (matched by email) are updated, not duplicated.

──────────────────────────────────────────────
ADD A CANDIDATE MANUALLY
──────────────────────────────────────────────
If you need to add someone without any assessment data yet:
1. Click "Add" on the dashboard.
2. Enter First Name, Last Name, and Email.
3. Click "Save."
You can fill in their scores later using the "Assess" button.
"""
        ),

        (
            "Managing Candidates",
            """
MANAGING CANDIDATES ON THE DASHBOARD
──────────────────────────────────────

SEARCHING
─────────
• Type a name or email in the search bar at the top of the dashboard.
• The list filters in real time as you type.
• Clear the box to show all candidates again.

SELECTING A CANDIDATE
──────────────────────
• Click any row to select (highlight) it.
• Most buttons (Assess, View PDF's, Candidate Profile) work on the
  currently highlighted row.

EDITING SCORES DIRECTLY IN THE GRID
─────────────────────────────────────
• You can click any score cell in the grid and type a new value.
• Changes save automatically when you press Enter or click away.
• Use this for quick corrections.

ASSESS BUTTON
──────────────
• Select a candidate, then click "Assess."
• All existing scores will be pre-filled in the form.
• Edit any field and click "Submit" to save.

DELETING CANDIDATES
────────────────────
To delete one candidate:
  1. Click a row to select it.
  2. Click "Delete" and confirm.

To delete multiple candidates at once:
  1. Tick the checkbox at the start of each row you want to delete.
     (Tip: hold Shift and drag your mouse down to check multiple rows fast.)
  2. Click "Delete" and confirm.

VIEW PDF's
───────────
• Select a candidate, then click "View PDF's."
• If one PDF is found it opens immediately.
• If multiple PDFs are found a selection window appears — pick the one
  you want or click "Open All."
• PDFs are only available if the candidate was imported via "Import PDF's."
  If the original folder was moved or deleted, re-import the PDFs.
"""
        ),

        (
            "Candidate Profile",
            """
CANDIDATE PROFILE
──────────────────
The Candidate Profile gives you everything about one person in a single window.
Select a candidate and click "Candidate Profile" to open it.

The profile has three tabs:

──────────────────────────────────────────────
TAB 1 — SCORES
──────────────────────────────────────────────
Displays all assessment scores organized by test section:
  • CCAT   — Cognitive Aptitude Test
  • CMRA   — Mechanical Reasoning Assessment
  • CBST   — Basic Skills Test
  • CLIK   — Computer Literacy
  • TT     — Typing Test
  • CAST   — Attention Skills Test
  • Microsoft Office (Word & Excel)
  • CSAP   — Customer Service Aptitude Profile

The header at the top always shows the candidate's name, email, test date,
Talent Signal score, and any Pathway Recommendation that has been set.

Scroll down to see all scores — the panel is scrollable.

──────────────────────────────────────────────
TAB 2 — CERTIFICATIONS
──────────────────────────────────────────────
Shows all certifications this candidate is working on or has completed.

  • Click "+ Add Certification" to add a new one.
  • Double-click any row to edit or delete it.
  • Rows turn GREEN when the result is Pass, RED when Fail.
  • The summary line at the top shows total certifications and how many
    were passed.

──────────────────────────────────────────────
TAB 3 — JOB PLACEMENTS
──────────────────────────────────────────────
Shows all job placements recorded for this candidate.

  • Click "+ Add Placement" to record a new placement.
  • Select a row and click "Update" to edit it, or "Delete" to remove it.
  • Double-click a row to update it quickly.
  • ORANGE rows = past 6 months (may need success confirmation).
  • YELLOW rows = approaching 6 months (review coming up soon).
  • The summary line shows total placements, how many are active, and
    how many have been marked successful.
"""
        ),

        (
            "Certifications",
            """
CERTIFICATION TRACKING
───────────────────────
Certifications track qualifications each candidate is pursuing or has earned.

WHERE TO MANAGE CERTIFICATIONS
───────────────────────────────
Open the Candidate Profile (select a candidate → "Candidate Profile") and
click the "Certifications" tab.

ADDING A CERTIFICATION
───────────────────────
1. Click "+ Add Certification."
2. Select the Career Pathway — this filters the Certification dropdown to
   show relevant certifications for that field.
   (Choose "All / General" to see universal certifications like OSHA 10 or
   Microsoft Office Specialist.)
3. Select or type a certification name.
4. Set the Status:
     • Pursuing  — the candidate is currently working toward this cert.
     • Completed — the candidate has sat the exam.
5. If Completed, set the Result (Pass or Fail).
6. Optionally check "Set date" and pick the relevant date.
7. Add any notes and click "Save Certification."

EDITING A CERTIFICATION
────────────────────────
Double-click any row in the Certifications tab to open the edit form.
You can update the status, result, date, and notes.
You can also delete the certification from the edit form.

CERTIFICATION COLORS
─────────────────────
  • GREEN row = Passed
  • RED row   = Failed
  • No color  = Still Pursuing or no result yet

AVAILABLE CAREER PATHWAYS & CERTS
───────────────────────────────────
The following pathways each have a recommended certification list:
  1. Carpentry
  2. Information Technology
  3. Digital Marketing
  4. Bookkeeping / Accounting
  5. Automotive Technician
  6. Manufacturing
  7. Restaurant Management
  8. Administrative Assistant
  9. Customer Service / Sales
  10. Landscape Technician

All pathways also include general certifications:
  OSHA 10 Safety, CPR / First Aid,
  Microsoft Office Specialist – Word,
  Microsoft Office Specialist – Excel
"""
        ),

        (
            "Job Placements",
            """
JOB PLACEMENT TRACKING
────────────────────────
Job placements record where a candidate was placed for work and track whether
they stayed for 6 or more months (which counts as a successful placement).

WHERE TO MANAGE PLACEMENTS
───────────────────────────
Placements are managed through the Candidate Profile:

  1. Select a candidate on the dashboard.
  2. Click "Candidate Profile."
  3. Click the "Job Placements" tab.

All of that candidate's placements are listed here, and you can add,
update, or delete them from this tab.

ADDING A PLACEMENT
───────────────────
1. Open the Candidate Profile and go to the "Job Placements" tab.
2. Click "+ Add Placement."
3. Select the Career Pathway they were placed into.
4. Set the Placement Date (the date they started).
5. Click "Save Placement."

UPDATING A PLACEMENT
─────────────────────
Double-click a placement row, or select it and click "Update."
You can:
  • Set an Exit Date if the person has left the position.
  • Mark it as Successful once they have confirmed 6+ months.

THE 6-MONTH REVIEW SYSTEM
──────────────────────────
The app automatically flags placements that are approaching or past the
6-month mark:

  • YELLOW = approaching 6 months (review coming up soon)
  • ORANGE = past 6 months but not yet confirmed successful

When any placements need review, a yellow banner appears at the top of
the main dashboard. Click the banner to open a list of all flagged
placements across all candidates.

MARKING A PLACEMENT SUCCESSFUL
────────────────────────────────
1. Open the placement (double-click its row).
2. Check the "Successful Placement" checkbox.
3. Click "Save." The row will no longer be flagged.
"""
        ),

        (
            "Analytics",
            """
ANALYTICS DASHBOARD
────────────────────
The Analytics screen gives you a high-level view of how candidates are
performing across pathways and certifications.

Click "Analytics" on the main dashboard to open it.

SUMMARY BAR (top of screen)
─────────────────────────────
Shows three quick stats at a glance:
  • Total Candidates in the system
  • Successful Placements (marked as successful at 6+ months)
  • Certifications Earned (exams passed)

TAB 1 — SCORES BY CAREER PATHWAY
──────────────────────────────────
Shows average scores for candidates grouped by the career pathway they
were placed into. Columns include:
  • # of Candidates in that pathway
  • Average CCAT percentile
  • Average CBST percentile
  • Average Composite score
  • Number of Successful Placements
  • Score Pass Rate

Use this to see which pathways your candidates tend to score higher in.

TAB 2 — CERTIFICATION OUTCOMES
────────────────────────────────
Shows a breakdown for every certification that has been tracked:
  • How many candidates are Pursuing it
  • How many Passed
  • How many Failed
  • Overall Exam Pass Rate

  GREEN rows = more passes than fails
  RED rows   = more fails than passes

TAB 3 — PASS vs. FAIL ANALYSIS
────────────────────────────────
Compares average scores between candidates who score above the pass threshold
and those who score below it. Useful for spotting trends.

EXPORTING TO CSV
─────────────────
Any of the three tabs can be exported to a CSV file:
1. Select the tab you want to export.
2. Click "Export CSV."
3. Choose a save location. The file opens in Excel.
"""
        ),

        (
            "Account & Settings",
            """
MANAGING YOUR ACCOUNT
──────────────────────

USER DETAILS
─────────────
Click "User Details" on the dashboard to:
  • View your username and role.
  • Update your First Name, Last Name, or Email.
  • Click "Change Password" to set a new password.

CHANGING YOUR PASSWORD
───────────────────────
1. Click "User Details" → "Change Password."
2. Enter your current password.
3. Enter and confirm your new password.
4. Click "Save." You will stay logged in.

LOGGING OUT
────────────
Click "Logout" on the dashboard. You will be asked to confirm.
The app returns to the login screen.

VIEWING ACTIVITY LOGS
──────────────────────
Click "View Logs" on the dashboard to see a record of everything that has
happened in the app — logins, imports, deletions, certification changes,
and placement updates.

  • Logs are ordered most recent first.
  • You can refresh the list or clear all logs from this screen.
  • All actions are automatically recorded — you do not need to do anything
    to enable logging.

USER ROLES
───────────
  • Admin — full access to everything including logs.
  • Staff — standard access. Cannot access certain admin features.
"""
        ),

        (
            "Troubleshooting",
            """
COMMON ISSUES & FIXES
──────────────────────

PROBLEM: Import PDF's says "No Summary PDF found"
───────────────────────────────────────────────────
Make sure you are selecting ALL 10 PDF files at once, including the one
with "Summary" in its filename. All 10 files come together in the same
folder from the Criteria assessment platform.

PROBLEM: "View PDF's" says no PDFs found
──────────────────────────────────────────
This happens when:
  • The candidate was imported via CSV (CSV imports do not link PDFs).
  • The folder containing the original PDFs was moved or deleted.

Fix: Re-import the PDFs for that candidate using "Import PDF's." This
will re-link the folder automatically.

PROBLEM: A candidate appears twice in the list
────────────────────────────────────────────────
This happens when the same person was imported twice with different email
addresses (for example, once with a placeholder email and once with their
real email).

Fix: Delete the duplicate record — check the checkbox next to it and
click "Delete."
To avoid this going forward, always update the placeholder email right
after importing a candidate's PDFs.

PROBLEM: I can't find a candidate
───────────────────────────────────
Use the search bar at the top of the dashboard and type the candidate's
first name, last name, or email. The list filters as you type.

PROBLEM: Forgot password / can't log in
─────────────────────────────────────────
On the login screen click "Forgot Password?" and enter your username,
full name, and email address — all three must match what is on file.
A temporary password will be shown on screen. Log in with it and you
will be asked to set a new password before you can continue.

If you do not know your registered email or full name, contact your
system administrator to reset your account.

PROBLEM: The placement notification banner won't go away
─────────────────────────────────────────────────────────
The yellow banner appears when one or more job placements are approaching
or past the 6-month mark. To clear it:
1. Click the banner at the top of the dashboard to see all flagged placements.
2. Find the flagged placement(s) — they will be highlighted orange or yellow.
3. Open the placement and either mark it as Successful or record an Exit Date.
Once all flagged placements are resolved the banner will disappear.

PROBLEM: A certification or placement I added isn't showing
────────────────────────────────────────────────────────────
Make sure you saved it — look for the "Save" confirmation message.
Then open the Candidate Profile and check the correct tab
(Certifications or Job Placements). If it still doesn't appear, try
closing and reopening the Candidate Profile.

PROBLEM: Scores are showing blank / N/A
─────────────────────────────────────────
Scores only appear after a candidate's assessment data has been imported.
  • Use "Import PDF's" to import from the Criteria PDF reports.
  • Use "Import CSV" to import from a Criteria CSV export.
  • Or select the candidate and click "Assess" to enter scores manually.
"""
        ),
    };

    // Full flat list used for search (section index + line)
    private readonly List<(int SectionIndex, string Line)> _searchIndex = new();

    public UserGuideForm()
    {
        InitializeComponent();
        BuildSearchIndex();
        PopulateSections();
        lstSections.SelectedIndex = 0;
    }

    // ── Build the search index ────────────────────────────────────────

    private void BuildSearchIndex()
    {
        for (int i = 0; i < _sections.Count; i++)
        {
            _searchIndex.Add((i, _sections[i].Title));
            foreach (var line in _sections[i].Content.Split('\n'))
                _searchIndex.Add((i, line.Trim()));
        }
    }

    // ── Populate section list ─────────────────────────────────────────

    private void PopulateSections(string filter = "")
    {
        lstSections.BeginUpdate();
        lstSections.Items.Clear();

        for (int i = 0; i < _sections.Count; i++)
        {
            // Show section if filter is empty, or if any line in section matches
            if (string.IsNullOrWhiteSpace(filter) ||
                _searchIndex
                    .Where(x => x.SectionIndex == i)
                    .Any(x => x.Line.Contains(filter, StringComparison.OrdinalIgnoreCase)))
            {
                lstSections.Items.Add(new SectionItem(i, _sections[i].Title));
            }
        }

        lstSections.EndUpdate();

        if (lstSections.Items.Count > 0)
            lstSections.SelectedIndex = 0;
        else
            rtbContent.Text = "No results found. Try a different search term.";
    }

    // ── Show content for selected section ────────────────────────────

    private void lstSections_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstSections.SelectedItem is not SectionItem item) return;

        var (_, content) = _sections[item.Index];
        rtbContent.Text = content;

        // If there's a search term, highlight matches in the content
        string search = txtSearch.Text.Trim();
        if (!string.IsNullOrWhiteSpace(search))
            HighlightMatches(search);
    }

    // ── Search ────────────────────────────────────────────────────────

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        PopulateSections(txtSearch.Text.Trim());
    }

    private void btnClearSearch_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        txtSearch.Focus();
    }

    // ── Highlight search term in RichTextBox ──────────────────────────

    private void HighlightMatches(string term)
    {
        rtbContent.SelectAll();
        rtbContent.SelectionBackColor = rtbContent.BackColor;

        string text = rtbContent.Text;
        int start = 0;
        while (true)
        {
            int idx = text.IndexOf(term, start, StringComparison.OrdinalIgnoreCase);
            if (idx < 0) break;

            rtbContent.Select(idx, term.Length);
            rtbContent.SelectionBackColor = Color.FromArgb(255, 255, 150);
            start = idx + term.Length;
        }

        rtbContent.SelectionStart = 0;
        rtbContent.SelectionLength = 0;
    }

    // ── Helper class for list items ───────────────────────────────────

    private class SectionItem
    {
        public int Index { get; }
        public string Title { get; }

        public SectionItem(int index, string title)
        {
            Index = index;
            Title = title;
        }

        public override string ToString() => Title;
    }
}
