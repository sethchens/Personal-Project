using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace bikeDatabaseLibrary;

// Make an object for user list
// make the database switch and entered database in an array

public class Program {
    public static void Main(string[] args)
    {
        // Resolve "" handler
        Console.WriteLine("You need to provide certain info to connect to the database");
        Console.WriteLine("Enter server name: ");
        var serverName = Console.ReadLine();
        Console.WriteLine("Enter user name: ");
        var userName = Console.ReadLine();
        Console.WriteLine("Enter password");
        var password = Console.ReadLine();
        Console.WriteLine("Enter the database to connect: ");
        var database = Console.ReadLine();

        var user = new User(serverName, database, userName, password);
        
        // Create a SQL server connection object
        using (MySqlConnection connection = new MySqlConnection(user.connectionString))
        {
            try
            {
                connection.Open();
                Console.Clear();
                user.displayInfo();
                connection.Close();
            }
            catch (MySqlException ex)
            {
                // Handle MySQL-specific exceptions
                Console.WriteLine("MySQL Exception: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // Handle InvalidOperationException
                Console.WriteLine("Invalid Operation Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine("General Exception: " + ex.Message);
            }
        }
    }
}
