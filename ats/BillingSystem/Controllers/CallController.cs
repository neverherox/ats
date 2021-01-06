using ats.BillingSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ats.BillingSys.Controllers
{
    public class CallController
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
        public ICollection<ExtendedCallInfo> GetClientCalls(Client client)
        {
            return calls.Where(x => x.To.Equals(client) || x.From.Equals(client)).ToList();
        }
    }
}
