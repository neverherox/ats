using ats.ats.Contracts;
using ats.ATS;
using ats.BillingSys.Controllers;
using System.Linq;


namespace ats.BillingSys
{
    public class BillingSystem
    {
        private ClientController clientController;
        private CallController callController;

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
        
        public void RegisterClient(Client client)
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
        public Report CreateReport(Client client)
        {
            var clientCalls = callController.GetClientCalls(client);
            foreach(var call in clientCalls)
            {
                call.Cost = call.CallInfo.Duration.Seconds * Tariff.CostPerMinute / 60.0;
            }
            Report report = new Report(clientCalls.ToList());
            return report;
        }
    }
}
