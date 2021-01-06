using ats.BillingSys.Contracts;
using ats.BillingSys.Controllers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace ats.BillingSys.Controllers
{
    public class ClientController : IClientController
    {
        private ICollection<IClient> clients;
        public ClientController()
        {
            clients = new List<IClient>();
        }

        public void Add(IClient client)
        {
            if (client != null)
            {
                clients.Add(client);
            }
        }
        public IClient GetClientByPhoneNumber(string phoneNumber)
        {
            return clients.Where(x => x.Phone.PhoneNumber.Equals(phoneNumber)).FirstOrDefault();
        }
    }
}
