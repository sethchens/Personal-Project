using System;

namespace bikeDatabaseLibrary;

public class Program {
    public static void Main(string[] args)
    {
        Console.WriteLine("You need to provide certain info to connect to the database");
        Console.WriteLine("Enter server name: ");
        var serverName = Console.ReadLine();
        Console.WriteLine("Enter user name: ");
        var user = Console.ReadLine();
        Console.WriteLine("Enter password");
        var password = Console.ReadLine();
        Console.WriteLine("Enter the database to connect: ");
        var database = Console.ReadLine();

        var user = new User(serverName, database, user, password);
        
        // Create a SQL server connection object
        using (SqlConnection connection = new SqlConnection(user.connectionString))
        {
            try
            {
                connection.Open();
                user.displayInfo();
                connection.Close();
            }
        }
    }
}
