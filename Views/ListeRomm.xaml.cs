using System;
using System.Collections.ObjectModel;
using System.Windows;
using NomDuProjet.Models;
using DAO;
using System.Windows.Controls;
using System.Net;
using System.Net.Mail;

namespace NomDuProjet
{
    public partial class ListeRomm : Window
    {
        public ObservableCollection<Room> AvailableRooms { get; set; }
        public Room SelectedRoom { get; set; }
        public Reservation NewReservation { get; set; }

        private readonly RoomDAO _roomDao;
        private readonly ReservationDAO _reservationDao;

        public ListeRomm()
        {
            InitializeComponent();

            // Initialiser DAO
            _roomDao = new RoomDAO();
         //   _reservationDao = new ReservationDAO();

            // Charger les données
            AvailableRooms = new ObservableCollection<Room>(_roomDao.FindAll());
          ////  NewReservation = new Reservation();

            // Configurer le DataContext
            DataContext = this;
        }




    }
}
