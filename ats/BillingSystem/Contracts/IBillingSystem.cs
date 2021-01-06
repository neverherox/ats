using ats.ats.Contracts;
using ats.BillingSys;
using System.Dynamic;

namespace ats.BillingSys.Contracts
{
    public interface IBillingSystem
    {
        void RegisterStationEventHandlers(IStation station);
        void RegisterClient(IClient client);
        IReport CreateReport(IClient client);
        void SortCallsByDate(IReport report);
        void SortCallsByCost(IReport report);
        void SortCallsByIncomingClient(IReport report);
        void SortCallsByOutgoingClient(IReport report);
    }
}
