namespace TraineeScreeningTool.WinForms.Models;

// Represents a single log entry in the database
public class AppLog
{
    public int Id { get; set; }

    // The username of whoever performed the action
    public string Username { get; set; } = string.Empty;

    // What action was performed (e.g. "Login", "Import", "Delete")
    public string Action { get; set; } = string.Empty;

    // Additional details about the action
    public string Details { get; set; } = string.Empty;

    // When the action happened
    public DateTime Timestamp { get; set; } = DateTime.Now;

    // Whether the action succeeded or failed
    public bool Success { get; set; } = true;
}