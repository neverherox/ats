using ats.ats;
using ats.ats.Contracts;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ats
{
    public class Port : IPort
    {
        private PortState portState;
        public event EventHandler StateChanged;
        public PortState PortState
        {
            get
            {
                return portState;
            }
            set
            {
                portState = value;
                OnStateChanged(this, null);
            }
        }
        public virtual void RegisterEventHandlersForPphone(IPhone phone)
        {
            phone.OutgoingCall += (sender, to) =>
            {
                var caller = sender as IPhone;
                caller.OutgoingCallPhoneNumber = to;
                PortState = PortState.Busy;
                Console.WriteLine(caller.PhoneNumber + " trying  to call " + to);
            };
            phone.IncomingCall += (sender, from) =>
            {
                var answerer = sender as IPhone;
                answerer.IncomingCallPhoneNumber = from;
                PortState = PortState.Busy;
                Console.WriteLine(from + " is calling " + answerer.PhoneNumber);
            };
        }
        protected virtual void OnStateChanged(object sender, EventArgs args)
        {
            StateChanged?.Invoke(sender, args);
        }
    }
}
