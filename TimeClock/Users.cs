using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeClock
{
    public class Users
    {
        private int _id;
        private string _name;
        private string _password;
        private DateTime _passwordExpiry;

        public Users()
        {

        }
        public Users(string name, string password)
        {
            name = _name;
            _password = password;
        }

        public int Id 
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name 
        {
            get { return _name; } 
            set { _name = value; }
        }
        public string Password 
        {
            get { return _password; }
            set { _password = value; }
        }
        public DateTime PasswordExpiry
        {
            get { return _passwordExpiry; }
            set { _passwordExpiry = value; }
        }
        
    }
}
