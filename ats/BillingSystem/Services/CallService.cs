using ats.BillingSys.Contracts;
using ats.BillingSys.Controllers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ats.BillingSys.Controllers
{
    public class CallService : ICallService
    {
        private ICollection<ExtendedCallInfo> calls;
        public event EventHandler<ExtendedCallInfo> ChargeMoney;

        public CallService()
        {
            calls = new List<ExtendedCallInfo>();
        }

        public void Add(ExtendedCallInfo call)
        {
            if (call != null)
            {
                calls.Add(call);
            }
        }
        public ICollection<ExtendedCallInfo> GetAbonentCalls(IAbonent abonent)
        {
            return calls.Where(x => x.To.Equals(abonent) || x.From.Equals(abonent)).ToList();
        }
        public ICollection<ExtendedCallInfo> GetIncomingCalls(IAbonent abonent)
        {
            return calls.Where(x => x.To.Equals(abonent)).ToList();
        }
        public ICollection<ExtendedCallInfo> GetOutgoingCalls(IAbonent abonent)
        {
            return calls.Where(x => x.From.Equals(abonent)).ToList();
        }
        public ICollection<ExtendedCallInfo> GetCallsStartedFrom(DateTime date)
        {
            return calls.Where(x => x.CallInfo.CallDate >= date).ToList();
        }
        protected virtual void OnChargeMoney(object sender, ExtendedCallInfo call)
        {
            ChargeMoney?.Invoke(sender, call);
        }
        public void ChargeForCall(ExtendedCallInfo call)
        {
            OnChargeMoney(this, call);
        }
    }
}
