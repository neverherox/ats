using ats.ats;
using ats.ats.Contracts;
using System;
using ats.ATS.States;

namespace ats
{
    public class Port : IPort
    {
        private PortState state;
        public event EventHandler StateChanged;
        public PortState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                OnStateChanged(this, null);
            }
        }
        public virtual void RegisterEventHandlersForPphone(IPhone phone)
        {
            phone.OutgoingCall += (sender, to) =>
            {
                phone.OutgoingCallPhoneNumber = to;
                State = PortState.Busy;
                Console.WriteLine(phone.PhoneNumber + " trying  to call " + to);
            };
            phone.IncomingCall += (sender, from) =>
            {
                phone.IncomingCallPhoneNumber = from;
                State = PortState.Busy;
                Console.WriteLine(from + " is calling " + phone.PhoneNumber);
            };
        }
        protected virtual void OnStateChanged(object sender, EventArgs args)
        {
            StateChanged?.Invoke(sender, args);
        }
    }
}
