using System;

namespace NomDuProjet.Models;

public class Room
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public int RoomTypeId { get; set; }
    public RoomType RoomType { get; set; }
    public bool IsAvailable { get; set; }
}
