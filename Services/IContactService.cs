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

        public bool CheckByID(string id);


    }
}
