using System;

namespace bikeDatabaseLibrary;

public class User
{
    private string _serverName { get; set; }
    private string _user { get; set; }
    private string _password { get; set; }
    private string _database { get; set; }
    private string _connectionSting = $"Server={_serverName};Database={_database};User={_user};Password={password};"

    public string connectionString
    {
        get{return _connectionString}
    }

    public User displayInfo()
    {
        Console.WriteLine($"Server name: {_serverName}");
        Console.WriteLine($"Database: {_database}");
        Console.WriteLine($"User: {_user}");
    }

    public void changeDatabase()
    {
        Console.WriteLine("Enter the database to connect: ");
        var database = Console.ReadLine();
        _database = database;
    }

    public User(string serverName, string database, string user, string password, string database)
    {
        _serverName = serverName;
        _database = database;
        _user = user;
        _password = password;
    }    
}

