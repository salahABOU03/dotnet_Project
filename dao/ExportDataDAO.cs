using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using NomDuProjet.Models;
using NomDuProjet.DB;

namespace DAO
{
    public class ExportDataDAO
    {
        // Création d'une exportation dans la base de données
        public bool Create(ExportData exportData)
        {
            const string query = "INSERT INTO ExportData (FileName, FileType, ExportDate) VALUES (@FileName, @FileType, @ExportDate)";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FileName", exportData.FileName);
                    command.Parameters.AddWithValue("@FileType", exportData.FileType);
                    command.Parameters.AddWithValue("@ExportDate", exportData.ExportDate);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating export data: {ex.Message}");
                return false;
            }
        }

        // Suppression d'une exportation de la base de données
        public bool Delete(ExportData exportData)
        {
            const string query = "DELETE FROM ExportData WHERE Id = @Id";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", exportData.Id);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting export data: {ex.Message}");
                return false;
            }
        }

        // Mise à jour d'une exportation dans la base de données
        public bool Update(ExportData exportData)
        {
            const string query = "UPDATE ExportData SET FileName = @FileName, FileType = @FileType, ExportDate = @ExportDate WHERE Id = @Id";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", exportData.Id);
                    command.Parameters.AddWithValue("@FileName", exportData.FileName);
                    command.Parameters.AddWithValue("@FileType", exportData.FileType);
                    command.Parameters.AddWithValue("@ExportDate", exportData.ExportDate);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating export data: {ex.Message}");
                return false;
            }
        }

        // Recherche d'une exportation par ID
        public ExportData FindById(int id)
        {
            const string query = "SELECT * FROM ExportData WHERE Id = @Id";
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
                            return new ExportData
                            {
                                Id = reader.GetInt32("Id"),
                                FileName = reader.GetString("FileName"),
                                FileType = reader.GetString("FileType"),
                                ExportDate = reader.GetDateTime("ExportDate")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding export data by ID: {ex.Message}");
            }

            return null;
        }

        // Récupérer toutes les exportations
        public List<ExportData> FindAll()
        {
            const string query = "SELECT * FROM ExportData";
            List<ExportData> exportDataList = new List<ExportData>();

            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            exportDataList.Add(new ExportData
                            {
                                Id = reader.GetInt32("Id"),
                                FileName = reader.GetString("FileName"),
                                FileType = reader.GetString("FileType"),
                                ExportDate = reader.GetDateTime("ExportDate")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding all export data: {ex.Message}");
            }

            return exportDataList;
        }
    }
}
