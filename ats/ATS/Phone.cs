using ats.ats;
using ats.ats.Contracts;
using System;


namespace ats
{
    public class Phone : IPhone
    {
        public IPort Port { get; set; }
        public string PhoneNumber { get; set; }
        public string IncomingCallPhoneNumber { get; set; }
        public string OutgoingCallPhoneNumber { get; set; }

        public event EventHandler<string> OutgoingCall;
        public event EventHandler<string> IncomingCall;
        public event EventHandler Answer;
        public event EventHandler Drop;

        protected virtual void OnOutgoingCall(object sender, string to)
        {
            OutgoingCall?.Invoke(this, to);
        }
        protected virtual void OnIncomingCall(object sender, string from)
        {
            IncomingCall?.Invoke(sender, from);
        }
        protected virtual void OnAnswerCall(object sender, EventArgs args)
        {
            Answer.Invoke(this, args);
        }
        protected virtual void OnDropCall(object sender, EventArgs args)
        {
            Drop.Invoke(this, args);
        }
        public void Call(string to)
        {
            if (Port.PortState == PortState.Free)
            {
                OnOutgoingCall(this, to);
            }
        }
        public void IncomingCallFrom(string from)
        {
            OnIncomingCall(this, from);
        }
        public void AnswerCall()
        {
            if (Port.PortState == PortState.Busy && IncomingCallPhoneNumber != string.Empty)
            {
                OnAnswerCall(this, null);
            }
        }
        public void DropCall()
        {
            if (Port.PortState == PortState.Busy)
            {
                OnDropCall(this, null);
            }
        }
    }
}
