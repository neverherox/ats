using ats.ATS;
using ats.BillingSys.Contracts;

namespace ats.BillingSys
{
    public class ExtendedCallInfo
    {
        public double Cost { get; set; }

        public IAbonent To { get; set; }

        public IAbonent From { get; set; }

        public CallInfo CallInfo { get; set; }
    }
}
