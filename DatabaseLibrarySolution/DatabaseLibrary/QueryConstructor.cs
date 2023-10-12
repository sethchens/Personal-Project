using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DatabaseLibrary;

public class QueryConstructor
{
    private string _table { get; set; }
    private string _column;
    private string _searchTerm { get; set; }
    private string? _condition;

    public string column
    {
        get
        {
            return _column;
        }
        set
        {
            _column = value;
        }
    }

    public string? condition
    {
        get { return _condition;}
        set
        {
            if (value == "" || value == null)
            {
                _condition = null;
            }
        }
    }

    private string _query;

    public string query
    {
        get { return _query; }
    }
    
    public QueryConstructor(string table, string column, string? condition,string searchTerm)
    {
        // To have the user input passed in and construct a query
        _table = table;
        _column = column;
        _condition = condition;
        _searchTerm = searchTerm;

        if (_searchTerm == "")
        { 
            _query = $"SELECT {_column} FROM {_table}";
        }
        else
        {
            if (_condition != "")
            {
                _query = $"SELECT {_column} FROM {_table} WHERE {_condition} LIKE '%{_searchTerm}%'";
            }
        }
    }
}