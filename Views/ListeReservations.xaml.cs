using System.Collections.ObjectModel;
using System.Windows;
using NomDuProjet.Models; // Assurez-vous que le namespace est correct.
using DAO; // Inclut ReservationDAO pour interagir avec la base de données.

namespace NomDuProjet
{
    public partial class ListeReservations : Window
    {
        // Collection observable pour afficher les réservations dans la ListView.
        public ObservableCollection<Reservation> Reservations { get; set; }
        public Reservation SelectedReservation { get; set; }

        private ReservationDAO reservationDAO;

        public ListeReservations()
        {
            InitializeComponent();

            // Initialisation du DAO pour interagir avec la base de données.
            reservationDAO = new ReservationDAO();

            // Chargement des réservations.
            LoadReservations();

            // Définition du contexte de données pour le binding.
            DataContext = this;
        }

        private void LoadReservations()
        {
            // Récupération des réservations depuis la base de données.
            var reservationsFromDb = reservationDAO.FindAll();

            // Initialisation de la collection observable.
            Reservations = new ObservableCollection<Reservation>(reservationsFromDb);
        }
    }
}
