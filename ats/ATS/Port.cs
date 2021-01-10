using ats.ats;
using ats.ats.Contracts;
using System;
using ats.ATS.States;
using ats.ATS;

namespace ats
{
    public class Port : IPort
    {
        public PortState State { get; set; }

        public event EventHandler<CallEventArg> OutgoingCall;
        public event EventHandler<CallEventArg> IncomingCall;
        public event EventHandler<CallEventArg> Answer;
        public event EventHandler<CallEventArg> Drop;
        
        public virtual void RegisterEventHandlersForPhone(IPhone phone)
        {
            phone.OutgoingCall += (sender, arg) =>
            {
                State = PortState.Busy;
                OutgoingCall?.Invoke(this, arg);
            };
            phone.IncomingCall += (sender, arg) =>
            {
                State = PortState.Busy;
                phone.Connection = arg;
            };
            phone.Answer += (sender, arg) =>
            {
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
