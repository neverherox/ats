using ats.ats.Contracts;
using ats.ATS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ats.BillingSystem
{
    public class BillingSystem
    {
        public ICollection<ExtendedCallInfo> calls;

        public BillingSystem()
        {
            calls = new List<ExtendedCallInfo>();
        }
        public virtual void RegisterStationEventHandlers(IStation station)
        {
            station.CallHappened += OnCallHappened;
        }
        private void OnCallHappened(object sender, CallInfo callInfo)
        {
            
        }
    }
}
