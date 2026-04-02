namespace TraineeScreeningTool.WinForms.Models;

// Represents a staff user who can log into the application
public class User
{
    public int Id { get; set; }

    // The username used to log in
    public string Username { get; set; } = string.Empty;

    // The password stored as a hashed value for security
    public string PasswordHash { get; set; } = string.Empty;

    // The role of the user - either "Admin" or "Staff"
    public string Role { get; set; } = "Staff";

    // Whether this account is active or disabled
    public bool IsActive { get; set; } = true;
}