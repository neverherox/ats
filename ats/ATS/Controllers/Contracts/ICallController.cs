using System.Collections.Generic;

namespace ats.ATS.Contracts
{
    public interface ICallController
    {
        ICollection<CallInfo> Calls { get; }
        void AddCall(CallInfo processedCall);
        CallInfo GetCall(string from, string to);

    }
}
