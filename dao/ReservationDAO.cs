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
            const string query = "INSERT INTO Reservations (ClientName, RoomId, CheckInDate, CheckOutDate, Email) VALUES (@ClientName, @RoomId, @CheckInDate, @CheckOutDate, @Email)";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClientName", reservation.ClientName);
                    command.Parameters.AddWithValue("@RoomId", reservation.RoomId);
                    command.Parameters.AddWithValue("@CheckInDate", reservation.CheckInDate);
                    command.Parameters.AddWithValue("@CheckOutDate", reservation.CheckOutDate);
                    command.Parameters.AddWithValue("@Email", reservation.Email);

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
            const string query = "UPDATE Reservations SET ClientName = @ClientName, RoomId = @RoomId, CheckInDate = @CheckInDate, CheckOutDate = @CheckOutDate, Email = @Email WHERE Id = @Id";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", reservation.Id);
                    command.Parameters.AddWithValue("@ClientName", reservation.ClientName);
                    command.Parameters.AddWithValue("@RoomId", reservation.RoomId);
                    command.Parameters.AddWithValue("@CheckInDate", reservation.CheckInDate);
                    command.Parameters.AddWithValue("@CheckOutDate", reservation.CheckOutDate);
                    command.Parameters.AddWithValue("@Email", reservation.Email);

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
                                ClientName = reader.GetString("ClientName"),
                                RoomId = reader.GetInt32("RoomId"),
                                CheckInDate = reader.GetDateTime("CheckInDate"),
                                CheckOutDate = reader.GetDateTime("CheckOutDate"),
                                Email = reader.GetString("Email")
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
                                ClientName = reader.GetString("ClientName"),
                                RoomId = reader.GetInt32("RoomId"),
                                CheckInDate = reader.GetDateTime("CheckInDate"),
                                CheckOutDate = reader.GetDateTime("CheckOutDate"),
                                Email = reader.GetString("Email")
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
