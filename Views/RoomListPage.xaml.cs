using System;
using System.Collections.Generic;
using System.Windows;
using DAO;
using NomDuProjet.Models;

namespace NomDuProjet.Views
{
    public partial class RoomListPage : Window
    {
        private readonly RoomDAO roomDAO;

        public RoomListPage()
        {
            InitializeComponent();
            roomDAO = new RoomDAO();
        }

        private void btnAfficher_Click(object sender, RoutedEventArgs e)
        {
            LoadRooms();
        }

        private void LoadRooms()
        {
            var rooms = roomDAO.FindAll();
            lstRooms.Items.Clear();
            foreach (var room in rooms)
            {
                lstRooms.Items.Add($"Room {room.RoomNumber} - {room.RoomType} - {(room.IsAvailable ? "Available" : "Not Available")}");
            }
        }
    }
}
