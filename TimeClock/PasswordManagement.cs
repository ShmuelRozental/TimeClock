using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeClock
{
    internal static class PasswordManagement
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
 {
        // Assuming you have a method in DatabaseManager to update the password
        public static bool UpdatePassword(Users user, string newPassword)
        {
            int userId = user.Id;
            string currentPassword = user.Password;

            // Validate current credentials before proceeding
            if (DatabaseManager.ValidateCredentials(userId, currentPassword))
            {
                // Additional validation logic (e.g., password history check) can go here

                // Update the user's password in the Users object
                user.Password = newPassword;

                // Save the new password to the database
                bool passwordUpdated = DatabaseManager.UpdatePassword(userId, newPassword);

                if (passwordUpdated)
                {
                    // Optionally, update the password expiry here if you manage it
                    // user.PasswordExpiry = DateTime.Now.AddMonths(3); // Example: Set expiry 3 months from now

                    // Password updated successfully
                    return true;
                }
                else
                {
                    // Handle database update failure if needed
                    return false;
                }
            }
            else
            {
                // Invalid current credentials, return false or handle as needed
                return false;
            }
        }

        public static bool PasswordHasExpired(Users user)
        {
            return user.PasswordExpiry < DateTime.Now;
        }
    }
}