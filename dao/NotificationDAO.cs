using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using NomDuProjet.Models;
using NomDuProjet.DB;

namespace DAO
{
    public class NotificationDAO
    {
        // Création d'une notification dans la base de données
        public bool Create(Notification notification)
        {
            const string query = "INSERT INTO Notifications (Email, Subject, Message, SentDate, IsSent) VALUES (@Email, @Subject, @Message, @SentDate, @IsSent)";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", notification.Email);
                    command.Parameters.AddWithValue("@Subject", notification.Subject);
                    command.Parameters.AddWithValue("@Message", notification.Message);
                    command.Parameters.AddWithValue("@SentDate", notification.SentDate);
                    command.Parameters.AddWithValue("@IsSent", notification.IsSent);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating notification: {ex.Message}");
                return false;
            }
        }

        // Suppression d'une notification de la base de données
        public bool Delete(Notification notification)
        {
            const string query = "DELETE FROM Notifications WHERE Id = @Id";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", notification.Id);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting notification: {ex.Message}");
                return false;
            }
        }

        // Mise à jour d'une notification dans la base de données
        public bool Update(Notification notification)
        {
            const string query = "UPDATE Notifications SET Email = @Email, Subject = @Subject, Message = @Message, SentDate = @SentDate, IsSent = @IsSent WHERE Id = @Id";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", notification.Id);
                    command.Parameters.AddWithValue("@Email", notification.Email);
                    command.Parameters.AddWithValue("@Subject", notification.Subject);
                    command.Parameters.AddWithValue("@Message", notification.Message);
                    command.Parameters.AddWithValue("@SentDate", notification.SentDate);
                    command.Parameters.AddWithValue("@IsSent", notification.IsSent);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating notification: {ex.Message}");
                return false;
            }
        }

        // Recherche d'une notification par ID
        public Notification FindById(int id)
        {
            const string query = "SELECT * FROM Notifications WHERE Id = @Id";
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
                            return new Notification
                            {
                                Id = reader.GetInt32("Id"),
                                Email = reader.GetString("Email"),
                                Subject = reader.GetString("Subject"),
                                Message = reader.GetString("Message"),
                                SentDate = reader.GetDateTime("SentDate"),
                                IsSent = reader.GetBoolean("IsSent")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding notification by ID: {ex.Message}");
            }

            return null;
        }

        // Récupérer toutes les notifications
        public List<Notification> FindAll()
        {
            const string query = "SELECT * FROM Notifications";
            List<Notification> notifications = new List<Notification>();

            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            notifications.Add(new Notification
                            {
                                Id = reader.GetInt32("Id"),
                                Email = reader.GetString("Email"),
                                Subject = reader.GetString("Subject"),
                                Message = reader.GetString("Message"),
                                SentDate = reader.GetDateTime("SentDate"),
                                IsSent = reader.GetBoolean("IsSent")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding all notifications: {ex.Message}");
            }

            return notifications;
        }
    }
}
