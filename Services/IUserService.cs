using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        public void AddUser(string username, string pass, string display);
        public bool CheckUser(string username, string password);

        public List<User> GetAllUsers();
        public User GetUser(string username);
        public bool checkValidation(string username, string password);

        public bool IsUserExist(string username);


    }
}
