using ats.ATS;
using ats.BillingSys.Contracts;
using System.Text;

namespace ats.BillingSys
{
    public class ExtendedCallInfo
    {
        public double Cost { get; set; }

        public IAbonent To { get; set; }

        public IAbonent From { get; set; }

        public CallInfo CallInfo { get; set; }

        public override string ToString()
        {
            return "From: " + From.ToString() + "\tTo: " + To.ToString()  + "\tCost: " + Cost + "\n" + CallInfo.ToString();
        }
    }
}
