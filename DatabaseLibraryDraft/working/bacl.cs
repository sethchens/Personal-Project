using DatabaseLibrary;
using MySql.Data.MySqlClient;
namespace TestProject1;

public class UnitTest1
{
    [Theory]
    [InlineData("localhost", "bike", "root", "byran30508")]
    [InlineData("localhost", "employees", "root", "byran30508")]
    public void Test1(string a, string b, string c, string d)
    {
        
        // Act
        var connectionString = new Connection(a, b, c, d).connectionString;
        var isConnected = false;

        using (MySqlConnection connect = new MySqlConnection(connectionString))
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
        }
        
        // Assert
        Assert.Equal($"Server={a};Database={b};User={c};Password={d};",
            connectionString);
        Assert.True(isConnected);
    }
}