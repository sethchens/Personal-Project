using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace bikeDatabaseLibrary;

public class User
{
    private string _serverName { get; set; }
    private string _userName { get; set; }
    private string _password { get; set; }
    private string _database { get; set; }
    private string _connectionString;

    public string connectionString
    {
        get { return _connectionString; }
    }

    public void displayInfo()
    {
        Console.WriteLine($"Server name: {_serverName}");
        Console.WriteLine($"Database: {_database}");
        Console.WriteLine($"User: {_userName}");
    }

    public void changeDatabase()
    {
        Console.WriteLine("Enter the database to connect: ");
        var database = Console.ReadLine();
        if (database != null)
        {
            _database = database;
        }
    }

    public User(string serverName, string database, string userName, string password)
    {
        _serverName = serverName;
        _database = database;
        _userName = userName;
        _password = password;
        _connectionString = $"Server={_serverName};Database={_database};User={_userName};Password={password};";
    }    
}

