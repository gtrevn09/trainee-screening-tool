namespace TraineeScreeningTool.Models;

public class Candidate {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    // Assessment Scores
    public double ITScore { get; set; }
    public double CarpentryScore { get; set; }
    public double BookkeepingScore { get; set; }
    
    // Result Fields (Step 5 & 6)
    public string Recommendation { get; set; } = "Pending";
    public string ReadinessRating { get; set; } = "N/A";
    public string Explanation { get; set; } = "Assessment not yet completed.";
}