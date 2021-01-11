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

        public void RegisterUnprocessedCall(CallEventArg arg)
        {
            calls.Add(new CallInfo
            {
                From = arg.SourcePhoneNumber,
                To = arg.TargetPhoneNumber,
                CallDate = DateTime.Now,
                Duration = TimeSpan.Zero,
                CallState = arg.State
            });
        }
        public void RegisterProcessedCall(CallEventArg arg)
        {
            var processedCall = calls.Where(x => x.From == arg.SourcePhoneNumber && x.To == arg.TargetPhoneNumber).FirstOrDefault();
            if (processedCall != null)
            {
                processedCall.CallDate = DateTime.Now;
                processedCall.CallState = arg.State;
            }
        }
        public void RegisterDroppedCall(CallEventArg arg)
        {
            CallInfo call = calls.Where(x => x.From == arg.SourcePhoneNumber && x.To == arg.TargetPhoneNumber).FirstOrDefault();
            if (call != null)
            {
                call.Duration = DateTime.Now - call.CallDate;
                calls.Remove(call);
                CallHappened?.Invoke(this, call);
            }
        }
    }
}
