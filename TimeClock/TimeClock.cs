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


        public static void ManageWork(int userId )
        {
            if (DatabaseManager.HasClockedInToday(userId))
            {
                if (!DatabaseManager.HasClockedOutToday(userId))
                {
                    EndWork(userId);
                    MessageBox.Show($"You have successfully logged your exit.{endWorkTime}");
                }
                else
                {
                    MessageBox.Show("You have already clocked out today.");
                }
            }
            else
            {
                StartWork(userId);
                MessageBox.Show($"You have successfully logged your entry.{startWorkTime}");
            }
        }
        public static void StartWork(int userId)
        {
            startWorkTime = DateTime.Now;
            if (!DatabaseManager.HasClockedInToday(userId))
            {
                DatabaseManager.LogClockEntry(userId, startWorkTime.Value.Date, startWorkTime.Value);
                Console.WriteLine($"Work started at: {startWorkTime}");
            }
            else
            {
                Console.WriteLine("You have already clocked in today.");
            }
        }

        public static void EndWork(int userId)
        {
            if (!startWorkTime.HasValue)
            {
                Console.WriteLine("Work has not started yet.");
                return;
            }

            endWorkTime = DateTime.Now;

            if (endWorkTime.HasValue)
            {
                DatabaseManager.UpdateClockOutTime(userId, endWorkTime.Value);
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

