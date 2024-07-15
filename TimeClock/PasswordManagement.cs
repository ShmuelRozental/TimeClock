using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace TimeClock
{
    
    internal static class PasswordManagement
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static bool UpdatePassword(Users user, string newPassword)
        {
            int userId = user.Id;
            string currentPassword = user.Password;

            // Validate current credentials before proceeding
            if (!DatabaseManager.ValidateCredentials(userId, currentPassword))
            {
                // Invalid current credentials
                return false;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Open the connection
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Validate password history within the transaction
                        if (!DatabaseManager.ValidateNewPassword(userId, newPassword, connection, transaction))
                        {
                            // Failed password history validation
                            return false;
                        }

                        // Update the user's password in the Users table within the transaction
                        bool passwordUpdated = DatabaseManager.UpdateUserPassword(userId, newPassword, connection, transaction);

                        if (passwordUpdated)
                        {
                            // Insert old password into PasswordHistory within the transaction
                            bool historyUpdated = DatabaseManager.InsertPasswordHistory(userId, currentPassword, connection, transaction);

                            if (historyUpdated)
                            {
                                // Commit the transaction
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                // Rollback if history update fails
                                transaction.Rollback();
                                return false;
                            }
                        }
                        else
                        {
                            // Rollback if password update fails
                            transaction.Rollback();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction on error
                        transaction.Rollback();
                        Console.WriteLine("Transaction rolled back: " + ex.Message);
                        throw; // Rethrow the exception to handle it at a higher level
                    }
                }
            }
        }


        public static bool PasswordHasExpired(int userID)
        {
            Users user = DatabaseManager.GetUserById(userID);
            DateTime expiryDate = user.PasswordExpiry.AddDays(90);
            return expiryDate < DateTime.Now;
        }
    }
}