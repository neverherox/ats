using ats.BillingSys.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ats.BillingSys
{
    public class Report : IReport
    {
        private StringBuilder sb;
        public IAbonent Abonent { get; set; }

        private ICollection<ExtendedCallInfo> calls;
        private ICollection<ExtendedCallInfo> incomingCalls;
        private ICollection<ExtendedCallInfo> outgoingCalls;
        public ICollection<ExtendedCallInfo> Calls { get => calls.ToList(); set => calls = value; }
        public ICollection<ExtendedCallInfo> IncomingCalls { get => incomingCalls.ToList(); set => incomingCalls = value; }
        public ICollection<ExtendedCallInfo> OutgoingCalls { get => outgoingCalls.ToList(); set => outgoingCalls = value; }

        public override string ToString()
        {
            if (sb == null)
            {
                sb = new StringBuilder();
            }
            else
            {
                sb.Clear();
            }
            sb.Append("Report for: " + Abonent.Name + "\tBalance: " + Abonent.Balance);
            if (incomingCalls.Count > 0)
            {
                sb.Append("\nIncoming calls:\n");
                foreach (var call in incomingCalls)
                {
                    sb.Append(call.ToString() + '\n');
                }
            }
            if (outgoingCalls.Count > 0)
            {
                sb.Append("\nOutgoing calls:\n");
                foreach (var call in outgoingCalls)
                {
                    sb.Append(call.ToString() + '\n');
                }
            }
            return sb.ToString();
        }
    }
}
