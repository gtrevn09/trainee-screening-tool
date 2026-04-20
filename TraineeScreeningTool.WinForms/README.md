# LIFE Works Trainee Screening Tool

## Project Overview
A desktop application built with C# WinForms and SQLite that allows LIFE Works staff to import, manage, and analyze trainee assessment data — tracking certifications, job placements, and building data-driven career pathway recommendations over time.

**Tech Stack:** C#, WinForms, Entity Framework Core, SQLite, .NET 10.0, iText7 (PDF parsing)

---

## Prerequisites — Install These First

1. **Visual Studio Community 2022**
   - Download from https://visualstudio.microsoft.com/vs/community/
   - During installation, select the **.NET Desktop Development** workload
2. **.NET 10.0 SDK**
   - Download from https://dotnet.microsoft.com/download
3. **Git**
   - Download from https://git-scm.com/downloads

---

## Getting Started

### Step 1 — Open the Solution
- Open Visual Studio → **File → Open → Project/Solution**
- Open `TraineeScreeningTool.sln`
- In Solution Explorer you will see two projects:
  - `TraineeScreeningTool` — old web app, **ignore completely**
  - `TraineeScreeningTool.WinForms` — the active app, **work here only**

### Step 2 — Set Startup Project
- Right-click **TraineeScreeningTool.WinForms** → **Set as Startup Project**
- It should go **bold** to confirm

### Step 3 — Build & Run
- Click **Build → Rebuild Solution** (restores NuGet packages automatically)
- Press **F5** to run — the database is created automatically on first launch

### Step 4 — First Time Login
- Default credentials: **Username:** `admin` / **Password:** `lifeworks123`
- On your very first login you will be taken to an account setup screen
- Enter your first name, last name, email, and a new password, then click **Complete Setup**
- All future logins go straight to the dashboard

---

## Project Structure

```
TraineeScreeningTool.WinForms/
├── Data/
│   └── ApplicationDbContext.cs           — Database connection, seeding, and logging
├── Models/
│   ├── Candidate.cs                      — All candidate assessment score fields
│   ├── User.cs                           — Staff login accounts
│   ├── AppLog.cs                         — Activity log entries
│   ├── Certification.cs                  — Certification tracking per candidate
│   ├── JobPlacement.cs                   — Job placement tracking with 6-month evaluation
│   └── CareerPathways.cs                 — Career pathway list and associated certifications
├── AddCandidateForm.cs                   — Manually add a candidate
├── AddCertificationForm.cs               — Add a certification to a candidate
├── AddPlacementForm.cs                   — Record a new job placement
├── AnalyticsForm.cs                      — Analytics dashboard (scores by pathway, cert outcomes)
├── AssessForm.cs                         — View and edit candidate assessment scores
├── CandidateProfileForm.cs               — Full candidate view (scores, certs, placements)
├── ChangePasswordForm.cs                 — Change password
├── DetailsForm.cs                        — Read-only score detail view
├── EditCertificationForm.cs              — Edit or delete a certification
├── FirstTimeSetupForm.cs                 — Account setup on first login
├── ForgotPasswordForm.cs                 — Forgot password with identity verification
├── ImportForm.cs                         — Bulk CSV import
├── JobPlacementsForm.cs                  — All placements across all candidates
├── LoginForm.cs                          — Login screen
├── LogViewerForm.cs                      — View and clear activity logs
├── MainForm.cs                           — Main dashboard
├── PdfImportForm.cs                      — Import candidate results from 10 Criteria PDFs
├── PdfSelectForm.cs                      — Pick which PDF to open when multiple are linked
├── UpdatePlacementForm.cs                — Edit an existing placement
├── UserDetailsForm.cs                    — View and update account details
├── UserGuideForm.cs                      — In-app searchable user guide
└── Program.cs                            — App entry point, database setup, login flow
```

---

## Database

The app uses a local SQLite file called `trainees.db` stored in the build output folder. It is created automatically on first run.

**Tables:**

