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


        public Form1()
        {
            InitializeComponent();

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

                if (DatabaseManager.ValidateCredentials(userId, password))
                {
                    if (!PasswordManagement.PasswordHasExpired(userId))
                    {
                        // Check if the user has already clocked in today
                        if (!DatabaseManager.HasClockedInToday(userId))
                        {
                            // User has already clocked in today, manage work
                            TimeClock.ManageWork(userId);
                        }
                        else
                        {
                            // User has valid credentials but hasn't clocked in today
                            MessageBox.Show("You have not clocked in today.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("you need to change your password");
                        UpdatePassword updatePassword = new UpdatePassword();
                        updatePassword.SetUserId(userId);
                        updatePassword.Show();
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
                if (DatabaseManager.HasClockedInToday(userId))
                {
                    TimeClock.ManageWork(userId);
                }
                else
                {
                    MessageBox.Show("You need clocked in befor you try exit.");
                }
            }
            else
            {
                MessageBox.Show("Invalid user ID format. Please enter a valid integer.");
            }
        }

        private void changePassBtn_Click(object sender, EventArgs e)
        {

            // Check if userId is valid
            if (!int.TryParse(idTxt.Text, out int userId))
            {
                MessageBox.Show("Please enter a valid user ID.");
                return;
            }

            // Create an instance of UpdatePassword form
            UpdatePassword updatePassword = new UpdatePassword();

            // Set the user ID for the UpdatePassword form
            updatePassword.SetUserId(userId);

            // Show the UpdatePassword form
            updatePassword.Show();
        }

        private void TimeEntriesBtn_Click(object sender, EventArgs e)
        {
            if (int.TryParse(idTxt.Text, out int userId))
            {

                TimeEntriesForm timeEntriesForm = new TimeEntriesForm(userId); // Pass userId to the TimeEntriesForm
                timeEntriesForm.ShowDialog(); 
            }
            else
            {
                // Handle the case where parsing idTxt.Text to int fails
                MessageBox.Show("Invalid user ID. Please enter a valid integer ID.");
            }
        }
    }
}