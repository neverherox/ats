using ats.BillingSys.Contracts;
using System.Collections.Generic;
using System.Linq;


namespace ats.BillingSys
{
    public class Report : IReport
    {
        private ICollection<ExtendedCallInfo> calls;
        private ICollection<ExtendedCallInfo> incomingCalls;
        private ICollection<ExtendedCallInfo> outgoingCalls;

        public ICollection<ExtendedCallInfo> Calls { get => calls.ToList(); set => calls = value; }
        public ICollection<ExtendedCallInfo> IncomingCalls { get => incomingCalls.ToList(); set => incomingCalls = value; }
        public ICollection<ExtendedCallInfo> OutgoingCalls { get => outgoingCalls.ToList(); set => outgoingCalls = value; }

    }
}
