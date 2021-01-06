using System.Collections.Generic;

namespace ats.ATS.Contracts
{
    public interface ICallController
    {
        void AddCall(CallInfo processedCall);
        void Remove(CallInfo call);

        CallInfo GetCall(string from, string to);
    }
}
