using MySql.Data.MySqlClient;
using System;

namespace NomDuProjet.DB;

    public static class DatabaseHelper
    {
        
        
        private static readonly string ConnectionString =
            "Server=localhost;Database=hotelreservationa;User ID=root;Password=;Pooling=true;";

               public static MySqlConnection GetConnection()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                connection.Open(); 
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur de connexion : {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Teste la connexion à la base de données.
        /// </summary>
        public static void TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    Console.WriteLine("Connexion réussie à la base de données MySQL.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du test de connexion : {ex.Message}");
                throw;
            }
        }
    }

