namespace TraineeScreeningTool.WinForms.Models;

// Represents a certification a candidate is pursuing or has completed
public class Certification
{
    public int Id { get; set; }

    // The candidate this certification belongs to
    public int CandidateId { get; set; }
    public Candidate? Candidate { get; set; }

    // Name of the certification (e.g. "CompTIA Network+", "QuickBooks Certified User")
    public string CertName { get; set; } = string.Empty;

    // "Pursuing" or "Completed"
    public string Status { get; set; } = "Pursuing";

    // "Pass", "Fail", or null if still in progress
    public string? Result { get; set; }

    // Date pursued (start date) or completed (exam date)
    public DateTime? Date { get; set; }

    // Notes about the certification
    public string Notes { get; set; } = string.Empty;

    // Convenience display for grids
    public string DisplayResult => Result ?? "—";
    public string DisplayDate => Date?.ToString("MMM d, yyyy") ?? "—";
}