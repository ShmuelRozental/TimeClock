using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TimeClock
{
    public class TimeEntry
    {
        public int EntryId { get; set; }
        public int UserId { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }

        // Default to current time when creating a new entry
        public TimeEntry()
        {
            EntryTime = DateTime.Now;  
        }

        // Method to calculate the duration of work
        public TimeSpan? CalculateWorkDuration()
        {
            if (ExitTime.HasValue)
            {
                return ExitTime.Value - EntryTime;
            }
            else
            {
                return null; // Work duration is unknown if exit time is not set
            }
        }

        // Method to update exit time and calculate work duration
        public void ClockOut()
        {
            ExitTime = DateTime.Now;
            TimeSpan? duration = CalculateWorkDuration();
            Console.WriteLine($"Logged out at: {ExitTime}. Total work duration: {duration}");
        }

        // Validation method to ensure EntryTime is always set
        public bool ValidateEntryTime()
        {
            return EntryTime != default(DateTime);
        }

    }


}