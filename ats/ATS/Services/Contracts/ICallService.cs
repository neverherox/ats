using System;
using System.Collections.Generic;

namespace ats.ATS.Contracts
{
    public interface ICallService
    {
        event EventHandler<CallInfo> CallHappened;
        void RegisterUnprocessedCall(CallEventArgs arg);
        void RegisterProcessedCall(CallEventArgs arg);
        void RegisterDroppedCall(CallEventArgs arg);
    }
}
