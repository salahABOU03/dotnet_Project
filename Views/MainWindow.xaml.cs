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
        private void DownloadPdfButton_Click(object sender, RoutedEventArgs e)
        {
            // Chemin relatif du fichier dans le projet
            string sourcePath = @"tt.pdf"; // Le fichier doit être dans le dossier "Files" du projet

            // Vérifier si le fichier existe
            if (File.Exists(sourcePath))
            {
                // Ouvrir une boîte de dialogue pour choisir l'emplacement de sauvegarde
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    FileName = "monfichier.pdf", // Nom par défaut
                    Filter = "PDF Files|*.pdf"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string localPath = saveFileDialog.FileName;

                    try
                    {
                        // Copier le fichier du projet vers l'emplacement choisi
                        File.Copy(sourcePath, localPath, overwrite: true);
                        MessageBox.Show("Téléchargement terminé avec succès !");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Une erreur est survenue : {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Le fichier PDF n'a pas été trouvé dans le projet.");
            }
        }
    }

    