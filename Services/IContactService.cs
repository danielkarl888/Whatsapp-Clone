using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI;

namespace Services
{
    public interface IContactService
    {
        public List<Contact> GetAllContacts(string username);

        public Contact Get(string id, string username);

        public void Delete(string id, string username);

        public void Edit(string id, string name, string server, string last, string username);

        public void Create(string id, string name, string server, string username);

        public bool CheckContactByID(string id, string username);

        public bool CheckMessageByID(string idContact, int idMessage, string username);

        public List<Message> GetMessages(string id, string username);
        public int GetNextIdMessage(string id, string username);

        public Message GetMessageById(string idContact, int idMessage, string username);


        public Message GetLastMessage(string id, string username);



    }
}
