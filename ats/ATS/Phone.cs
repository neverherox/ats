using ats.ats;
using ats.ats.Contracts;
using ats.ATS;
using ats.ATS.States;
using System;


namespace ats
{
    public class Phone : IPhone
    {
        public IPort Port { get; set; }
        public string PhoneNumber { get; set; }
        public CallEventArg Connection { get; set; }

        public event EventHandler<CallEventArg> OutgoingCall;
        public event EventHandler<CallEventArg> IncomingCall;
        public event EventHandler<CallEventArg> Answer;
        public event EventHandler<CallEventArg> Drop;
        protected virtual void OnOutgoingCall(object sender, CallEventArg arg)
        {
            OutgoingCall?.Invoke(sender, arg);
        }
        protected virtual void OnIncomingCall(object sender, CallEventArg arg)
        {
            IncomingCall?.Invoke(sender, arg);
        }
        protected virtual void OnAnswerCall(object sender, CallEventArg arg)
        {
            Answer?.Invoke(sender, arg);
        }
        protected virtual void OnDropCall(object sender, CallEventArg arg)
        {
            Drop?.Invoke(sender, arg);
        }

        public void Call(string to)
        {
            if (Port.State == PortState.Free)
            {
                Connection = new CallEventArg { TargetPhoneNumber = to, SourcePhoneNumber = PhoneNumber };
                OnOutgoingCall(this, Connection);
            }
        }
        public void IncomingCallFromPort(CallEventArg arg)
        {
            OnIncomingCall(this, arg);
        }
        public void AnswerCall()
        {
            if (Connection.SourcePhoneNumber != string.Empty)
            {
                OnAnswerCall(this, Connection);
            }
        }
        public void DropCall()
        {
            OnDropCall(this, Connection);
        }

    }
}
