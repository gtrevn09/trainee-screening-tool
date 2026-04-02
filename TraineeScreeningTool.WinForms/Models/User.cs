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

    // Whether this is the user's first time logging in
    public bool IsFirstLogin { get; set; } = true;

    // The user's full name
    public string FullName { get; set; } = string.Empty;

    // The user's email address - used for password reset
    public string Email { get; set; } = string.Empty;

    // Tracks whether the user needs to change their password
    public bool MustChangePassword { get; set; } = false;
}