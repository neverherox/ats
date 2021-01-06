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
        public ICollection<ExtendedCallInfo> GetClientCalls(IClient client)
        {
            return calls.Where(x => x.To.Equals(client) || x.From.Equals(client)).ToList();
        }
    }
}
