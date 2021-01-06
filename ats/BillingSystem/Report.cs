using ats.BillingSys.Contracts;
using System.Collections.Generic;
using System.Linq;


namespace ats.BillingSys
{
    public class Report : IReport
    {
        private ICollection<ExtendedCallInfo> calls;
        public ICollection<ExtendedCallInfo> Calls { get => calls.ToList(); set => calls = value; }

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
