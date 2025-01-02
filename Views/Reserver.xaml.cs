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
    public partial class Reserver : Window
    {
        public ObservableCollection<Room> AvailableRooms { get; set; }
        public Room SelectedRoom { get; set; }
        public Reservation NewReservation { get; set; }

        private readonly RoomDAO _roomDao;
        private readonly ReservationDAO _reservationDao;

        public Reserver()
        {
            InitializeComponent();

            // Initialiser DAO
            _roomDao = new RoomDAO();
            _reservationDao = new ReservationDAO();

            // Charger les données
            AvailableRooms = new ObservableCollection<Room>(_roomDao.GetAvailableRooms());
            NewReservation = new Reservation();

            // Configurer le DataContext
            DataContext = this;
        }

     private void SubmitReservation(object sender, RoutedEventArgs e)
{
    // Récupérer les valeurs directement des TextBox et DatePicker
    var clientNameTextBox = this.FindName("txtClientName") as TextBox;
    var clientEmailTextBox = this.FindName("txtClientEmail") as TextBox;
    var checkInDatePicker = this.FindName("dpCheckInDate") as DatePicker;
    var checkOutDatePicker = this.FindName("dpCheckOutDate") as DatePicker;

    if (SelectedRoom == null)
    {
        MessageBox.Show("Veuillez sélectionner une chambre.");
        return;
    }

    // Validation des champs de réservation
    if (clientNameTextBox == null || string.IsNullOrWhiteSpace(clientNameTextBox.Text) ||
        clientEmailTextBox == null || string.IsNullOrWhiteSpace(clientEmailTextBox.Text))
    {
        MessageBox.Show("Veuillez entrer le nom et l'email du client.");
        return;
    }

    if (checkInDatePicker == null || !checkInDatePicker.SelectedDate.HasValue ||
        checkOutDatePicker == null || !checkOutDatePicker.SelectedDate.HasValue)
    {
        MessageBox.Show("Veuillez sélectionner les dates d'arrivée et de départ.");
        return;
    }

    if (checkInDatePicker.SelectedDate >= checkOutDatePicker.SelectedDate)
    {
        MessageBox.Show("La date de départ doit être après la date d'arrivée.");
        return;
    }

    // Créer une nouvelle réservation
    var newReservation = new Reservation
    {
        ClientName = clientNameTextBox.Text,
        Email = clientEmailTextBox.Text,
        RoomId = SelectedRoom.Id,
        CheckInDate = checkInDatePicker.SelectedDate.Value,
        CheckOutDate = checkOutDatePicker.SelectedDate.Value
    };

    // Sauvegarder la réservation dans la base de données
    if (_reservationDao.Create(newReservation))
    {
        MessageBox.Show("Réservation réussie !");
        
        // Envoi de l'email à l'administrateur
        SendEmailToAdmin(newReservation);

        // Réinitialiser les champs après la soumission
        clientNameTextBox.Text = string.Empty;
        clientEmailTextBox.Text = string.Empty;
        checkInDatePicker.SelectedDate = null;
        checkOutDatePicker.SelectedDate = null;
    }
    else
    {
        MessageBox.Show("Erreur lors de la réservation.");
    }
}

private void SendEmailToAdmin(Reservation reservation)
{
    try
    {
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("se552733@gmail.com", "azerqsdftyuighjk"),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("se552733@gmail.com"),
            Subject = "Nouvelle réservation",
            Body = $"Une nouvelle réservation a été effectuée.\n\n" +
                   $"Nom du client: {reservation.ClientName}\n" +
                   $"Email du client: {reservation.Email}\n" +
                   $"Chambre réservée: {reservation.RoomId}\n" 
                
        };

        mailMessage.To.Add("se552733@gmail.com");

        smtpClient.Send(mailMessage);
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Erreur lors de l'envoi de l'email : {ex.Message}");
    }
}


    }
}
