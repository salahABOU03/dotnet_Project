using System;
using System.Windows;

namespace NomDuProjet.Views
{
    public partial class First : Window
    {
        public First()
        {
            InitializeComponent();
        }

        // Méthode appelée lorsque l'utilisateur clique sur "Continue as Client"
        private void ContinueAsClient_Click(object sender, RoutedEventArgs e)
        {
          //  MessageBox.Show("Continuing as Client...");

             Reserver manageReservations = new Reserver();
            manageReservations.Show();
            // Ajoutez ici la logique pour rediriger vers le client ou une autre fenêtre
        }

        // Méthode appelée lorsque l'utilisateur clique sur "Continue as Admin"
        private void ContinueAsAdmin_Click(object sender, RoutedEventArgs e)
        {
           // MessageBox.Show("Continuing as Admin...");
             LoginPage manageReservations = new LoginPage();
            manageReservations.Show();
            // Ajoutez ici la logique pour rediriger vers l'administration ou une autre fenêtre
        }
    }
}
