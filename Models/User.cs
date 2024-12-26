using System;

namespace NomDuProjet.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public string Role { get; set; } // e.g., Admin, Employee
    public DateTime CreatedAt { get; set; }
}
