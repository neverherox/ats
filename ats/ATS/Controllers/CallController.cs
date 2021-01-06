using ats.ATS.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace ats.ATS.Controllers
{
    public class CallController : ICallController
    {
        private ICollection<CallInfo> calls;
        public CallController()
        {
            calls = new List<CallInfo>();
        }
        public ICollection<CallInfo> Calls { get => calls.ToList(); }
        public void AddCall(CallInfo processedCall)
        {
            calls.Add(processedCall);
        }
        public CallInfo GetCall(string from, string to)
        {
            return calls.Where(x => x.From.Equals(from) && x.To.Equals(to)).FirstOrDefault();
        }
    }
}
