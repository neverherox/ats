using ats.ats;
using ats.ats.Contracts;
using ats.ATS.Controllers.Contracts;
using System.Collections.Generic;

namespace ats.ATS.Controllers
{
    public class PortService : IPortService
    {
        private IDictionary<string, IPort> busyPorts;
        public PortService()
        {
            busyPorts = new Dictionary<string, IPort>();
        }
        public void MapPhoneToPort(IPhone phone, IPort port)
        {
            if (port != null && phone != null)
            {
                phone.Port = port;
                port.RegisterEventHandlersForPhone(phone);
                busyPorts.Add(phone.PhoneNumber, port);
            }
        }
        public IPort GetPortByPhoneNumber(string phoneNumber)
        {
            IPort port;
            if (busyPorts.TryGetValue(phoneNumber, out port))
            {
                return port;
            }
            return null;
        }
        public IPort CreatePort()
        {
            IPort port = new Port();
            port.State = PortState.Free;
            return port;
        }
    }
}
