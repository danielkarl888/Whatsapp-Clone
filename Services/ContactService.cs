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
            new Contact{ Id="bob", Name="Bobby", Last="hii", Server="localhost:3000", LastDate=DateTime.Now },
            new Contact{ Id="alice", Name="Alicia", Last="biibii", Server="localhost:2500", LastDate=DateTime.UtcNow }
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

        public List<Contact> GetAllContacts()
        {
            return contacts;
        }
        public void Create(string id, string name, string server)
        {
            Contact c = new Contact { Id = id, Name = name, Server = server, Last = null, LastDate = null };
            contacts.Add(c);
        }
        public bool CheckByID(string id)
        {
             if (contacts.Find(x=>x.Id==id) !=null)
            {
                return true;
            } else
                return false;
        }


    }
}
