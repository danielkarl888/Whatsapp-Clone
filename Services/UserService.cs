using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebAPI;

namespace Services
{
    public class UserService : IUserService
    {
        private static List<User> users = new List<User> {  new User {UserName="david", Password="david1", Conversations=null, DisplayName="davi" },
                                                            new User {UserName="raz", Password="raz1", Conversations= null, DisplayName="razi" } };

        public void AddUser(string username, string pass, string display)
        {
            users.Add(new User { UserName = username, Password = pass, DisplayName = display });
            ContactService.contacts.Add(username, new List<Contact>());
        }

        public bool CheckUser(string username, string password)
        {
            if (users.Find(x => x.UserName == username && x.Password == password) != null)
            {
                return true;
            }
            return false;
        }

        public List<User> GetAllUsers()
        {
            return users;
        }
        public User GetUser(string username)
        {
            return users.Find(x => x.UserName == username);
        }
        public bool checkValidation(string username, string password)
        {
            if (users.Find(x => x.UserName == username) != null)
            {
                return false;
            }
            else if (!Regex.IsMatch(password, "([A-Za-z]+[0-9]|[0-9]+[A-Za-z])[A-Za-z0-9]*"))
            {
                return false;
            }
            return true;
        }







    }
}
