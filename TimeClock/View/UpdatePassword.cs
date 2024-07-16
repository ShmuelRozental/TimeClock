using System;
using System.Windows.Forms;

namespace TimeClock
{
    public partial class UpdatePassword : Form
    {
        private int userId;

        public UpdatePassword()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.UpdatePassword_Load);
        }



        private void UpdatePassword_Load(object sender, EventArgs e)
        {
           
        }
        public void SetUserId(int userId)
        {
            this.userId = userId;
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            User user = DatabaseManager.GetUserById(userId);
            string oldPassword = user.Password;
            if (oldPassTxt.Text == oldPassword)
            {
                string newPassword = newPassTxt.Text;
                if (newPassword == confirmPassTxt.Text)
                {
                    try
                    {
                        bool passwordUpdated = PasswordManagement.UpdatePassword(user, newPassword);

                        if (passwordUpdated)
                        {
                            MessageBox.Show("Password updated successfully.");
                            this.Close(); // סגירת הטופס לאחר עדכון הסיסמה בהצלחה
                        }
                        else
                        {
                            MessageBox.Show("Failed to update password. Please check your old password.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Passwords do not match.");
                }
            }
            else
            {
                MessageBox.Show("The old password does not match.");
            }
        }
    }
}
