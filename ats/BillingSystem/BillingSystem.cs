using ats.ats.Contracts;
using ats.ATS;
using ats.BillingSys.Controllers;
using ats.BillingSys.Contracts;
using ats.BillingSys.Controllers.Contracts;
using System.Linq;
using ats.ATS.States;
using System;

namespace ats.BillingSys
{
    public class BillingSystem : IBillingSystem
    {
        private IAbonentService abonentService;
        private ICallService callService;

        public BillingSystem()
        {
            abonentService = new AbonentService();
            callService = new CallService();
            RegisterCallServiceEventHandlers(callService);
        }
        public virtual void RegisterStationEventHandlers(IStation station)
        {
            station.CallService.CallHappened += OnCallHappened;
        }
        public virtual void RegisterCallServiceEventHandlers(ICallService callService)
        {
            callService.ChargeMoney += OnChargeMoney;
        }
        private void OnCallHappened(object sender, CallInfo call)
        {
            ExtendedCallInfo extendedCall = new ExtendedCallInfo
            {
                CallInfo = call,
                To = abonentService.GetAbonentByPhoneNumber(call.To),
                From = abonentService.GetAbonentByPhoneNumber(call.From),
            };
            if (call.CallState == CallState.Processed)
            {
                extendedCall.Cost = call.Duration.Seconds * Tariff.CostPerMinute / 60.0;
                callService.ChargeForCall(extendedCall);
            }
            callService.Add(extendedCall);
        }

        private void OnChargeMoney(object sender, ExtendedCallInfo call)
        {
            call.From.Balance -= call.Cost;
        }
        public void RegisterAbonent(IAbonent abonent)
        {
            abonentService.Add(abonent);
        }
        public IReport CreateReport(IAbonent abonent)
        {
            IReport report = new Report
            {
                Calls = callService.GetAbonentCalls(abonent),
                IncomingCalls = callService.GetIncomingCalls(abonent),
                OutgoingCalls = callService.GetOutgoingCalls(abonent),
                Abonent = abonent
            };
            return report;
        }
        public IReport CreateReport(IAbonent abonent, DateTime from)
        {
            IReport report = new Report
            {
                Calls = callService.GetAbonentCalls(abonent).Intersect(callService.GetCallsStartedFrom(from)).ToList(),
                IncomingCalls = callService.GetIncomingCalls(abonent).Intersect(callService.GetCallsStartedFrom(from)).ToList(),
                OutgoingCalls = callService.GetOutgoingCalls(abonent).Intersect(callService.GetCallsStartedFrom(from)).ToList(),
                Abonent = abonent
            };
            return report;
        }
        public void SortCallsByDate(IReport report)
        {
            report.Calls = report.Calls.OrderBy(x => x.CallInfo.CallDate).ToList();
        }
        public void SortCallsByCost(IReport report)
        {
            report.Calls = report.Calls.OrderBy(x => x.Cost).ToList();
        }
        public void SortIncomingCallsByAbonent(IReport report)
        {
            report.IncomingCalls = report.IncomingCalls.OrderBy(x => x.From.Name).ToList();
        }
        public void SortOutgoingCallsByAbonent(IReport report)
        {
            report.OutgoingCalls = report.OutgoingCalls.OrderBy(x => x.To.Name).ToList();
        }
    }
}
