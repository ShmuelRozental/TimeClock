using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TimeClock
{
    public partial class TimeEntriesForm : Form
    {
        private int userId;

        public TimeEntriesForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void TimeEntriesForm_Load(object sender, EventArgs e)
        {
            List<TimeEntry> entries = DatabaseManager.GetTimeEntriesForUser(userId);

            // Check if entries has data
            if (entries != null && entries.Count > 0)
            {
                MessageBox.Show($"Retrieved {entries.Count} time entries for user {userId}");

                // Clear previous entries
                listBoxEntries.Items.Clear();

                // Populate ListBox with entries
                foreach (var entry in entries)
                {
                    string entryInfo = $"Entry ID: {entry.EntryId}, User ID: {entry.UserId}, Entry Time: {entry.EntryTime}, Exit Time: {entry.ExitTime ?? DateTime.MinValue}";
                    listBoxEntries.Items.Add(entryInfo);
                }
            }
            else
            {
                MessageBox.Show("No time entries found for the user.");
            }
        }

        private void listBoxEntries_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
