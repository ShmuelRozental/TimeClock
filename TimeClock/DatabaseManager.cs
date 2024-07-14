using System;
using System.Configuration;
using System.Data.SqlClient;

namespace TimeClock
{
    public  class DatabaseManager
    {
        private readonly string connectionString;

        public DatabaseManager()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
  

        public bool ValidateCredentials(int userId, string password)
        {
            string query = "SELECT COUNT(1) FROM Users WHERE UserId = @UserId AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    Console.WriteLine("user auth");
                    return count == 1;
                }
            }
        }

        public bool HasClockedInToday(int userId)
        {
            string query = "SELECT COUNT(*) FROM TimeEntries " +
                "WHERE UserId = @UserId " +
                "AND CONVERT(DATE, EntryTime) = CONVERT(DATE, GETDATE()) ";
                

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }
        public bool HasClockedOutToday(int userId)
        {
            string query = "SELECT COUNT(*) FROM TimeEntries " +
                           "WHERE UserId = @UserId " +
                           "AND CONVERT(DATE, EntryTime) = CONVERT(DATE, GETDATE()) " +
                           "AND ExitTime IS NOT NULL";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public void LogClockEntry(int userId,DateTime date, DateTime entryTime)
        {
            string query = "INSERT INTO TimeEntries (UserId, EntryTime, Date) VALUES (@UserId, @EntryTime, @date)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@EntryTime", entryTime);
                    

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateClockOutTime(int userId, DateTime exitTime)
        {
            string query = "UPDATE TimeEntries SET ExitTime = @ExitTime WHERE UserId = @UserId AND ExitTime IS NULL AND CONVERT(DATE, EntryTime) = CONVERT(DATE, GETDATE())";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@ExitTime", exitTime);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
