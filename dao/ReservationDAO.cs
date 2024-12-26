using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using NomDuProjet.Models;
using NomDuProjet.DB;

namespace DAO
{
    public class ReservationDAO
    {
        // Création d'une réservation dans la base de données
        public bool Create(Reservation reservation)
        {
            const string query = "INSERT INTO Reservations (ClientId, RoomId, CheckInDate, CheckOutDate, Status) VALUES (@ClientId, @RoomId, @CheckInDate, @CheckOutDate, @Status)";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClientId", reservation.ClientId);
                    command.Parameters.AddWithValue("@RoomId", reservation.RoomId);
                    command.Parameters.AddWithValue("@CheckInDate", reservation.CheckInDate);
                    command.Parameters.AddWithValue("@CheckOutDate", reservation.CheckOutDate);
                    command.Parameters.AddWithValue("@Status", reservation.Status);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating reservation: {ex.Message}");
                return false;
            }
        }

        // Suppression d'une réservation de la base de données
        public bool Delete(Reservation reservation)
        {
            const string query = "DELETE FROM Reservations WHERE Id = @Id";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", reservation.Id);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting reservation: {ex.Message}");
                return false;
            }
        }

        // Mise à jour d'une réservation dans la base de données
        public bool Update(Reservation reservation)
        {
            const string query = "UPDATE Reservations SET ClientId = @ClientId, RoomId = @RoomId, CheckInDate = @CheckInDate, CheckOutDate = @CheckOutDate, Status = @Status WHERE Id = @Id";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", reservation.Id);
                    command.Parameters.AddWithValue("@ClientId", reservation.ClientId);
                    command.Parameters.AddWithValue("@RoomId", reservation.RoomId);
                    command.Parameters.AddWithValue("@CheckInDate", reservation.CheckInDate);
                    command.Parameters.AddWithValue("@CheckOutDate", reservation.CheckOutDate);
                    command.Parameters.AddWithValue("@Status", reservation.Status);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating reservation: {ex.Message}");
                return false;
            }
        }

        // Recherche d'une réservation par ID
        public Reservation FindById(int id)
        {
            const string query = "SELECT * FROM Reservations WHERE Id = @Id";
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
                            return new Reservation
                            {
                                Id = reader.GetInt32("Id"),
                                ClientId = reader.GetInt32("ClientId"),
                                RoomId = reader.GetInt32("RoomId"),
                                CheckInDate = reader.GetDateTime("CheckInDate"),
                                CheckOutDate = reader.GetDateTime("CheckOutDate"),
                                Status = reader.GetString("Status")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding reservation by ID: {ex.Message}");
            }

            return null;
        }

        // Récupérer toutes les réservations
        public List<Reservation> FindAll()
        {
            const string query = "SELECT * FROM Reservations";
            List<Reservation> reservations = new List<Reservation>();

            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservations.Add(new Reservation
                            {
                                Id = reader.GetInt32("Id"),
                                ClientId = reader.GetInt32("ClientId"),
                                RoomId = reader.GetInt32("RoomId"),
                                CheckInDate = reader.GetDateTime("CheckInDate"),
                                CheckOutDate = reader.GetDateTime("CheckOutDate"),
                                Status = reader.GetString("Status")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding all reservations: {ex.Message}");
            }

            return reservations;
        }
    }
}
