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
        private static TimeEntry currentEntry; 


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
                TimeEntry timeEntry = new TimeEntry(); // Create a new TimeEntry object
                DatabaseManager.LogClockEntry(userId, timeEntry.EntryTime.Date, timeEntry.EntryTime);
                Console.WriteLine($"Work started at: {timeEntry.EntryTime}");
            }
            else
            {
                Console.WriteLine("You have already clocked in today.");
            }
        }
        public static void EndWork(int userId)
        {
            if (currentEntry != null)
            {
                currentEntry.ClockOut(); // Set ExitTime and calculate work duration
                DatabaseManager.UpdateClockOutTime(userId, currentEntry.ExitTime.Value);
                Console.WriteLine($"Work stopped at: {currentEntry.ExitTime}");
                Console.WriteLine($"Total work duration: {currentEntry.CalculateWorkDuration()}");

                // Reset the currentEntry for next session
                currentEntry = null;
            }
            else
            {
                Console.WriteLine("Work has not started yet.");
            }
        }
    }
}

