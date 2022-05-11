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
        public List<Contact> GetAllContacts();

        public Contact Get(string id);

        public void Delete(string id);

        public void Edit(string id, string name, string server, string last);

        public void Create(string id, string name, string server);

        public bool CheckContactByID(string id);

        public bool CheckMessageByID(string idContact, int idMessage);

        public List<Message> GetMessages(string id);
        public int GetNextIdMessage(string id);

        public Message GetMessageById(string idContact, int idMessage);





    }
}
