using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DatabaseLibrary;

public class User
{
    private string _userName;

    public string userName
    {
        get { return _userName; }
        set
        {
            if (value != "")
            {
                _userName = value;
            }
        }
    }

    private string _password;

    public string password
    {
        get { return _password; }
        set
        {
            if (value != "")
            {
                _password = value;
            }
        }
    }

    public void displayInfo()
    {
        Console.WriteLine($"User: {_userName}");
    }

    public User(string userName, string password)
    {
        _userName = userName;
        _password = password;
    }

    public User()
    {
        // For Connection to instanciate.
    }
}

