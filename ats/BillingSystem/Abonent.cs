﻿using ats.ats.Contracts;
using ats.BillingSys.Contracts;
using ats.BillingSys.Controllers.Contracts;

namespace ats.BillingSys
{
    public class Abonent : IAbonent
    {
        public string Name { get; set; }
        public IPhone Phone { get; set; }
        public double Balance { get; set; }
        
        public override string ToString()
        {
            return "Abonent name: " + Name + " Number: " + Phone.PhoneNumber;
        }
    }
}
