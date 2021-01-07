using ats.ats.Contracts;
using ats.BillingSys;
using System.Dynamic;

namespace ats.BillingSys.Contracts
{
    public interface IBillingSystem
    {
        void RegisterStationEventHandlers(IStation station);
        void RegisterAbonent(IAbonent abonent);
        IReport CreateReport(IAbonent abonent);
        void SortCallsByDate(IReport report);
        void SortCallsByCost(IReport report);
        void SortCallsByIncomingAbonent(IReport report);
        void SortCallsByOutgoingAbonent(IReport report);
    }
}
