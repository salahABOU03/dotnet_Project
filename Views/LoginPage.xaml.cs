using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace NomDuProjet.Views
{
    public partial class LoginPage : Window
    {
        private static readonly string connectionString = "Server=localhost;Database=test;User ID=root;Password=;Pooling=true;";

        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        { 
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;  // Vérifiez que le nom est correct ici

            if (AuthenticateUser(username, password))
            {
                MainWindow mainWindow = new MainWindow(); // Remplacez par la page à ouvrir
                mainWindow.Show();
                this.Close(); // Fermer la page de connexion
             //   MessageBox.Show("Connexion réussie!", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                // Rediriger vers la page suivante ou la fenêtre principale ici
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM users WHERE Username = @Username AND Password = @Password";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        
                        cmd.Parameters.AddWithValue("@Password", password);

                        int userCount = Convert.ToInt32(cmd.ExecuteScalar());
                        return true ; 
                        //return userCount > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion à la base de données: {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
   }
}
