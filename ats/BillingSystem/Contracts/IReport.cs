using ats.BillingSys;
using System.Collections.Generic;

namespace ats.BillingSys.Contracts
{
    public interface IReport
    {
        ICollection<ExtendedCallInfo> Calls { get; set; }
        void Remove(ExtendedCallInfo call);
        void Clear();
    }
}
