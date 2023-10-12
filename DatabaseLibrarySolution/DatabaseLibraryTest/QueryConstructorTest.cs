using System.Security.Authentication.ExtendedProtection;
using DatabaseLibrary;
using MySql.Data.MySqlClient;

namespace DatabaseLibraryTest;

public class QueryConstructorTest
{
    [Theory]
    [InlineData("customer", "*", "", "", 1445)] // should return 1445 rows of data
    [InlineData("customer", "first_name", "first_name", "KuanYu", 0)] // Should return rsult with only two rows of data
    public void TruthyQueryTest(string table, string column, string? condition, string searchTerm, int expected)
    {
        // Scenario: When I do various kind of query, does it give me the correct number of rows of data?
        // Act
        var queryConstructor = new QueryConstructor(table, column, condition, searchTerm);
        var testQuery = $"SELECT {column} FROM {table} WHERE {condition} LIKE '%{searchTerm}%'";
        if (searchTerm == "")
        { 
	        testQuery = $"SELECT {column} FROM {table}";
        }
        else
        {
	        if (condition != "")
	        {
		        testQuery = $"SELECT {column} FROM {table} WHERE {condition} LIKE '%{searchTerm}%'";
	        }
        }

        // Assert
        Assert.Equal(testQuery, queryConstructor.query);
        var connectionString = $"Server=localhost;Database=bike;User=root;Password=bryan30508;";
				using (MySqlConnection connection = new MySqlConnection(connectionString))
				{
					connection.Open();
					using (MySqlCommand command = new MySqlCommand(queryConstructor.query, connection))
					{
						using (MySqlDataReader reader = command.ExecuteReader())
						{
								var count = 0;
								while (reader.Read())
								{
									count += 1;
								}
								Assert.Equal(expected, count);
						}
					}
					connection.Close();
				}
				
				//Defects found: John returns 2 rows of data -> Was intended to use "KuanYu" as searchTerm -> None
    }
    
    [Theory]
    [InlineData("customer", "middle_name", "", "", true)] // should invoke exception
    [InlineData("customer", "first_name", "first_name", "KuanYu", true)] // Should return null
    public void FalsyQueryTest(string table, string column, string? condition, string searchTerm, bool expected)
    {
	    // Scenario: When I do various kind of query, does it give me the correct number of rows of data?
	    // Act
	    var queryConstructor = new QueryConstructor(table, column, condition, searchTerm);
	    var testQuery = $"SELECT {column} FROM {table} WHERE {condition} LIKE '%{searchTerm}%'";
	    if (searchTerm == "")
	    { 
		    testQuery = $"SELECT {column} FROM {table}";
	    }
	    else
	    {
		    if (condition != "")
		    {
			    testQuery = $"SELECT {column} FROM {table} WHERE {condition} LIKE '%{searchTerm}%'";
		    }
	    }

	    var isFalsy = false;

	    // Assert
	    Assert.Equal(testQuery, queryConstructor.query);
	    var connectionString = $"Server=localhost;Database=bike;User=root;Password=bryan30508;";
	    using (MySqlConnection connection = new MySqlConnection(connectionString))
	    {
		    try
		    {
			    connection.Open();
			    using (MySqlCommand command = new MySqlCommand(queryConstructor.query, connection))
			    {
				    using (MySqlDataReader reader = command.ExecuteReader())
				    {
					    if (!reader.HasRows)
					    {
						    // Here is to test when it returns null
						    isFalsy = true;
					    }

					    Assert.Equal(expected, isFalsy);
				    }
			    }
			    connection.Close();
		    }
		    catch (MySqlException ex)
		    {
			    // Here is to test if the exception is properly handled
			    isFalsy = true;
		    }
	    }

    }
}
 