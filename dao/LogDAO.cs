using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using NomDuProjet.Models;
using NomDuProjet.DB;

namespace DAO
{
    public class LogDAO
    {
        // Création d'un log dans la base de données
        public bool Create(Log log)
        {
            const string query = "INSERT INTO Logs (Level, Message, Timestamp) VALUES (@Level, @Message, @Timestamp)";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Level", log.Level);
                    command.Parameters.AddWithValue("@Message", log.Message);
                    command.Parameters.AddWithValue("@Timestamp", log.Timestamp);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating log: {ex.Message}");
                return false;
            }
        }

        // Suppression d'un log de la base de données
        public bool Delete(Log log)
        {
            const string query = "DELETE FROM Logs WHERE Id = @Id";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", log.Id);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting log: {ex.Message}");
                return false;
            }
        }

        // Mise à jour d'un log dans la base de données
        public bool Update(Log log)
        {
            const string query = "UPDATE Logs SET Level = @Level, Message = @Message, Timestamp = @Timestamp WHERE Id = @Id";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", log.Id);
                    command.Parameters.AddWithValue("@Level", log.Level);
                    command.Parameters.AddWithValue("@Message", log.Message);
                    command.Parameters.AddWithValue("@Timestamp", log.Timestamp);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating log: {ex.Message}");
                return false;
            }
        }

        // Recherche d'un log par ID
        public Log FindById(int id)
        {
            const string query = "SELECT * FROM Logs WHERE Id = @Id";
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
                            return new Log
                            {
                                Id = reader.GetInt32("Id"),
                                Level = reader.GetString("Level"),
                                Message = reader.GetString("Message"),
                                Timestamp = reader.GetDateTime("Timestamp")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding log by ID: {ex.Message}");
            }

            return null;
        }

        // Récupérer tous les logs
        public List<Log> FindAll()
        {
            const string query = "SELECT * FROM Logs";
            List<Log> logs = new List<Log>();

            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(new Log
                            {
                                Id = reader.GetInt32("Id"),
                                Level = reader.GetString("Level"),
                                Message = reader.GetString("Message"),
                                Timestamp = reader.GetDateTime("Timestamp")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding all logs: {ex.Message}");
            }

            return logs;
        }
    }
}
