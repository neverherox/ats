using ats.ATS;
namespace ats.BillingSys
{
    public class ExtendedCallInfo
    {
        public double Cost { get; set; }

        public Client To { get; set; }

        public Client From { get; set; }

        public CallInfo CallInfo { get; set; }
    }
}
