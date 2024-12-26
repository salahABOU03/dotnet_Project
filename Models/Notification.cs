using System;

namespace NomDuProjet.Models;

public class Notification
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public DateTime SentDate { get; set; }
    public bool IsSent { get; set; }
}
