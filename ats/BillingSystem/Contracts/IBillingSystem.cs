using ats.ats.Contracts;
using ats.BillingSys.Controllers.Contracts;
using System;

namespace ats.BillingSys.Contracts
{
    public interface IBillingSystem
    {
        void RegisterStationEventHandlers(IStation station);
        void RegisterCallServiceEventHandlers(ICallService callService);
        void RegisterAbonent(IAbonent abonent);
        IReport CreateReport(IAbonent abonent);
        IReport CreateReport(IAbonent abonent, DateTime from);
        void SortCallsByDate(IReport report);
        void SortCallsByCost(IReport report);
        void SortIncomingCallsByAbonent(IReport report);
        void SortOutgoingCallsByAbonent(IReport report);
    }
}
