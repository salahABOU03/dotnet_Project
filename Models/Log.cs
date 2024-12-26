using System;

namespace NomDuProjet.Models;

public class Log
{
    public int Id { get; set; }
    public string Level { get; set; } // e.g., Info, Error, Warning
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
    
}
