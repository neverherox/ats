using ats.ATS;
using ats.ATS.Contracts;
using System;

namespace ats.ats.Contracts
{
    public interface IStation
    {
        ICallService CallService { get; }
        void RegisterPhone(IPhone phone);
        void UnregisterPhone(IPhone phone);
    }
}
