using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeClock
{
    public partial class Form1 : Form
    {
        DatabaseManager dbManager;

        public Form1()
        {
            InitializeComponent();
            dbManager = new DatabaseManager();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        private void enterbtn_Click(object sender, EventArgs e)
        {
            int userId;

            if (int.TryParse(idTxt.Text, out userId))
            {
                string password = passwordTxt.Text;

                if (dbManager.ValidateCredentials(userId, password))
                {
                    // Check if the user has already clocked in today
                    if (!dbManager.HasClockedInToday(userId))
                    {
                        // User has already clocked in today, manage work
                        TimeClock.ManageWork(userId, dbManager);
                    }
                    else
                    {
                        // User has valid credentials but hasn't clocked in today
                        MessageBox.Show("You have not clocked in today.");
                        // Optionally, you might provide an option to clock in here
                    }
                }
                else
                {
                    // Invalid credentials
                    MessageBox.Show("Invalid ID or Password.");
                }
            }
            else
            {
                // Invalid user ID format
                MessageBox.Show("Invalid user ID format. Please enter a valid integer.");
            }
        }
        private void idTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void passwordTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            int userId;

            if (int.TryParse(idTxt.Text, out userId))
            {
                // Check if the user has already clocked in today
                if (dbManager.HasClockedInToday(userId))
                {
                    TimeClock.ManageWork(userId, dbManager);
                }
                else
                {
                    MessageBox.Show("You have not clocked in today.");
                }
            }
            else
            {
                MessageBox.Show("Invalid user ID format. Please enter a valid integer.");
            }
        }

    }
}
