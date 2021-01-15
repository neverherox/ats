using ats.BillingSys;
using ats.BillingSys.Contracts;
using System;
using System.Collections.Generic;

namespace ats.BillingSys.Controllers.Contracts
{
    public interface ICallService
    {

        event EventHandler<ExtendedCallInfo> ChargeMoney;
        void ChargeForCall(ExtendedCallInfo call);
        void Add(ExtendedCallInfo call);
        ICollection<ExtendedCallInfo> GetAbonentCalls(IAbonent abonent);
        ICollection<ExtendedCallInfo> GetIncomingCalls(IAbonent abonent);
        ICollection<ExtendedCallInfo> GetOutgoingCalls(IAbonent abonent);
        ICollection<ExtendedCallInfo> GetCallsStartedFrom(DateTime date);
    }
}
