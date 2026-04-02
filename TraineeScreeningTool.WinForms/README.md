LIFE Works Trainee Screening Tool
Project Overview
A desktop application built with C# WinForms and SQLite that allows LIFE Works staff to import, manage, and analyze trainee assessment data to recommend career pathways.
Built by: Gage Trevino, Hamza Quadri, Jordan Charlie, Evan Ernst, Branson McLaughlin

Prerequisites — Install These First
Before pulling the project, make sure you have the following installed:

Visual Studio Community 2022

Download from https://visualstudio.microsoft.com/vs/community/
During installation, select the .NET Desktop Development workload


.NET 9.0 SDK

Download from https://dotnet.microsoft.com/download


Git

Download from https://git-scm.com/downloads




Getting Started After Pulling the Project
Step 1 — Open the Solution

Open Visual Studio
Click File → Open → Project/Solution
Navigate to the project folder and open TraineeScreeningTool.sln
Wait for the Solution Explorer to load — you should see two projects:

TraineeScreeningTool — the old web app, ignore this
TraineeScreeningTool.WinForms — this is the app we are building, work here only



Step 2 — Set Startup Project

In Solution Explorer, right-click TraineeScreeningTool.WinForms
Click Set as Startup Project
It should go bold to confirm

Step 3 — Restore NuGet Packages

In the top menu click Build → Rebuild Solution
Visual Studio will automatically restore all NuGet packages
Wait for it to finish — should say "Build succeeded" at the bottom

Step 4 — Run the App

Press F5 or click the green Play button
The app will automatically create the database on first run
The login screen should appear

Step 5 — Log In

Default credentials for everyone:

Username: admin
Password: lifeworks123


Important: Change your password after your first login using the "Change Password" button


Project Structure
TraineeScreeningTool.WinForms/
├── Data/
│   └── ApplicationDbContext.cs    — Database connection and configuration
├── Models/
│   ├── Candidate.cs               — Candidate data model with all assessment fields
│   ├── User.cs                    — User account model
│   └── AppLog.cs                  — Activity log model
├── AddCandidateForm.cs            — Form to manually add a new candidate
├── AssessForm.cs                  — Form to view and edit assessment scores
├── ChangePasswordForm.cs          — Form to change your password
├── DetailsForm.cs                 — Form to view candidate results and recommendation
├── ImportForm.cs                  — Form to import assessment data from CSV
├── LoginForm.cs                   — Login screen
├── LogViewerForm.cs               — View activity logs
├── MainForm.cs                    — Main dashboard
└── Program.cs                     — App entry point

How to Use the App
Importing Assessment Data

Log in to the app
Click Import CSV on the dashboard
Click Browse and select the CSV file exported from the assessment platform
Click Import CSV
A summary will show how many candidates were imported, skipped, and any errors
Only rows with status "Complete" are imported — incomplete assessments are skipped automatically

Adding a Candidate Manually

Click Add Candidate on the dashboard
Enter the candidate's first name, last name, and a valid email address
Click Save

Entering Assessment Scores

Select a candidate from the grid
Click Assess
If the candidate was imported from CSV their scores will auto-populate
Edit any scores if needed and click Submit

Viewing Candidate Details

Select a candidate from the grid
Click Details to view all their scores and recommendation

Searching for a Candidate

Type a name or email in the search box at the top of the dashboard
The grid will filter automatically as you type

Deleting a Candidate

Select a candidate from the grid
Click Delete
Confirm the deletion in the popup

Viewing Activity Logs

Click View Logs on the dashboard
All logins, imports, deletions and other actions are recorded here
Logs can be refreshed or cleared from this screen


Troubleshooting
"No such table: Candidates" error
This happens when the database structure has changed. To fix it:

Stop the app
Open File Explorer and navigate to:
TraineeScreeningTool.WinForms\bin\Debug\net9.0-windows
Delete the trainees.db file
Run the app again with F5
Re-import the CSV file

NuGet packages not restoring

Right-click the solution in Solution Explorer
Click Restore NuGet Packages
Then do Build → Rebuild Solution

Wrong project running

Make sure TraineeScreeningTool.WinForms is bold in Solution Explorer
If not, right-click it → Set as Startup Project

Build errors after pulling latest changes

Do Build → Rebuild Solution
If errors persist, check if the database needs to be deleted (see above)
If still failing, compare your Candidate.cs model with the latest version


Default Login Credentials
UsernamePasswordRoleadminlifeworks123Admin
Change your password on first login!

Current Features

Login with password hashing and security
Import assessment CSV from the LIFE Works assessment platform
Manually add candidates
View and edit assessment scores (CCAT, CBST, CMRA, Typing, and more)
Search candidates by name or email
View candidate details and recommendations
Activity logging for all key actions
Change password

Features In Progress

Scoring engine and career pathway recommendations (waiting on client criteria)
PDF export of candidate results
Reporting dashboard


************** Notes for the Team *******************

Do not edit the TraineeScreeningTool web project — it is the old version and will be removed later
Always pull before you push to avoid merge conflicts
Do not commit the trainees.db file — it is in .gitignore and each person has their own local copy
The scoring algorithm will be added once the client (Joseph) responds with the career pathway thresholds
Default password should be changed by each team member on first run