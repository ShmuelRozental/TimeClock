using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace TimeClock
{
    public static class DatabaseManager
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        //get user from data base by id
        public static User GetUserById(int userId)
        {
            string query = "SELECT UserId, UserName, Password, PasswordExpiry, CreatedAt FROM Users WHERE UserId = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User user = new User
                            {
                                Id = (int)reader["UserId"],
                                Name = reader["UserName"].ToString(),
                                Password = reader["Password"].ToString(),
                                PasswordExpiry = (DateTime)reader["PasswordExpiry"],
                              
                            };
                            return user;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        // Check if user exists and password matches
        public static bool ValidateCredentials(int userId, string password)
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
                    Console.WriteLine($"user auth");
                    return count == 1;
                }
            }
        }

        //check if user already clock in
        public static bool HasClockedInToday(int userId)
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
        //check if user already clock out
        public static bool HasClockedOutToday(int userId)
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

        // Logs a clock entry for a user into the TimeEntries table.
        public static void LogClockEntry(int userId,DateTime date, DateTime entryTime)
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

        // Updates the clock-out time for a user's entry in the TimeEntries table.
        public static void UpdateClockOutTime(int userId, DateTime exitTime)
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
        // Validate new password against the last three passwords in history
        public static bool ValidateNewPassword(int userID, string newPassword, SqlConnection connection, SqlTransaction transaction)
        {
            string query = @"
        SELECT COUNT(*) 
        FROM (SELECT TOP 3 OldPassword 
              FROM PasswordHistory 
              WHERE UserId = @UserId 
              ORDER BY ChangedAt DESC) AS LastThreePasswords
        WHERE OldPassword = @NewPassword";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@UserId", userID);
                command.Parameters.AddWithValue("@NewPassword", newPassword);

                try
                {
                    // Execute the query and handle empty result set
                    object result = command.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                    {
                        return true; // No previous passwords found, so new password is valid
                    }

                    int count = Convert.ToInt32(result);
                    return count == 0; // Valid new password if count is 0
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred in ValidateNewPassword: " + ex.Message);
                    throw; // Rethrow the exception to handle it at a higher level
                }
            }
        }


        // Update user's password in the Users table
        public static bool UpdateUserPassword(int userId, string newPassword, SqlConnection connection, SqlTransaction transaction)
        {
            string updateQuery = @"
        UPDATE Users 
        SET Password = @NewPassword, PasswordExpiry = DATEADD(DAY, 90, GETDATE())
        WHERE UserId = @UserId";

            using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@NewPassword", newPassword);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0; // Return true if update succeeded
            }
        }

        // Insert old password into PasswordHistory table
        public static bool InsertPasswordHistory(int userId, string oldPassword, SqlConnection connection, SqlTransaction transaction)
        {
            string insertQuery = @"
        INSERT INTO PasswordHistory (UserId, OldPassword, ChangedAt)
        VALUES (@UserId, @OldPassword, GETDATE())";

            using (SqlCommand command = new SqlCommand(insertQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@OldPassword", oldPassword);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0; // Return true if insert succeeded
            }
        }

        public static void ConsoleLogTimeEntriesForUser(int userId)
        {
            List<TimeEntry> timeEntries = new List<TimeEntry>();
            string query = @"
        SELECT EntryId, UserId, EntryTime, ExitTime 
        FROM TimeEntries 
        WHERE UserId = @UserId 
        ORDER BY EntryTime";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TimeEntry entry = new TimeEntry
                            {
                                EntryId = (int)reader["EntryId"],
                                UserId = (int)reader["UserId"],
                                EntryTime = (DateTime)reader["EntryTime"],
                                ExitTime = reader["ExitTime"] != DBNull.Value ? (DateTime?)reader["ExitTime"] : null
                            };
                            timeEntries.Add(entry);
                        }
                    }
                }
            }

            // Output to console
            Console.WriteLine($"Time entries for user {userId}:");
            foreach (var entry in timeEntries)
            {
                Console.WriteLine($"EntryId: {entry.EntryId}, UserId: {entry.UserId}, EntryTime: {entry.EntryTime}, ExitTime: {entry.ExitTime}");
            }
        }

        // Method to get all time entries for a user
        public static List<TimeEntry> GetTimeEntriesForUser(int userId)
        {
            List<TimeEntry> timeEntries = new List<TimeEntry>();
            string query = @"
                SELECT EntryId, UserId, EntryTime, ExitTime 
                FROM TimeEntries 
                WHERE UserId = @UserId 
                ORDER BY EntryTime";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TimeEntry entry = new TimeEntry
                            {
                                EntryId = (int)reader["EntryId"],
                                UserId = (int)reader["UserId"],
                                EntryTime = (DateTime)reader["EntryTime"],
                                ExitTime = reader["ExitTime"] != DBNull.Value ? (DateTime?)reader["ExitTime"] : null
                            };
                            timeEntries.Add(entry);
                        }
                    }
                }
            }

            return timeEntries;
        }
    }
}
