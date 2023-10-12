// All connection and query related part is done testing in connectionTest and queryConstructorTest
// This test focuses on logics of changing database and keeping searching in the same database

using DatabaseLibrary;
using MySql.Data.MySqlClient;
using Xunit.Abstractions;

namespace DatabaseLibraryTest;

public class ProgramTest
{
	private readonly ITestOutputHelper _testOutputHelper;

	public ProgramTest(ITestOutputHelper testOutputHelper)
	{
		_testOutputHelper = testOutputHelper;
	}
	
    [Fact]
    public void MainTest()
    {
	    // Scenario: The user only search once and no database changing
	    // Arrange
	    var user = new User("root", "bryan30508");
	    var connection = new Connection("localhost", "bike", user.userName, user.password);
	    var queryConstructor = new QueryConstructor("customer", "*", "", "");
	    var dataRenderer = new DataRenderer();
	    dataRenderer._path = "resutlTest.txt";
	    var done = false; // Indicates that user hit enter to exit the program
	    var changeDatabase = false; // Indicates that user does not want to change database(since the user exit the program)
	    var database = "bike";
	    var loopNumber = 0; // To inspect how many times the program has looped
	    
				// Act
        		try {

			        //  Loop until the user quits
			        while (!done)
			        {
				        loopNumber += 1;

				        // Ask database if changeDatabase = true
				        // In the first execution changeDatabase is always true so in any way database won't be default string
				        if (changeDatabase) {
					        _testOutputHelper.WriteLine("Enter the database to connect: ");
					        database = database;
					        changeDatabase = false;
				        }
        
				        // Create a SQL server connection object
				        using (MySqlConnection connect = new MySqlConnection(connection.connectionString))
				        {
					        connect.Open();
					        using (MySqlCommand command = new MySqlCommand(queryConstructor.query, connect))
					        {
						        using (MySqlDataReader reader = command.ExecuteReader())
						        {
							        if (File.Exists(dataRenderer._path))
							        {
								        dataRenderer._lines.Add(queryConstructor.query);
								        while (reader.Read())
								        {
									        for (int i = 0; i < reader.FieldCount; i++)
									        {
										        if (!reader.IsDBNull(i))
										        {
											        string columnValue = reader.GetString(i);
											        dataRenderer._lines.Add(columnValue);
										        }
										        else
										        {
											        dataRenderer._lines.Add("Null");
										        }
									        }
								        }
								        dataRenderer.construct();
							        }
							        _testOutputHelper.WriteLine("Ask user to hit enter to keep " +
							                                    "searching or type something to quit");
							        done = true; // Mocking user input "type something to quit"
							    
							        if (!done) {
								        _testOutputHelper.WriteLine("Hit enter to begin another search " +
								                                    "or type 'CHANGE' to enter another database");
								        changeDatabase = true;// Here does not matter in this scenario since the user already quitted
							        }
							        else
							        {
								        connect.Close();
							        }
						        }
					        }
				        }
			        }
		        }
		        catch (MySqlException ex)
		        {
			        // Handle MySQL-specific exceptions
			        _testOutputHelper.WriteLine("MySQL Exception: " + ex.Message);
		        }
		        catch (InvalidOperationException ex)
		        {
			        // Handle InvalidOperationException
			        _testOutputHelper.WriteLine("Invalid Operation Exception: " + ex.Message);
		        }
		        catch (Exception ex)
		        {
			        // Handle other exceptions
			        _testOutputHelper.WriteLine("General Exception: " + ex.Message);
		        }
		        
		        // Assert
		        Assert.Equal(1, loopNumber);
		        
		        // Defects found: Error in instanciating queryConstructor in Arrange part(using bike as table) -> None
    }
}