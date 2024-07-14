using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeClock
{
    internal static class TimeClock
    {
        public static readonly DateTime UnixEpoch = new DateTime(
            1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static DateTime? startWorkTime;
        private static DateTime? endWorkTime;


        public static void ManageWork(int userId, DatabaseManager dbManager)
        {
            if (dbManager.HasClockedInToday(userId))
            {
                if (!dbManager.HasClockedOutToday(userId))
                {
                    EndWork(userId, dbManager);
                    MessageBox.Show($"You have successfully logged your exit.{endWorkTime}");
                }
                else
                {
                    MessageBox.Show("You have already clocked out today.");
                }
            }
            else
            {
                StartWork(userId, dbManager);
                MessageBox.Show($"You have successfully logged your entry.{startWorkTime}");
            }
        }
        public static void StartWork(int userId, DatabaseManager dbManager)
        {
            startWorkTime = DateTime.Now;
            if (!dbManager.HasClockedInToday(userId))
            {
                dbManager.LogClockEntry(userId, startWorkTime.Value.Date, startWorkTime.Value);
                Console.WriteLine($"Work started at: {startWorkTime}");
            }
            else
            {
                Console.WriteLine("You have already clocked in today.");
            }
        }

        public static void EndWork(int userId, DatabaseManager dbManager)
        {
            if (!startWorkTime.HasValue)
            {
                Console.WriteLine("Work has not started yet.");
                return;
            }

            endWorkTime = DateTime.Now;

            if (endWorkTime.HasValue)
            {
                dbManager.UpdateClockOutTime(userId, endWorkTime.Value);
                var duration = endWorkTime.Value - startWorkTime.Value;
                Console.WriteLine($"Work stopped at: {endWorkTime}");
                Console.WriteLine($"Total work duration: {duration}");
                startWorkTime = null;
            }
            else
            {
                Console.WriteLine("Failed to end work due to invalid time.");
            }
        }
    }
}

