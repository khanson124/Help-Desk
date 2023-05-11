using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help_Desk
{
    public class UserManager
    {
        private List<User> users;

        public UserManager()
        {
            users = new List<User>();
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public void UpdateUser(User user)
        {
            // Find the user by email address and update the properties
            var existingUser = users.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Password = user.Password;
            }
        }

        public void DeleteUser(string email)
        {
            // Find the user by email address and remove it from the list
            var user = users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                users.Remove(user);
            }
        }

        public List<User> GetUsers()
        {
            return users;
        }
    }

}
