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
        public void Add(IPhone phone)
        {
            if (phone != null)
            {
                phones.Add(phone);
            }
        }
        public IPhone GetPhoneByPhoneNumber(string phoneNumber)
        {
            return phones.Where(x => x.PhoneNumber.Equals(phoneNumber)).FirstOrDefault();
        }
    }
}
