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
        private static List<Contact> contacts = new List<Contact>  
        {
            new Contact{ Id="bob", Name="Bobby", Last="hii", Server="localhost:3000", LastDate=DateTime.Now,
                
                Messages=new List<Message>{ new Message {Id=121, Content="hii", Created=DateTime.Now, Sent=true },
                                            new Message {Id=123, Content="biibii", Created=DateTime.UtcNow, Sent=false },                                                                            } },
                new Contact{ Id="alice", Name="Alicia", Last="biibii", Server="localhost:2500", LastDate=DateTime.UtcNow,
                                     Messages=new List<Message>{ new Message {Id=141, Content="razzz", Created=DateTime.Now, Sent=true },
                                                                 new Message {Id=143, Content="david", Created=DateTime.UtcNow, Sent=false } }                                                                                             }
        };
        public void Delete(string id)
        {
            contacts.Remove(Get(id));
        }

        public void Edit(string id, string name, string server, string last)
        {
            Contact contact = Get(id);
            contact.Name = name;
            contact.Server = server;
            contact.Last = last;

        }

        public Contact Get(string id)
        {
            return contacts.Find(x => x.Id == id);
        }
        public List<Message> GetMessages(string id)
        {
            return contacts.Find(x => x.Id == id).Messages;
        }
        public int GetNextIdMessage(string id)
        {
            int max = GetMessages(id).Max(t=>t.Id);
            return max + 1;
        }

        public List<Contact> GetAllContacts()
        {
            return contacts;
        }
        public void Create(string id, string name, string server)
        {
            Contact c = new Contact { Id = id, Name = name, Server = server, Last = null, LastDate = null };
            contacts.Add(c);
        }
        public bool CheckContactByID(string id)
        {
             if (contacts.Find(x=>x.Id==id) !=null)
            {
                return true;
            } else
                return false;
        }
        public bool CheckMessageByID(string idContact, int idMessage)
        {
             if (GetMessages(idContact).Find(x=> x.Id == idMessage) != null)
            {
                return true;
            }
             return false;

        }

        public Message GetMessageById(string idContact, int idMessage)
        {
            if (CheckMessageByID(idContact, idMessage))
            {
                return GetMessages(idContact).Find(x => x.Id == idMessage);
            }
            return null;
        }




    }
}
