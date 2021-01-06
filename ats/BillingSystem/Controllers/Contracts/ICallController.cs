using ats.BillingSys;
using ats.BillingSys.Contracts;
using System.Collections.Generic;

namespace ats.BillingSys.Controllers.Contracts
{
    public interface ICallController
    {
        void Add(ExtendedCallInfo call);

        ICollection<ExtendedCallInfo> GetClientCalls(IClient client);
    }
}
