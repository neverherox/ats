using System;
using System.Collections.Generic;

namespace ats.ATS.Contracts
{
    public interface ICallService
    {
        event EventHandler<CallInfo> CallHappened;
        void RegisterUnprocessedCall(CallEventArg arg);
        void RegisterProcessedCall(CallEventArg arg);
        void RegisterDroppedCall(CallEventArg arg);
    }
}
