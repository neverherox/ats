using ats.ATS;
using ats.BillingSys.Contracts;

namespace ats.BillingSys
{
    public class ExtendedCallInfo
    {
        public double Cost { get; set; }

        public IClient To { get; set; }

        public IClient From { get; set; }

        public CallInfo CallInfo { get; set; }
    }
}
