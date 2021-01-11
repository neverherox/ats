using ats.ats;
using ats.ats.Contracts;
using System;
using ats.ATS;

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
        public event EventHandler<CallEventArg> OutgoingCall;
        public event EventHandler<CallEventArg> IncomingCall;
        public event EventHandler<CallEventArg> Answer;
        public event EventHandler<CallEventArg> Drop;
        
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
                if (phone.Port.State == PortState.Busy)
                {
                    State = PortState.Free;
                }
                Drop?.Invoke(this, arg);
            };
            this.IncomingCall += (sender, arg) =>
            {
                phone.IncomingCallFromPort(arg);
            };
        }

        protected virtual void OnIncomingCall(object sender, CallEventArg arg)
        {
            IncomingCall?.Invoke(sender, arg);
        }
        public void IncomingCallFromStation(CallEventArg arg)
        {
            OnIncomingCall(this, arg);
        }
    }
}
