namespace TraineeScreeningTool.WinForms.Models;

// Represents a candidate and all their assessment data
public class Candidate
{
    // Basic Info
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // CCAT Scores (Cognitive Ability Test)
    public int? CCATRawScore { get; set; }
    public int? CCATMathPercentile { get; set; }
    public int? CCATVerbalPercentile { get; set; }
    public int? CCATSpatialPercentile { get; set; }
    public int? CCATOverallPercentile { get; set; }

    // CBST Scores (Basic Skills Test)
    public int? CBSTRawScore { get; set; }
    public int? CBSTMathRaw { get; set; }
    public int? CBSTVerbalRaw { get; set; }
    public int? CBSTOverallPercentile { get; set; }

    // CMRA Score (Memory & Reasoning)
    public int? CMRAOverallPercentile { get; set; }

    // Typing Test
    public int? TypingWordsPerMinute { get; set; }
    public int? TypingErrors { get; set; }
    public int? TypingOverallPercentile { get; set; }

    // Microsoft Office Skills
    public string WordProficiency { get; set; } = string.Empty;
    public int? WordRawScore { get; set; }
    public string ExcelProficiency { get; set; } = string.Empty;
    public int? ExcelRawScore { get; set; }

    // CAST Scores (Attention & Reaction)
    public int? CASTOverallPercentile { get; set; }
    public int? CASTDividedAttention { get; set; }
    public int? CASTFiltering { get; set; }
    public int? CASTReactionTime { get; set; }
    public int? CASTVigilance { get; set; }

    // CSAP Scores (Personality/Sales)
    public string CSAPRecommendation { get; set; } = string.Empty;
    public int? CSAPAchievement { get; set; }
    public int? CSAPAssertiveness { get; set; }
    public int? CSAPCooperativeness { get; set; }
    public int? CSAPGoalOrientation { get; set; }
    public int? CSAPMotivation { get; set; }
    public int? CSAPTeamPlayer { get; set; }

    // Overall Talent Signal
    public int? TalentSignal { get; set; }

    // Result Fields - filled in by the scoring engine
    public string Recommendation { get; set; } = "Pending";
    public string ReadinessRating { get; set; } = "N/A";
    public string Explanation { get; set; } = "Assessment not yet completed.";

    // Helper property to display full name
    public string FullName => $"{FirstName} {LastName}";

    // Test date from PDF
    public string TestDate { get; set; } = string.Empty;

    // CLIK scores
    public string CLIKProficiency { get; set; } = string.Empty;
    public int? CLIKRawScore { get; set; }

    // Path to the folder containing the imported PDFs for this candidate
    public string? PdfFolderPath { get; set; }
}