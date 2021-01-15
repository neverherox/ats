using ats.ats;
using ats.ats.Contracts;
using System;
using ats.ATS;
using ats.ATS.States;

namespace ats
{
    public class Port : IPort
    {
        private PortState state;
        public PortState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                StateChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<PortState> StateChanged;
        public event EventHandler<CallEventArgs> OutgoingCall;
        public event EventHandler<CallEventArgs> IncomingCall;
        public event EventHandler<CallEventArgs> Answer;
        public event EventHandler<CallEventArgs> Drop;

        public virtual void RegisterEventHandlersForPhone(IPhone phone)
        {
            phone.OutgoingCall += (sender, arg) =>
            {
                Console.WriteLine(arg.SourcePhoneNumber + " is trying to call " + arg.TargetPhoneNumber);
                State = PortState.Busy;
                OutgoingCall?.Invoke(this, arg);
            };
            phone.IncomingCall += (sender, arg) =>
            {
                Console.WriteLine(arg.SourcePhoneNumber + " is calling " + arg.TargetPhoneNumber);
                State = PortState.Busy;
                phone.Connection = arg;
            };
            phone.Answer += (sender, arg) =>
            {
                Console.WriteLine(arg.TargetPhoneNumber + " answered " + arg.SourcePhoneNumber);
                Answer?.Invoke(this, arg);
            };
            phone.Drop += (sender, arg) =>
            {
                State = PortState.Free;
                Drop?.Invoke(this, arg);
            };
            this.IncomingCall += (sender, arg) =>
            {
                phone.IncomingCallFromPort(arg);
            };
        }

        protected virtual void OnIncomingCall(object sender, CallEventArgs arg)
        {
            IncomingCall?.Invoke(sender, arg);
        }
        public void IncomingCallFromStation(CallEventArgs arg)
        {
            OnIncomingCall(this, arg);
        }

        public override bool Equals(object obj)
        {
            return obj is Port port &&
                   state == port.state;
        }

        public override int GetHashCode()
        {
            return 259708774 + state.GetHashCode();
        }
    }
}
