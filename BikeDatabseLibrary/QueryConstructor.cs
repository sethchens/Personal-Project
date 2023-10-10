using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace bikeDatabaseLibrary;

public class QueryConstructor
{
    private string _database { get; set; }
    public string _column { get; set; }
    public string _searchTerm { get; set; }
    private string _query;

    public string query
    {
        get { return _query; }
    }
    public QueryConstructor(string database, string column, string searchTerm)
    {
        // To have the user input passed in and construct a query
        _database = database;
        _column = column;
        _searchTerm = searchTerm;
        _query = $"SELECT * FROM {_database} WHERE {_column} LIKE '%{_searchTerm}%'";
    }
}