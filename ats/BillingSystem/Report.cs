using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ats.BillingSys
{
    public class Report
    {
        private ICollection<ExtendedCallInfo> calls;
        public Report()
        {
            calls = new List<ExtendedCallInfo>();
        }
        public Report(ICollection<ExtendedCallInfo> calls)
        {
            this.calls = calls;
        }
        public void Remove(ExtendedCallInfo call)
        {
            if (call != null)
            {
                calls.Remove(call);
            }
        }
        public void Clear()
        {
            calls.Clear();
        }
    }
}
