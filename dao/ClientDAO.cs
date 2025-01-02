using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using NomDuProjet.Models;
using NomDuProjet.DB;

namespace DAO
{
    public class ClientDAO
    {
        public bool Create(Client client)
        {
            const string query = "INSERT INTO Clients (FirstName, LastName, Email, PhoneNumber) VALUES (@FirstName, @LastName, @Email, @PhoneNumber)";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", client.FirstName);
                    command.Parameters.AddWithValue("@LastName", client.LastName);
                    command.Parameters.AddWithValue("@Email", client.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating client: {ex.Message}");
                return false;
            }
        }

        public bool Delete(Client client)
        {
            const string query = "DELETE FROM Clients WHERE Id = @Id";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", client.Id);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting client: {ex.Message}");
                return false;
            }
        }
    public bool DeleteById(int clientId)
{
    const string query = "DELETE FROM Clients WHERE Id = @Id";
    try
    {
        using (var connection = DatabaseHelper.GetConnection())
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Id", clientId);

            return command.ExecuteNonQuery() > 0;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error deleting client by ID: {ex.Message}");
        return false;
    }
}



        public bool Update(Client client)
        {
            const string query = "UPDATE Clients SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber WHERE Id = @Id";
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", client.Id);
                    command.Parameters.AddWithValue("@FirstName", client.FirstName);
                    command.Parameters.AddWithValue("@LastName", client.LastName);
                    command.Parameters.AddWithValue("@Email", client.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating client: {ex.Message}");
                return false;
            }
        }

        public Client FindById(int id)
        {
            const string query = "SELECT * FROM Clients WHERE Id = @Id";
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
                            return new Client
                            {
                                Id = reader.GetInt32("Id"),
                                FirstName = reader.GetString("FirstName"),
                                LastName = reader.GetString("LastName"),
                                Email = reader.GetString("Email"),
                                PhoneNumber = reader.GetString("PhoneNumber")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding client by ID: {ex.Message}");
            }

            return null;
        }

        public List<Client> FindAll()
        {
            const string query = "SELECT * FROM Clients";
            List<Client> clients = new List<Client>();

            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clients.Add(new Client
                            {
                                Id = reader.GetInt32("Id"),
                                FirstName = reader.GetString("FirstName"),
                                LastName = reader.GetString("LastName"),
                                Email = reader.GetString("Email"),
                                PhoneNumber = reader.GetString("PhoneNumber")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding all clients: {ex.Message}");
            }

            return clients;
        }

        



    }
}
