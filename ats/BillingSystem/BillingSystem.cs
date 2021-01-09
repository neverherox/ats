using ats.ats.Contracts;
using ats.ATS;
using ats.BillingSys.Controllers;
using ats.BillingSys.Contracts;
using ats.BillingSys.Controllers.Contracts;
using System.Linq;


namespace ats.BillingSys
{
    public class BillingSystem : IBillingSystem
    {
        private IAbonentController abonentController;
        private ICallController callController;

        public BillingSystem()
        {
            abonentController = new AbonentController();
            callController = new CallController();
        }
        public virtual void RegisterStationEventHandlers(IStation station)
        {
            station.CallService.CallHappened += OnCallHappened;
        }
        private void OnCallHappened(object sender, CallInfo callInfo)
        {
            callController.Add(new ExtendedCallInfo
            {
                CallInfo = callInfo,
                To = abonentController.GetAbonentByPhoneNumber(callInfo.To),
                From = abonentController.GetAbonentByPhoneNumber(callInfo.From),
                Cost = callInfo.Duration.Seconds * Tariff.CostPerMinute / 60.0
        });
        }
        public void RegisterAbonent(IAbonent abonent)
        {
            abonentController.Add(abonent);
        }
        public IReport CreateReport(IAbonent abonent)
        {
            IReport report = new Report();
            report.Calls = callController.GetAbonentCalls(abonent);
            report.IncomingCalls = callController.GetIncomingCalls(abonent);
            report.OutgoingCalls = callController.GetOutgoingCalls(abonent);
            report.Abonent = abonent;
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
