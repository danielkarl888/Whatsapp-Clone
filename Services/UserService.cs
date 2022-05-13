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
        private static List<User> users = new List<User> { new User {UserName="david", Password="david1",
            Conversations=
            new List<Contact>
        {
            new Contact{ Id="bob", Name="Bobby", Last="hii", Server="localhost:3000", LastDate=DateTime.Now,

                Messages=new List<Message>{ new Message {Id=121, Content="hii", Created=DateTime.Now, Sent=true },
                                            new Message {Id=123, Content="biibii", Created=DateTime.UtcNow, Sent=false },                                                                            } },
                new Contact{ Id="alice", Name="Alicia", Last="biibii", Server="localhost:2500", LastDate=DateTime.UtcNow,
                                     Messages=new List<Message>{ new Message {Id=141, Content="razzz", Created=DateTime.Now, Sent=true },
                                                                 new Message {Id=143, Content="david", Created=DateTime.UtcNow, Sent=false } }                                                                                             }
        },
            DisplayName="dudo" },
            new User {UserName="raz", Password="raz1",
            Conversations= new List<Contact>
        {
            new Contact{ Id="bobwithraz", Name="Bobby", Last="withraz", Server="localhost:3000", LastDate=DateTime.Now,

                Messages=new List<Message>{ new Message {Id=145, Content="hii", Created=DateTime.Now, Sent=true },
                                            new Message {Id=147, Content="biibii", Created=DateTime.UtcNow, Sent=false },                                                                            } },
            new Contact{ Id="alicewithraz", Name="Alicia", Last="biibii", Server="localhost:2500", LastDate=DateTime.UtcNow,
                                     Messages=new List<Message>{ new Message {Id=141, Content="razzz", Created=DateTime.Now, Sent=true },
                                                                 new Message {Id=143, Content="david", Created=DateTime.UtcNow, Sent=false } }                                                                                             }
        }, DisplayName="razi" } };

        public static User? activeUser;
        public void AddUser(string username, string pass, string display)
        {   
            users.Add(new User {UserName=username, Password = pass, DisplayName = display});
        }

        public bool CheckUser(string username, string password)
        {
            if (users.Find(x => x.UserName == username && x.Password==password) != null)
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
            } else if (!Regex.IsMatch(password, "([A-Za-z]+[0-9]|[0-9]+[A-Za-z])[A-Za-z0-9]*")){
                return false;
            }
            return true;
        }
        public void activeUserChange(string username)
        {
            activeUser = GetUser(username);
        }

        public User GetActiveUser()
        {
            return activeUser;
        }






    }
}
