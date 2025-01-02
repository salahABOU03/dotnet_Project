    using NomDuProjet.Models;

    public class Reservation
    {
        public int Id { get; set; }
        public String ClientName { get; set; } // Association avec la classe Client
        public int RoomId { get; set; } // ID de la chambre réservée
        public DateTime? CheckInDate { get; set; } // Date d'arrivée (nullable)
        public DateTime? CheckOutDate { get; set; } // Date de départ (nullable)
        public string Email { get; set; } // Adresse e-mail associée à la réservation
    }
