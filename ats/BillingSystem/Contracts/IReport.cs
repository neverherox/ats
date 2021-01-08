using ats.BillingSys;
using System.Collections.Generic;

namespace ats.BillingSys.Contracts
{
    public interface IReport
    {
        IAbonent Abonent { get; set; }
        ICollection<ExtendedCallInfo> Calls { get; set; }
        ICollection<ExtendedCallInfo> IncomingCalls { get; set; }
        ICollection<ExtendedCallInfo> OutgoingCalls { get; set; }
    }
}
