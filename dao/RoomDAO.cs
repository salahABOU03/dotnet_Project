using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using NomDuProjet.Models;
using NomDuProjet.DB;

namespace DAO
{
    public class RoomDAO
    {
        // Création d'une chambre dans la base de données
        public bool Create(Room room)
        {
            const string query = "INSERT INTO Rooms (RoomNumber, RoomTypeId, IsAvailable) VALUES (@RoomNumber, @RoomTypeId, @IsAvailable)";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                    command.Parameters.AddWithValue("@RoomTypeId", room.RoomTypeId);
                    command.Parameters.AddWithValue("@IsAvailable", room.IsAvailable);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating room: {ex.Message}");
                return false;
            }
        }

        // Suppression d'une chambre de la base de données
        public bool Delete(Room room)
        {
            const string query = "DELETE FROM Rooms WHERE Id = @Id";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", room.Id);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting room: {ex.Message}");
                return false;
            }
        }

        // Mise à jour d'une chambre dans la base de données
        public bool Update(Room room)
        {
            const string query = "UPDATE Rooms SET RoomNumber = @RoomNumber, RoomTypeId = @RoomTypeId, IsAvailable = @IsAvailable WHERE Id = @Id";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", room.Id);
                    command.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                    command.Parameters.AddWithValue("@RoomTypeId", room.RoomTypeId);
                    command.Parameters.AddWithValue("@IsAvailable", room.IsAvailable);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating room: {ex.Message}");
                return false;
            }
        }

        // Recherche d'une chambre par ID
        public Room FindById(int id)
        {
            const string query = "SELECT * FROM Rooms WHERE Id = @Id";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Room
                            {
                                Id = reader.GetInt32("Id"),
                                RoomNumber = reader.GetInt32("RoomNumber"),
                                RoomTypeId = reader.GetInt32("RoomTypeId"),
                                IsAvailable = reader.GetBoolean("IsAvailable")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding room by ID: {ex.Message}");
            }

            return null;
        }

        // Récupérer toutes les chambres
        public List<Room> FindAll()
        {
            const string query = "SELECT * FROM Rooms";
            List<Room> rooms = new List<Room>();

            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rooms.Add(new Room
                            {
                                Id = reader.GetInt32("Id"),
                                RoomNumber = reader.GetInt32("RoomNumber"),
                                RoomTypeId = reader.GetInt32("RoomTypeId"),
                                IsAvailable = reader.GetBoolean("IsAvailable")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding all rooms: {ex.Message}");
            }

            return rooms;
        }
    }
}