| Table | Purpose |
|---|---|
| Candidates | All candidate records and assessment scores |
| Users | Staff login accounts with hashed passwords |
| AppLogs | Audit log of all key actions |
| Certifications | Certifications each candidate is pursuing or has completed |
| JobPlacements | Career placements with 6-month success tracking |

> **Do not commit `trainees.db` to GitHub** — it is in `.gitignore` and each team member keeps their own local copy.

---

## Features

### Candidates
- Import from Criteria PDF reports (10 PDFs per candidate, reads all scores automatically)
- Import bulk historical data from CSV export
- Manually add candidates
- Edit scores directly in the dashboard grid — saves automatically
- Real-time search by name or email
- Checkbox + shift-drag for bulk delete

### Candidate Profile
- Single tabbed window showing a candidate's scores, certifications, and placements together
- Reopen original Criteria PDF reports from within the app

### Certifications
- Track certifications each candidate is pursuing or has completed
- Per-pathway certification suggestions (10 pathways supported)
- Pass / Fail outcomes with color-coded rows
- Add, edit, and delete from the Candidate Profile

### Job Placements
- Record career placements with pathway, start date, and exit date
- 6-month evaluation system — automatically flags placements approaching or past the milestone
- Yellow dashboard notification banner when placements need review
- Mark placements successful once confirmed

### Analytics
- Average scores grouped by career pathway
- Certification pass/fail rates per certification
- Pass vs. fail score comparisons across the candidate pool
- Export any analytics view to CSV

### Account & Security
- SHA256 password hashing
- First-time login setup (name, email, new password)
- Forgot password with username + full name + email verification
- Forced password change on first login and after a reset
- Activity logging for all key actions (logins, imports, deletions, changes)

### In-App User Guide
- Searchable guide covering all features
- Accessible from the dashboard via the **User Guide** button

---

## Default Login Credentials

| Username | Password | Role |
|---|---|---|
| admin | lifeworks123 | Admin |

> You will be required to complete account setup on your very first login.

---

## Career Pathways

The following 10 pathways are used for placement tracking and certification suggestions:

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

---

## Assessment Scores Tracked

| Score | Description |
|---|---|
| Talent Signal | Overall weighted score across all tests |
| CCAT Raw / Overall % | Cognitive ability |
| CCAT Math / Verbal / Spatial % | Cognitive sub-scores |
| CMRA % | Mechanical reasoning |
| CBST Raw / Overall % | Basic skills |
| CBST Math / Verbal | Basic skills sub-scores |
| CLIK Raw / Proficiency | Computer literacy |
| Typing WPM / Errors / % | Typing speed and accuracy |
| CAST Overall / Sub-scores % | Attention and concentration |
| Word 365 Raw / Proficiency | Microsoft Word |
| Excel 365 Raw / Proficiency | Microsoft Excel |
| CSAP Recommendation | Customer service aptitude recommendation |
| CSAP Sub-scores | Achievement, Assertiveness, Cooperativeness, Goal Orientation, Motivation, Team Player |

---

## Troubleshooting

### App crashes on startup / "No such table" error
The database file is outdated. Delete `trainees.db` from the build output folder and rerun the app — it will recreate the database automatically.

Build output location:
```
TraineeScreeningTool.WinForms\bin\Debug\net10.0-windows\
```

### Database structure changed after pulling
If a model file (`Candidate.cs`, `User.cs`, etc.) was updated by a teammate, the existing database won't match. Fix:

1. Open `Program.cs`
2. Add `context.Database.EnsureDeleted();` **before** `context.Database.EnsureCreated();`
3. Run once with F5 — the database is wiped and rebuilt
4. **Immediately remove that line** before doing anything else
5. Re-import your data

> ⚠ Never commit `EnsureDeleted` — it wipes all data on every launch.

### NuGet packages not restoring
Right-click the solution → **Restore NuGet Packages**, then **Build → Rebuild Solution**.

### Wrong project running
Right-click `TraineeScreeningTool.WinForms` in Solution Explorer → **Set as Startup Project**.

---

## Notes

- Do not edit the `TraineeScreeningTool` web project — it is the old version and will be removed
- Always pull before you push
- Do not commit `trainees.db`
- Do not commit `EnsureDeleted`
