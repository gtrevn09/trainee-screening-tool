# LIFE Works Trainee Screening Tool

## Project Overview
A desktop application built with C# WinForms and SQLite that allows LIFE Works staff to import, manage, and analyze trainee assessment data to track certifications, job placements, and build data-driven career pathway recommendations over time.

**Built by:** Gage Trevino, Hamza Quadri, Jordan Charlie, Evan Ernst, Branson McLaughlin

**Tech Stack:** C#, WinForms, Entity Framework Core, SQLite, .NET 9.0, iText7 (PDF parsing)

---

## Prerequisites — Install These First
Before pulling the project, make sure you have the following installed:

1. **Visual Studio Community 2022**
   - Download from https://visualstudio.microsoft.com/vs/community/
   - During installation, select the **.NET Desktop Development** workload
2. **.NET 9.0 SDK**
   - Download from https://dotnet.microsoft.com/download
3. **Git**
   - Download from https://git-scm.com/downloads

---

## Getting Started After Pulling the Project

### Step 1 — Open the Solution
- Open Visual Studio
- Click **File → Open → Project/Solution**
- Navigate to the project folder and open `TraineeScreeningTool.sln`
- Wait for the Solution Explorer to load — you should see two projects:
  - `TraineeScreeningTool` — the old web app, **IGNORE THIS COMPLETELY**
  - `TraineeScreeningTool.WinForms` — this is the app we are building, **work here only**

### Step 2 — Set Startup Project
- In Solution Explorer, right-click **TraineeScreeningTool.WinForms**
- Click **Set as Startup Project**
- It should go **bold** to confirm
- If you skip this step the wrong project will run

### Step 3 — Restore NuGet Packages
- In the top menu click **Build → Rebuild Solution**
- Visual Studio will automatically restore all NuGet packages including iText7 for PDF parsing
- Wait for it to finish — should say "Build succeeded" at the bottom

### Step 4 — Run the App
- Press **F5** or click the green Play button
- The app will automatically create the `trainees.db` database file on first run
- The login screen should appear

### Step 5 — First Time Login
- Default credentials for everyone:
  - **Username:** admin
  - **Password:** lifeworks123
- On your **very first login** you will be taken to an account setup screen
- You must enter your **first name, last name, email address** and set a **new password** before you can continue
- This only happens once — after setup you go straight to the dashboard on future logins

---

## Project Structure
TraineeScreeningTool.WinForms/
├── Data/
│   └── ApplicationDbContext.cs       — Database connection, configuration, seeding and logging
├── Models/
│   ├── Candidate.cs                  — Candidate data model with all assessment score fields
│   ├── User.cs                       — User account model (username, password hash, role, email)
│   ├── AppLog.cs                     — Activity log model for tracking user actions
│   ├── Certification.cs              — Certification tracking model (IN PROGRESS - Branson)
│   └── JobPlacement.cs               — Job placement tracking model (IN PROGRESS - Hamza)
├── AddCandidateForm.cs               — Form to manually add a new candidate
├── AssessForm.cs                     — Form to view and edit all candidate assessment scores
├── ChangePasswordForm.cs             — Form to change your password
├── DetailsForm.cs                    — Form to view a candidate's full scores and details
├── FirstTimeSetupForm.cs             — Account setup screen shown on very first login
├── ForgotPasswordForm.cs             — Forgot password screen with identity verification
├── ImportForm.cs                     — Form to import bulk assessment data from a CSV file
├── LoginForm.cs                      — Login screen shown on app startup
├── LogViewerForm.cs                  — View and clear activity logs
├── MainForm.cs                       — Main dashboard with candidate grid and all action buttons
├── PdfImportForm.cs                  — Form to import candidate results from 10 PDF reports
├── UserDetailsForm.cs                — View and update your account details and password
└── Program.cs                        — App entry point, database setup, and login flow

---

## Database Structure
The app uses a local SQLite database file called `trainees.db` stored in the build output folder.
It currently contains three tables:

- **Candidates** — stores all candidate records and their full assessment scores
- **Users** — stores staff login accounts with hashed passwords
- **AppLogs** — stores a log of all key actions performed in the app

Tables being added:
- **Certifications** — tracks certifications each candidate is pursuing or has completed (Branson)
- **JobPlacements** — tracks career placements and 6-month success evaluations (Hamza)

The database is created automatically on first run. Each team member has their own local copy.
Do not commit `trainees.db` to GitHub.

---

## How to Use the App

### First Time Login
1. Log in with the default credentials (admin / lifeworks123)
2. You will be automatically redirected to an account setup screen
3. Enter your first name, last name, email address and a new password
4. Click **Complete Setup** to proceed to the dashboard
5. On future logins you will go straight to the dashboard

### Forgot Password
1. On the login screen click **Forgot Password?**
2. Enter your username, full name and email address — all three must match what is on file
3. A temporary password will be displayed on screen
4. Log in with the temporary password
5. You will be required to change your password before you can access the dashboard

