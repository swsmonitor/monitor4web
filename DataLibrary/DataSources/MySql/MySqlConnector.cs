using MySqlConnector;

namespace DataLibrary
{
    public static class MySqlConnector
    {
        public static MySqlConnection? MySqlConnection = null;

        public async static Task<MySqlConnection?> OpenDatabaseConnection(string connectionString)
        {
            if (MySqlConnector.MySqlConnection is null)
            {
                try
                {
                    // Initialize the MySqlConnector with the provided connection string
                    MySqlConnector.MySqlConnection = new MySqlConnection(connectionString);
                    await MySqlConnector.MySqlConnection.OpenAsync();
                    bool finished = MySqlConnector.MySqlConnection.State == System.Data.ConnectionState.Open;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error initializing MySqlConnection: {ex.Message}");
                    return null;
                }
            }
            return MySqlConnector.MySqlConnection ?? null;
        }

        public static void CloseDatabaseConnection()
        {
            if (TestDatabaseConnection())
            {
                MySqlConnector.MySqlConnection.Close();
            }

            MySqlConnector.MySqlConnection = null;
        }

        public static bool TestDatabaseConnection()
        {
            return MySqlConnector.MySqlConnection is not null &&
                   MySqlConnector.MySqlConnection.State == System.Data.ConnectionState.Open;
        }
    }
}
