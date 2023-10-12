﻿using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace bikeDatabaseLibrary;

// Make an object for user list
// make the database switch and entered database in an array

public class Program {
    public static void Main(string[] args)
    {
		try {
        // Resolve "" handler
        Console.WriteLine("You need to provide certain info to connect to the database");

		// Get user
        Console.WriteLine("Enter user name: ");
        var userName = Console.ReadLine();
        Console.WriteLine("Enter password");
        var password = Console.ReadLine();
		var user = new User(userName, password);

		// Get serverName and database
		Console.WriteLine("Enter server name: ");
        var serverName = Console.ReadLine();

		//  Loop until the user quits
		var done = false;
		var changeDatabase = true;
		var database = "database";
		while (!done) {

			// Ask database if changeDatabase = true
			// In the first execution changeDatabase is always true so in any way database won't be default string
			if (changeDatabase) {
        		Console.WriteLine("Enter the database to connect: ");
        		database = Console.ReadLine();
				changeDatabase = false;
			}
			
			var databaseConnection = new Connection(serverName, database, userName, password);
        
        	// Create a SQL server connection object
        	using (MySqlConnection connection = new MySqlConnection(databaseConnection.connectionString))
        	{
                connection.Open();
                Console.Clear();
                user.displayInfo();
				// Instanciate queryConstructor
				Console.WriteLine("Enter table name:");
					var table = Console.ReadLine();

				Console.WriteLine("Enter column name(type '*' if want to select all columns in the table):");
				var column = Console.ReadLine();

				Console.WriteLine("Enter keyword you want to search(Hit enter if want to see all data):");
				var searchTerm = Console.ReadLine();
				
		        Console.WriteLine("Enter the column the search term will be applied to");
		        var condition = Console.ReadLine();
				condition = (searchTerm != "") ? condition : "";

				var queryConstructor = new QueryConstructor(table, column, condition, searchTerm);
				using (MySqlCommand command = new MySqlCommand(queryConstructor.query, connection))
				{
					using (MySqlDataReader reader = command.ExecuteReader())
					{
						Console.Clear();
						Console.WriteLine(queryConstructor.query);
						var count = 0;
						while (reader.Read())
						{
							Console.WriteLine("-------------------------");
							count += 1;
							for (int i = 0; i < reader.FieldCount; i++)
							{
								if (!reader.IsDBNull(i))
								{
									string columnValue = reader.GetString(i);
									Console.WriteLine(columnValue);
								}
								else
								{
									Console.WriteLine("Null");
								}
							}
						}

						Console.WriteLine($"Total: {count}");
					}
				}
				Console.WriteLine("Hit enter to keep searching or type something to quit");
				var input = Console.ReadLine();
				done = (input == "") ? false : true;
				if (!done) {
					Console.WriteLine("Hit enter to begin another search " +
									  "or type 'CHANGE' to enter another database");
					input = Console.ReadLine();
					changeDatabase = (input == "") ? false : true;
				}
				else
				{
					connection.Close();
				}
        	}
		}
		}
        catch (MySqlException ex)
        {
        	// Handle MySQL-specific exceptions
        	Console.WriteLine("MySQL Exception: " + ex.Message);
         }
         catch (InvalidOperationException ex)
         {
         	// Handle InvalidOperationException
         	Console.WriteLine("Invalid Operation Exception: " + ex.Message);
         }
         catch (Exception ex)
         {
         	// Handle other exceptions
         	Console.WriteLine("General Exception: " + ex.Message);
         }
    }
}
