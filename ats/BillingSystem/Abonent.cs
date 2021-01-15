using ats.ats.Contracts;
using ats.BillingSys.Contracts;
using ats.BillingSys.Controllers.Contracts;
using System.Collections.Generic;

namespace ats.BillingSys
{
    public class Abonent : IAbonent
    {
        public string Name { get; set; }
        public IPhone Phone { get; set; }
        public double Balance { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Abonent abonent &&
                   Name == abonent.Name &&
                   EqualityComparer<IPhone>.Default.Equals(Phone, abonent.Phone) &&
                   Balance == abonent.Balance;
        }

        public override int GetHashCode()
        {
            var hashCode = 1223826072;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<IPhone>.Default.GetHashCode(Phone);
            hashCode = hashCode * -1521134295 + Balance.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return "Abonent name: " + Name + " Number: " + Phone.PhoneNumber;
        }
    }
}
