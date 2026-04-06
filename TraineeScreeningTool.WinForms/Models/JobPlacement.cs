namespace TraineeScreeningTool.WinForms.Models;

public class JobPlacement
{
    public int Id { get; set; }

    // The candidate this placement belongs to
    public int CandidateId { get; set; }
    public Candidate? Candidate { get; set; }

    // Which of the 10 career pathways they were placed into
    public string CareerPathway { get; set; } = string.Empty;

    // Date they started in the position
    public DateTime PlacementDate { get; set; } = DateTime.Today;

    // Date they left the position (null if still active)
    public DateTime? ExitDate { get; set; }

    // True when the person has stayed 6+ months (manually confirmed or auto-flagged)
    public bool IsSuccessful { get; set; } = false;

    // How many months have elapsed since placement (using today or exit date)
    public double MonthsPlaced
    {
        get
        {
            var endDate = ExitDate ?? DateTime.Today;
            return (endDate - PlacementDate).TotalDays / 30.44;
        }
    }

    // Active = no exit date recorded yet
    public bool IsActive => ExitDate == null;

    // Needs review = active placement approaching or past 6-month milestone
    public bool NeedsReview => IsActive && !IsSuccessful && MonthsPlaced >= 5.5;

    // Convenience label for grid display
    public string Status
    {
        get
        {
            if (IsSuccessful) return "Successful";
            if (!IsActive) return "Exited (< 6 months)";
            if (MonthsPlaced >= 6) return "Past 6 Months – Review";
            if (MonthsPlaced >= 5.5) return "Approaching 6 Months";
            return "Active";
        }
    }
}