### Importing Assessment Data from PDFs (Primary Method)
The client receives 10 PDF reports per candidate from the Criteria assessment platform. Import them like this:
1. Log in to the app
2. Click **Import PDFs** on the dashboard
3. Click **Browse** and select all 10 PDF files for one candidate at a time
4. Click **Import PDFs**
5. The app will automatically read the Summary PDF and individual test PDFs and extract all scores
6. A placeholder email will be assigned — update it in the candidate record afterward
7. Repeat for each candidate

### Importing from CSV (Bulk Historical Data)
For importing historical data from the LIFE Works CSV export:
1. Click **Import CSV** on the dashboard
2. Click **Browse** and select the CSV file
3. Click **Import CSV**
4. Only rows with status **"Complete"** are imported
5. If a candidate with the same email already exists they will be updated not duplicated

### Adding a Candidate Manually
1. Click **Add Candidate** on the dashboard
2. Enter the candidate's first name, last name, and a valid email address
3. Click **Save**

### Entering or Editing Assessment Scores
1. Select a candidate from the grid
2. Click **Assess**
3. All existing scores will auto-populate in the form
4. Edit any scores if needed and click **Submit**
5. You can also edit scores directly in the grid — changes save automatically

### Viewing Candidate Details
1. Select a candidate from the grid
2. Click **Details** to view all scores organized by test section

### Searching for a Candidate
- Type a name or email in the search box at the top of the dashboard
- The grid will filter automatically as you type in real time

### Deleting Candidates
- Check the checkbox next to one or more candidates in the grid
- Click **Delete**
- Confirm the deletion in the popup
- Hold **Shift** and drag your mouse down over rows to quickly check multiple at once

### Viewing and Updating Your Account Details
1. Click **User Details** on the dashboard
2. View your username, role, and current details
3. Update your first name, last name or email and click **Update Details**
4. Click **Change Password** to change your password

### Viewing Activity Logs
- Click **View Logs** on the dashboard
- All logins, failed login attempts, imports, deletions and other actions are recorded here
- Logs are ordered most recent first
- Logs can be refreshed or cleared from this screen

---

## Troubleshooting

### "No such table: Candidates" error
This happens when the database file exists but has an old structure that doesn't match the current models.
To fix it:
1. Stop the app
2. Open File Explorer and navigate to:
   `TraineeScreeningTool.WinForms\bin\Debug\net9.0-windows`
3. Delete the `trainees.db` file
4. Run the app again with **F5** — it will recreate the database automatically
5. Re-import your data

### Database Structure Changed (After a teammate updates a model file)
This happens when someone adds or removes fields in `Candidate.cs`, `User.cs`, `AppLog.cs`, `Certification.cs`, or `JobPlacement.cs`.
The existing database won't match the new structure and the app will crash on startup.

To fix it temporarily during development:
1. Open **Program.cs** in the WinForms project
2. Add this line BEFORE `context.Database.EnsureCreated()`:
context.Database.EnsureDeleted();
3. Run the app once with **F5** — it will wipe and rebuild the database with the new structure
4. Stop the app immediately
5. **Remove that line** from Program.cs before doing anything else
6. Re-import your data

**Critical rules for EnsureDeleted:**
- **Never commit it to GitHub** — it wipes all data every time the app starts
- **Always remove it after exactly one run**
- **Always re-import data** after wiping the database
- This is a temporary development fix only

### NuGet packages not restoring
1. Right-click the solution in Solution Explorer
2. Click **Restore NuGet Packages**
3. Then do **Build → Rebuild Solution**

### Wrong project running
- Make sure `TraineeScreeningTool.WinForms` is **bold** in Solution Explorer
- If not, right-click it → **Set as Startup Project**

### Build errors after pulling latest changes
1. Do **Build → Rebuild Solution**
2. If errors persist, check if the database needs to be deleted and recreated (see above)
3. If still failing, check if a model file was updated and use EnsureDeleted to reset the database

---

## Default Login Credentials
| Username | Password | Role |
|---|---|---|
| admin | lifeworks123 | Admin |

**You will be required to complete account setup on your very first login!**

---

## Career Pathways
The following 10 career pathways are used by LIFE Works. These are used for job placement tracking and future analytics:

1. Carpentry
2. IT
3. Digital Marketing
4. Bookkeeping / Accounting
5. Automotive Technician
6. Manufacturing
7. Restaurant Management
8. Administrative Assistant
9. Customer Service / Sales
10. Landscape Technician

---

## How the Scoring System Works
The client (Joseph Ceasar) has requested a **data-driven approach** rather than fixed thresholds. Here is how it works:

**Phase 1 — Data Collection (Current)**
- Import candidate assessment results via PDF or CSV
- Track which certifications each candidate pursues and whether they pass or fail
- Track job placements and whether candidates stay for 6+ months

**Phase 2 — Pattern Recognition (Future)**
- Once enough candidates are tagged with certifications and placements, the app will calculate average scores for successful vs unsuccessful candidates
- This will reveal which assessment scores best predict success in each pathway
- For example: CCAT scores may predict success in IT and Bookkeeping; CMRA scores may predict success in Carpentry and Manufacturing

**There are no fixed score thresholds yet** — they will emerge naturally from the data over time.

