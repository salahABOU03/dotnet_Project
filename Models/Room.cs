using System;

namespace NomDuProjet.Models;

public class Room
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public bool IsAvailable { get; set; }
        public byte[] Image { get; set; } // Nouvelle propriété pour l'image
    }
