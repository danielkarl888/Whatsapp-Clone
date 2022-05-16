using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI;

namespace Services
{
    public class ContactService : IContactService
    {
        public static Dictionary<string, List<Contact>> contacts = new Dictionary<string, List<Contact>>
        {
            {"david", new List<Contact>{
                new Contact{ Id="bob", Name="Bobby", Last="biibii", Server="localhost:3000", LastDate=DateTime.Now,

                Messages=new List<Message>{ new Message {Id=121, Content="hii", Created=DateTime.UtcNow, Sent=true },
                                            new Message {Id=123, Content="biibii", Created=DateTime.Now, Sent=false },                                                                            } },
                new Contact{ Id="alice", Name="Alicia", Last="david", Server="localhost:2500", LastDate=DateTime.Now,
                             Messages=new List<Message>{ new Message {Id=141, Content="razzz", Created=DateTime.UtcNow, Sent=true },
                                                         new Message {Id=143, Content="david", Created=DateTime.Now, Sent=false } }                                                                                             }
        }},
        {"raz", new List<Contact>{
                new Contact{    Id="bob_raz", Name="Bobby", Last="biibii", Server="localhost:3001", LastDate=DateTime.Now,
                                Messages=new List<Message>{ new Message {Id=121, Content="hii", Created=DateTime.UtcNow, Sent=true },
                                                            new Message {Id=123, Content="biibii", Created=DateTime.Now, Sent=false },                                                                            } },
                new Contact{    Id="alice_raz", Name="Alicia_raz", Last="david_raz", Server="localhost:2502", LastDate=DateTime.Now,
                                Messages=new List<Message>{ new Message {Id=141, Content="razzz_raz", Created=DateTime.UtcNow, Sent=true },
                                                         new Message {Id=143, Content="david_raz", Created=DateTime.Now, Sent=false } }                                                                                             }
        }}
        };
        public void Delete(string id, string username)
        {
            contacts[username].Remove(Get(id, username));
        }
        public void Edit(string id, string name, string server, string last, string username)
        {
            Contact contact = Get(id, username);
            contact.Name = name;
            contact.Server = server;
            contact.Last = last;

        }

        public Contact Get(string id, string username)
        {
            return contacts[username].Find(x => x.Id == id);
        }
        public List<Message> GetMessages(string id, string username)
        {
            return contacts[username].Find(x => x.Id == id).Messages;
        }
        public int GetNextIdMessage(string id, string username)
        {
            if(GetMessages(id, username).Count != 0)
            {
                int max = GetMessages(id, username).Max(t => t.Id);
                return max + 1;
            }
            return 1;
        }

        public List<Contact> GetAllContacts(string username)
        {
            return contacts[username];
        }
        public void Create(string id, string name, string server, string username)
        {
            Contact c = new Contact { Id = id, Name = name, Server = server, Last = null, LastDate = null, Messages = new List<Message>() };
            contacts[username].Add(c);
        }
        public bool CheckContactByID(string id, string username)
        {
            if (contacts[username].Find(x => x.Id == id) != null)
            {
                return true;
            }
            else
                return false;
        }
        public bool CheckMessageByID(string idContact, int idMessage, string username)
        {
            if (GetMessages(idContact, username).Find(x => x.Id == idMessage) != null)
            {
                return true;
            }
            return false;

        }
        public Message GetMessageById(string idContact, int idMessage, string username)
        {
            if (CheckMessageByID(idContact, idMessage, username))
            {
                return GetMessages(idContact, username).Find(x => x.Id == idMessage);
            }
            return null;
        }
    }
}
