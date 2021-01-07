using ats.BillingSys.Contracts;
using ats.BillingSys.Controllers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace ats.BillingSys.Controllers
{
    public class AbonentController : IAbonentController
    {
        private ICollection<IAbonent> abonents;
        public AbonentController()
        {
            abonents = new List<IAbonent>();
        }

        public void Add(IAbonent abonent)
        {
            if (abonent != null)
            {
                abonents.Add(abonent);
            }
        }
        public IAbonent GetAbonentByPhoneNumber(string phoneNumber)
        {
            return abonents.Where(x => x.Phone.PhoneNumber.Equals(phoneNumber)).FirstOrDefault();
        }
    }
}