---

## Assessment Score Fields
The app stores the following scores for each candidate:

| Score | Description |
|---|---|
| Talent Signal | Overall weighted score across all tests |
| CCAT Raw Score | Cognitive ability raw score |
| CCAT Overall % | Overall cognitive percentile |
| CCAT Math % | Math and logic percentile |
| CCAT Verbal % | Verbal ability percentile |
| CCAT Spatial % | Spatial reasoning percentile |
| CMRA % | Mechanical reasoning percentile |
| CBST Raw Score | Basic skills raw score |
| CBST Overall % | Basic skills overall percentile |
| CBST Math | Math raw score |
| CBST Verbal | Verbal raw score |
| CLIK Raw Score | Computer literacy raw score |
| CLIK Proficiency | Computer literacy proficiency level |
| Typing WPM | Words per minute |
| Typing Errors | Number of errors |
| Typing % | Typing overall percentile |
| CAST Overall % | Attention and concentration percentile |
| CAST Divided Attention | Divided attention percentile |
| CAST Filtering | Filtering percentile |
| CAST Reaction Time | Reaction time percentile |
| CAST Vigilance | Vigilance percentile |
| Word 365 Raw Score | Microsoft Word proficiency score |
| Word 365 Proficiency | Beginner / Foundational / Intermediate / Skilled / Advanced |
| Excel 365 Raw Score | Microsoft Excel proficiency score |
| Excel 365 Proficiency | Beginner / Foundational / Intermediate / Skilled / Advanced |
| CSAP Recommendation | Customer service aptitude recommendation |
| CSAP Achievement | Achievement percentile |
| CSAP Assertiveness | Assertiveness percentile |
| CSAP Cooperativeness | Cooperativeness percentile |
| CSAP Goal Orientation | Goal orientation percentile |
| CSAP Motivation | Motivation percentile |
| CSAP Team Player | Team player percentile |

---

## Team Task Assignments

### Gage Trevino — PDF Import (COMPLETE)
- Built PdfImportForm to import all 10 PDF reports per candidate
- Parses Summary PDF for all scores and CSAP PDF for personality scores
- Creates or updates candidate records automatically
- Built CSV import as secondary bulk import method

### Branson McLaughlin — Certification Tracking (IN PROGRESS)
- Create `Certification` model (candidate ID, cert name, status, pass/fail, date)
- Add common certifications list relevant to each pathway
- Build Add Certification and Edit Certification forms
- Update candidate view to show certifications
- Log all certification changes

### Hamza Quadri — Job Placement Tracking + 6-Month Evaluator (IN PROGRESS)
- Create `JobPlacement` model (candidate ID, career pathway, placement date, exit date, success flag)
- Build Add Placement form with the 10 career pathways as a dropdown
- Build 6-month evaluator that flags placements needing review
- Add dashboard notification for placements approaching 6 months
- Log all placement changes

### Jordan Charlie — Analytics Dashboard (IN PROGRESS — depends on Branson and Hamza)
- Build Analytics form showing average scores by certification and career pathway
- Show success vs failure score comparisons
- Show summary stats (total candidates, placements, certifications)
- Export analytics data to CSV

### Evan Ernst — Candidate Profile Page + PDF Viewer + UI Polish (IN PROGRESS — depends on all above)
- Build expanded Candidate Profile showing scores, certifications and placements in one place
- Add View PDFs button to reopen original PDF reports
- Store PDF file paths on import so they can be reopened
- Polish UI and fix any remaining spacing or clipping issues
- Update README with completed features

---

## Current Features
- Login with SHA256 password hashing and security
- First time login setup (first name, last name, email, new password)
- Forgot password with username, full name and email verification
- Temporary password generation with forced password change on next login
- Import candidate PDFs — reads all 10 Criteria assessment reports automatically
- Import bulk historical data from CSV
- Manually add candidates with email validation
- View and edit all assessment scores with auto-population
- Edit scores directly in the grid — saves automatically
- Search candidates by name or email in real time
- Checkbox selection with shift-drag for bulk delete
- Full candidate details view organized by test section
- Activity logging for all key actions
- View and update account details

## Features In Progress
- Certification tracking — tag candidates with certifications and pass/fail outcomes (Branson)
- Job placement tracking — record career placements and 6-month success evaluations (Hamza)
- Analytics dashboard — average scores by certification and pathway (Jordan)
- Candidate profile page — all data in one view (Evan)
- PDF viewer — reopen original PDF reports from within the app (Evan)
- Data export to CSV/Excel (Jordan)

---

## Notes for the Team
- **Do not edit** the `TraineeScreeningTool` web project — it is the old version and will be removed later
- **Always pull before you push** to avoid merge conflicts
- **Do not commit** the `trainees.db` file — it is in `.gitignore`
- **Do not commit** `EnsureDeleted` — remove it immediately after one test run
- **Build in order** — Branson and Hamza first, then Jordan, then Evan
- Every team member goes through first time setup on their first run — this is expected
- If you get database errors after pulling, delete `trainees.db` and re-import your data
- If you need help understanding the codebase paste this README into an AI assistant for context