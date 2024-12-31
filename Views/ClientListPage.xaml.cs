using System;
using System.Collections.Generic;
using System.Windows;
using DAO;
using NomDuProjet.Models;

namespace NomDuProjet.Views
{
    public partial class ClientListPage : Window
    {
        private readonly ClientDAO clientDAO;

        public ClientListPage()
        {
            InitializeComponent();
            clientDAO = new ClientDAO();
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newClient = new Client
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text,
                    PhoneNumber = txtPhoneNumber.Text
                };

                if (clientDAO.Create(newClient))
                {
                    MessageBox.Show("Client ajouté avec succès.");
                    LoadClients();
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'ajout du client.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
        }

        private void btnAfficher_Click(object sender, RoutedEventArgs e)
        {
            LoadClients();
        }

       private void btnSupprimer_Click(object sender, RoutedEventArgs e)
{
    try
    {
        // Validation de la saisie
        if (string.IsNullOrWhiteSpace(txtId.Text))
        {
            MessageBox.Show("Veuillez entrer un ID avant de continuer.", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Conversion avec gestion des exceptions
        int clientId = Convert.ToInt32(txtId.Text.Trim());

        // Suppression du client
        if (clientId > 0) // Vérifie que l'ID est valide (supérieur à zéro)
        {
            if (clientDAO.DeleteById(clientId))
            {
                MessageBox.Show("Client supprimé avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadClients(); // Rafraîchir la liste des clients
            }
            else
            {
                MessageBox.Show("Erreur lors de la suppression du client.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        else
        {
            MessageBox.Show("L'ID doit être un nombre entier positif.", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
    catch (FormatException)
    {
        MessageBox.Show("Veuillez entrer un ID numérique valide.", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Warning);
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Erreur inattendue : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}



        private void LoadClients()
        {
            var clients = clientDAO.FindAll();
            lstClients.Items.Clear();
            foreach (var client in clients)
            {
                lstClients.Items.Add($"{client.Id} - {client.FirstName} {client.LastName} ({client.Email})");
            }
        }

        private void txtId_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Optionnel : Charger un client par ID pour l'éditer
        }
    }
}
