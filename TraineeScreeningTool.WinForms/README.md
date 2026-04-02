# LIFE Works Trainee Screening Tool

## Project Overview
A desktop application built with C# WinForms and SQLite that allows LIFE Works staff to import, manage, and analyze trainee assessment data to recommend career pathways.

**Built by:** Gage Trevino, Hamza Quadri, Jordan Charlie, Evan Ernst, Branson McLaughlin

**Tech Stack:** C#, WinForms, Entity Framework Core, SQLite, .NET 9.0

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
- Visual Studio will automatically restore all NuGet packages
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
│   └── AppLog.cs                     — Activity log model for tracking user actions
├── AddCandidateForm.cs               — Form to manually add a new candidate
├── AssessForm.cs                     — Form to view and edit a candidate's assessment scores
├── ChangePasswordForm.cs             — Form to change your password
├── DetailsForm.cs                    — Form to view a candidate's full details and recommendation
├── FirstTimeSetupForm.cs             — Account setup screen shown on very first login
├── ForgotPasswordForm.cs             — Forgot password screen with identity verification
├── ImportForm.cs                     — Form to import bulk assessment data from a CSV file
├── LoginForm.cs                      — Login screen shown on app startup
├── LogViewerForm.cs                  — View and clear activity logs
├── MainForm.cs                       — Main dashboard with candidate grid and all action buttons
├── UserDetailsForm.cs                — View and update your account details and password
└── Program.cs                        — App entry point, database setup, and login flow

---

## Database Structure
The app uses a local SQLite database file called `trainees.db` stored in the build output folder.
It contains three tables:

- **Candidates** — stores all candidate records and their assessment scores
- **Users** — stores staff login accounts with hashed passwords
- **AppLogs** — stores a log of all key actions performed in the app

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

### Importing Assessment Data
1. Log in to the app
2. Click **Import CSV** on the dashboard
3. Click **Browse** and select the CSV file exported from the LIFE Works assessment platform
4. Click **Import CSV**
5. A summary will show how many candidates were imported, skipped, and any errors
6. Only rows with status **"Complete"** are imported — incomplete assessments are skipped automatically
7. If a candidate with the same email already exists they will be updated not duplicated

### Adding a Candidate Manually
1. Click **Add Candidate** on the dashboard
2. Enter the candidate's first name, last name, and a valid email address
3. Click **Save**

### Entering Assessment Scores
1. Select a candidate from the grid
2. Click **Assess**
3. If the candidate was imported from CSV their scores will auto-populate in the form
4. Edit any scores if needed and click **Submit**

### Viewing Candidate Details
1. Select a candidate from the grid
2. Click **Details** to view all their scores and recommendation

### Searching for a Candidate
- Type a name or email in the search box at the top of the dashboard
- The grid will filter automatically as you type in real time

### Deleting Candidates
- Check the checkbox next to one or more candidates in the grid
- Click **Delete**
- Confirm the deletion in the popup
- Hold **Shift** and drag your mouse down over multiple rows to quickly check them all at once

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
5. Re-import the CSV file

### Database Structure Changed (After a teammate updates a model file)
This happens when someone adds or removes fields in `Candidate.cs`, `User.cs`, or `AppLog.cs`.
The existing database won't match the new structure and the app will crash on startup.

To fix it temporarily during development:
1. Open **Program.cs** in the WinForms project
2. Add this line BEFORE `context.Database.EnsureCreated()`:
context.Database.EnsureDeleted();
3. Run the app once with **F5** — it will wipe and rebuild the database with the new structure
4. Stop the app immediately
5. **Remove that line** from Program.cs before doing anything else
6. Re-import the CSV file

**Critical rules for EnsureDeleted:**
- **Never commit it to GitHub** — it wipes all data every time the app starts
- **Always remove it after exactly one run**
- **Always re-import the CSV** after wiping the database
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

## Assessment Score Fields
The app stores the following assessment scores for each candidate imported from the LIFE Works CSV:

| Score | Description |
|---|---|
| CCAT Raw Score | Cognitive ability overall raw score |
| CCAT Math % | Math and logic percentile |
| CCAT Verbal % | Verbal ability percentile |
| CCAT Spatial % | Spatial reasoning percentile |
| CCAT Overall % | Overall cognitive percentile |
| CBST Raw Score | Basic skills overall raw score |
| CBST Math | Math raw score |
| CBST Verbal | Verbal raw score |
| CBST Overall % | Basic skills overall percentile |
| CMRA % | Memory and reasoning percentile |
| Typing WPM | Adjusted words per minute |
| Typing Errors | Number of typing errors |
| Typing % | Typing overall percentile |
| Word Proficiency | Microsoft Word 365 proficiency level |
| Word Raw Score | Microsoft Word 365 raw score |
| Excel Proficiency | Microsoft Excel 365 proficiency level |
| Excel Raw Score | Microsoft Excel 365 raw score |
| CAST Overall % | Attention and reaction time percentile |
| CAST Divided Attention | Divided attention percentile |
| CAST Filtering | Filtering percentile |
| CAST Reaction Time | Reaction time percentile |
| CAST Vigilance | Vigilance percentile |
| CSAP Recommendation | Sales aptitude overall recommendation |
| CSAP Achievement | Achievement raw score |
| CSAP Assertiveness | Assertiveness raw score |
| CSAP Cooperativeness | Cooperativeness raw score |
| CSAP Goal Orientation | Goal orientation raw score |
| CSAP Motivation | Motivation raw score |
| CSAP Team Player | Team player raw score |
| Talent Signal | Overall talent signal score |

---

## Current Features
- Login with SHA256 password hashing and security
- First time login setup (first name, last name, email, new password)
- Forgot password with username, full name and email verification
- Temporary password generation with forced password change on next login
- Import assessment CSV from the LIFE Works assessment platform
- Manually add candidates with email validation
- View and edit assessment scores with auto-population from CSV data
- Search candidates by name or email in real time
- Checkbox selection with shift-drag for bulk operations
- Delete single or multiple candidates with confirmation
- View full candidate details and recommendations
- Activity logging for all key actions (login, import, delete, logout, etc)
- View and update your own account details
- Responsive layout that resizes with the window

## Features In Progress
- Scoring engine and career pathway recommendations (waiting on client criteria from Joseph)
- PDF export of candidate results
- Reporting dashboard with summary statistics

---

## Notes for the Team
- **Do not edit** the `TraineeScreeningTool` web project — it is the old version and will be removed later
- **Always pull before you push** to avoid merge conflicts
- **Do not commit** the `trainees.db` file — it is in `.gitignore` and each person has their own local copy
- **Do not commit** `EnsureDeleted` — remove it immediately after one test run
- The scoring algorithm will be built once the client (Joseph Ceasar) responds with career pathway thresholds
- Every team member will go through the first time setup flow on their first run — this is expected
- If you get database errors after pulling, delete `trainees.db` and re-import the CSV
