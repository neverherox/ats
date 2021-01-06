using ats.BillingSys;
using System.Collections.Generic;
using System.Linq;

namespace ats.BillingSys.Controllers
{
    public class ClientController
    {
        private ICollection<Client> clients;
        public ClientController()
        {
            clients = new List<Client>();
        }

        public void Add(Client client)
        {
            if (client != null)
            {
                clients.Add(client);
            }
        }
        public Client GetClientByPhoneNumber(string phoneNumber)
        {
            return clients.Where(x => x.Phone.PhoneNumber.Equals(phoneNumber)).FirstOrDefault();
        }
    }
}
