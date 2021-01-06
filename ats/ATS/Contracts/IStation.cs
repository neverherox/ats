using ats.ATS;
using System;

namespace ats.ats.Contracts
{
    public interface IStation
    {
        event EventHandler<CallInfo> CallHappened;
        void RegisterPhone(IPhone phone);
    }
}
