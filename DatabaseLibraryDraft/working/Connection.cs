using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace bikeDatabaseLibrary;

public class Connection
{
    public string _serverName { get; set; }

    public string _database { get; set; }

    private string _connectionString;

    public string connectionString
    {
        get { return _connectionString; }
    }

    public Connection(string serverName, string database, string userName, string password)
    {
        _serverName = serverName;
        _database = database;
        _connectionString = $"Server={_serverName};Database={_database};User={userName};Password={password};";
    }
}