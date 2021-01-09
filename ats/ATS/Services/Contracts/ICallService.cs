using System;
using System.Collections.Generic;

namespace ats.ATS.Contracts
{
    public interface ICallService
    {
        event EventHandler<CallInfo> CallHappened;
        void Add(CallInfo processedCall);
        void Remove(CallInfo call);
        void RegisterCall(CallInfo call);
        CallInfo GetCall(string from, string to);
    }
}
