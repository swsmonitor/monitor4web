

using AirtableApiClient;
using MySqlConnector;

namespace DataLibrary.DataSources;


public class MySqlConfig
{    public string ConnectionString { get; set; } = string.Empty;
}
public class SqlRecord: AirtableRecord
{
}

// Interface with MySQL database using MySqlConnector library
// This cannot be used with Blazor apps as Webassembly does not support direct database connections
// It is designed to be used in console, MAUI or server-side applications
public static class MySqlHelper
{
    public static async Task<List<SqlRecord>> ReadTable(object connectionInfo, string tableName, string? recordId = null)
    {
        if (connectionInfo is null || connectionInfo is not MySqlConfig)
        {
            throw new ArgumentException("Invalid connection info provided. Expected MySqlConfig.", nameof(connectionInfo));
        }
        List<SqlRecord> allRecords = new();
        try
        {
            MySqlConfig mySqlConfig = connectionInfo as MySqlConfig;
            try
            {   // Open the database connection asynchronously
                MySqlConnection? mySqlConnection = await MySqlConnector.OpenDatabaseConnection(mySqlConfig.ConnectionString);
                if (mySqlConnection is null)
                {
                    throw new InvalidOperationException("Failed to open MySQL connection.");
                }
                // SQL query to retrieve data
                string sql = $"SELECT * FROM {tableName}";
                // Create MySqlCommand to execute the query
                using var cmd = new MySqlCommand(sql, mySqlConnection);
                // Execute the command and retrieve data using MySqlDataReader
                using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                // Loop through the retrieved data and print to console
                while (await reader.ReadAsync())
                {
                    var fields = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        fields[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                    }

                    SqlRecord newRecord = new SqlRecord { Fields = fields };
                    allRecords.Add(newRecord);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening database connection: {ex.Message}");
            }

        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.ToString());
        }
        return allRecords.Count > 0 ? allRecords : new List<SqlRecord>(); // Return empty list if no records found
    }
    //public string toJSON(SqlDataReader o)
    //{
    //    StringBuilder s = new StringBuilder();
    //    s.Append("[");
    //    if (o.HasRows)
    //        while (o.Read())
    //            s.Append("{" + '"' + "Id" + '"' + ":" + o["Id"] + ", "
    //            + '"' + "CN" + '"' + ":" + o["CatName"] + ", "
    //            + '"' + "Ord" + '"' + ":" + o["Ord"] + ","
    //            + '"' + "Icon" + '"' + ":" + o["Icon"] + "}, ");
    //    s.Remove(s.Length - 2, 2);
    //    s.Append("]");
    //    o.Close();
    //    return s.ToString();
    //}
}