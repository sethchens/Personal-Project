using System;

namespace bikeDatabaseLibrary;

public class QueryConstructor
{
    private readonly var _database = "bike";
    public string _column { get; set; }
    public string _searchTerm { get; set; }
    private var _query = $"SELECT * FROM {_database} WHERE {_column} LIKE {_searchTerm}";

    public string query
    {
        get { return _query; }
    }
    public QueryConstructor(string column, string searchTerm)
    {
        // To have the user input passed in and construct a query
        _column = column;
        _searchTerm = $"'%{searchTerm}%'";
    }
}