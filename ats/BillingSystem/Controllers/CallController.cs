using ats.BillingSys.Contracts;
using ats.BillingSys.Controllers.Contracts;
using System.Collections.Generic;
using System.Linq;


namespace ats.BillingSys.Controllers
{
    public class CallController : ICallController
    {
        private ICollection<ExtendedCallInfo> calls;
        public CallController()
        {
            calls = new List<ExtendedCallInfo>();
        }
        public void Add(ExtendedCallInfo call)
        {
            if (call != null)
            {
                calls.Add(call);
            }
        }
        public ICollection<ExtendedCallInfo> GetAbonentCalls(IAbonent abonent)
        {
            return calls.Where(x => x.To.Equals(abonent) || x.From.Equals(abonent)).ToList();
        }
        public ICollection<ExtendedCallInfo> GetIncomingCalls(IAbonent abonent)
        {
            return calls.Where(x => x.To.Equals(abonent)).ToList();
        }
        public ICollection<ExtendedCallInfo> GetOutgoingCalls(IAbonent abonent)
        {
            return calls.Where(x => x.From.Equals(abonent)).ToList();
        }
    }
}
