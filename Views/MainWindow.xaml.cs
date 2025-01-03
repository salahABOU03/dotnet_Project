using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace NomDuProjet.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
    {


       
        public MainWindow()
        {
             
            InitializeComponent();
        }
    
         private void ViewClients_Click(object sender, RoutedEventArgs e)
        {
            ClientListPage clientListPage = new ClientListPage();
            clientListPage.Show();
        }
    private void ViewReservations_Click(object sender, RoutedEventArgs e)
        {
            ListeReservations listeReservations = new ListeReservations();
            listeReservations.Show();
        }

  private void ViewRooms_Click(object sender, RoutedEventArgs e)
        {
            ListeRomm listeRomm = new ListeRomm();
            listeRomm.Show();
        }

        private void ManageClients_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.Show();
        }

         private void ManageReservations_Click(object sender, RoutedEventArgs e)
        {
            ListeReservations manageReservations = new ListeReservations();
            manageReservations.Show();
        }

        private void Reserver_Click(object sender, RoutedEventArgs e)
        {
            Reserver manageReservations = new Reserver();
            manageReservations.Show();
        }

         private void AddRoomPage_Click(object sender, RoutedEventArgs e)
        {
            AddRoomPage manageReservations = new AddRoomPage();
            manageReservations.Show();
        }
// Méthode pour télécharger le fichier PDF depuis le projet
      private void DownloadFileButton_Click(object sender, RoutedEventArgs e)
{
    // Chemin absolu vers le fichier dans le dossier Views
    string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Views", "tt.pdf");

    // Vérifiez si le fichier existe
    if (!File.Exists(filePath))
    {
        MessageBox.Show($"Le fichier n'existe pas à l'emplacement : {filePath}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
    }

    // Boîte de dialogue pour choisir l'emplacement de sauvegarde
    SaveFileDialog saveFileDialog = new SaveFileDialog
    {
        FileName = "tt.pdf", // Nom de fichier par défaut
        Filter = "Fichiers PDF (*.pdf)|*.pdf"
    };

    if (saveFileDialog.ShowDialog() == true)
    {
        string localPath = saveFileDialog.FileName;

        try
        {
            // Copier le fichier vers l'emplacement choisi par l'utilisateur
            File.Copy(filePath, localPath, overwrite: true);
            MessageBox.Show("Le fichier a été téléchargé avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erreur lors du téléchargement : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}}