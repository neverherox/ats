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
        private IClientController clientController;
        private ICallController callController;

        public BillingSystem()
        {
            clientController = new ClientController();
            callController = new CallController();
            Tariff.CostPerMinute = 0.3;
        }
        public virtual void RegisterStationEventHandlers(IStation station)
        {
            station.CallHappened += OnCallHappened;
        }
        
        public void RegisterClient(IClient client)
        {
            clientController.Add(client);
        }
        private void OnCallHappened(object sender, CallInfo callInfo)
        {
            callController.Add(new ExtendedCallInfo
            {
                CallInfo = callInfo,
                To = clientController.GetClientByPhoneNumber(callInfo.To),
                From = clientController.GetClientByPhoneNumber(callInfo.From),
            });
        }
        public IReport CreateReport(IClient client)
        {
            var clientCalls = callController.GetClientCalls(client);
            foreach(var call in clientCalls)
            {
                call.Cost = call.CallInfo.Duration.Seconds * Tariff.CostPerMinute / 60.0;
            }
            IReport report = new Report(clientCalls.ToList());
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
        public void SortCallsByIncomingClient(IReport report)
        {
            report.Calls = report.Calls.OrderBy(x => x.From.Name).ToList();
        }
        public void SortCallsByOutgoingClient(IReport report)
        {
            report.Calls = report.Calls.OrderBy(x => x.To.Name).ToList();
        }
    }
}
