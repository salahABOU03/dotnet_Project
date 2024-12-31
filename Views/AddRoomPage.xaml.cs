using System;
using System.IO;
using System.Windows;
using Microsoft.Win32; // Pour la boîte de dialogue de sélection de fichier
using NomDuProjet.Models;
using DAO;

namespace NomDuProjet.Views
{
    public partial class AddRoomPage : Window
    {
        private readonly RoomDAO roomDAO;
        private byte[] roomImage; // Pour stocker l'image de la chambre

        public AddRoomPage()
        {
            InitializeComponent();
            roomDAO = new RoomDAO();
        }

        // Gérer le clic sur le bouton pour ajouter une chambre
        private void AddRoomButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validation des données
                if (string.IsNullOrWhiteSpace(RoomNumberTextBox.Text) || 
                    string.IsNullOrWhiteSpace(RoomTypeTextBox.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Création de l'objet Room
                var newRoom = new Room
                {
                    RoomNumber = int.Parse(RoomNumberTextBox.Text),
                    RoomType = RoomTypeTextBox.Text,
                    IsAvailable = IsAvailableCheckBox.IsChecked == true,
                    Image = roomImage
                };

                // Ajout dans la base de données
                if (roomDAO.Create(newRoom))
                {
                    MessageBox.Show("Chambre ajoutée avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'ajout de la chambre.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Veuillez entrer un numéro de chambre valide.", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Gérer le clic sur le bouton pour sélectionner une image
        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Fichiers image (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                roomImage = File.ReadAllBytes(openFileDialog.FileName);
                MessageBox.Show("Image sélectionnée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
