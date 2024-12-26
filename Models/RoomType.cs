using System;

namespace NomDuProjet.Models;

public class RoomType
{
    public int Id { get; set; }
    public string Name { get; set; } // e.g., Single, Double, Suite
    public string Description { get; set; }
    public decimal PricePerNight { get; set; }
}
