using ats.ats.Contracts;
using ats.ATS.Controllers.Contracts;
using System.Collections.Generic;
using System.Linq;


namespace ats.ATS.Controllers
{
    public class PhoneController : IPhoneController
    {
        private ICollection<IPhone> phones;
        public PhoneController()
        {
            phones = new List<IPhone>();
        }
        public IPhone CreatePhone(string phoneNumber)
        {
            IPhone phone = new Phone { PhoneNumber = phoneNumber};
            phones.Add(phone);
            return phone;
        }
        public IPhone GetPhoneByPhoneNumber(string phoneNumber)
        {
            return phones.Where(x => x.PhoneNumber.Equals(phoneNumber)).FirstOrDefault();
        }
    }
}
