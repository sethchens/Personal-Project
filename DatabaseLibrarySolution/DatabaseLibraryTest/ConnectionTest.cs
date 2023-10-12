using DatabaseLibrary;
using MySql.Data.MySqlClient;

namespace DatabaseLibraryTest;

public class ConnectionTest
{
    [Theory]
    [InlineData("localhost", "bike", "root", "bryan30508", true)]
    [InlineData("localhost", "employees", "root", "bryan30508", true)]
    public void SuccessfulConnectionTest(string serverName, string database, string userName, string password, bool expected)
    {
        // Scenario: When different database is given, is it able to connect to them?
        // Act
        var connection = new Connection(serverName, database, userName, password);
        var isConnected = false;
        
        // Assert
        Assert.Equal($"Server={serverName};Database={database};User={userName};Password={password};", connection.connectionString);
        using (MySqlConnection connect = new MySqlConnection(connection.connectionString))
        {
            try
            {
                connect.Open();
                isConnected = true;
            }
            catch (MySqlException)
            {
                isConnected = false;
            }
            finally
            {
                connect.Close();
            }
            Assert.Equal(expected, isConnected);
        }
        
        // Defects found: None
    }

    [Theory]
    [InlineData("localhost", "bike", "root", "", false)]
    [InlineData("localhost", "employees", "seth", "bryan30508", false)]
    public void FailedConnectionTest(string serverName, string database, string userName, string password, bool expected)
    {
        // Scenario: When wrong user name or password is given, is the program able to handle?
        // Act
        var connection = new Connection(serverName, database, userName, password);
        var isConnected = true;

        // Assert
        Assert.Equal($"Server={serverName};Database={database};User={userName};Password={password};", connection.connectionString);
        using (MySqlConnection connect = new MySqlConnection(connection.connectionString))
        {
            try
            {
                connect.Open();
                isConnected = true;
            }
            catch (MySqlException ex)
            {
                isConnected = false;
            }
            finally
            {
                connect.Close();
            }

            Assert.Equal(expected, isConnected);
        }

        // Defects found: None
    }
}