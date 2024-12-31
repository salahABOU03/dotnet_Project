using System;
using System.Windows;
using DAO;
using NomDuProjet.Models;

namespace NomDuProjet.Views
{
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();
        }

        // Bouton Ajouter
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupération des données
            string firstName = FirstNameTextBox.Text.Trim();
            string lastName = LastNameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string phoneNumber = PhoneNumberTextBox.Text.Trim();

            // Validation des données
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Création de l'objet Client
            var client = new Client
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            };

            // Ajout dans la base de données via DAO
            var clientDAO = new ClientDAO();
            try
            {
                if (clientDAO.Create(client))
                {
                    MessageBox.Show("Client ajouté avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close(); // Fermer la fenêtre après succès
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'ajout du client.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Bouton Annuler
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Fermer la fenêtre
        }
    }
}
