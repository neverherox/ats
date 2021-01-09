using ats.ATS.Contracts;
using System.Collections.Generic;
using System.Linq;
using ats.ATS.States;
using System;

namespace ats.ATS.Controllers
{
    public class CallService : ICallService
    {
        private ICollection<CallInfo> calls;
        public event EventHandler<CallInfo> CallHappened;

        public CallService()
        {
            calls = new List<CallInfo>();
        }
        public void Add(CallInfo processedCall)
        {
            calls.Add(processedCall);
        }
        public CallInfo GetCall(string from, string to)
        {
            return calls.Where(x => x.From.Equals(from) && x.To.Equals(to)).FirstOrDefault();
        }
        public void Remove(CallInfo call)
        {
            if (call != null)
            {
                calls.Remove(call);
            }
        }

        protected virtual void OnCallHappened(object sender, CallInfo call)
        {
            CallHappened?.Invoke(sender, call);
        }
        public void RegisterCall(CallInfo call)
        {
            OnCallHappened(this, call);
        }
    }
}
