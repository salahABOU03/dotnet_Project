using System.Collections.ObjectModel;
using System.Windows.Input;
using DAO;
using NomDuProjet.Models; // Assurez-vous que le namespace est correct

public class RoomViewModel
{
    public ObservableCollection<Room> AvailableRooms { get; set; }

    // Utilisation d'un Action directement pour la commande
    public ICommand LoadRoomsCommand { get; set; }

    public RoomViewModel()
    {
        AvailableRooms = new ObservableCollection<Room>();
        
        // Assignation de la commande LoadRoomsCommand
        LoadRoomsCommand = new Command(LoadAvailableRooms);
    }

    private void LoadAvailableRooms()
    {
        // Appel à la méthode GetAvailableRooms pour récupérer les chambres disponibles
        var roomDAO = new RoomDAO();
        var rooms = roomDAO.GetAvailableRooms();

        AvailableRooms.Clear();
        foreach (var room in rooms)
        {
            AvailableRooms.Add(room);
        }
    }
}

// Commande simplifiée (pas besoin d'une classe RelayCommand)
public class Command : ICommand
{
    private readonly Action _execute;

    public Command(Action execute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter)
    {
        _execute();
    }
}
