using ats.ats;
using ats.ats.Contracts;
using ats.ATS;
using ats.ATS.States;
using System;
using System.Collections.Generic;

namespace ats
{
    public class Phone : IPhone
    {
        public IPort Port { get; set; }
        public string PhoneNumber { get; set; }
        public CallEventArgs Connection { get; set; }

        public event EventHandler<CallEventArgs> OutgoingCall;
        public event EventHandler<CallEventArgs> IncomingCall;
        public event EventHandler<CallEventArgs> Answer;
        public event EventHandler<CallEventArgs> Drop;

        public Phone()
        {
            Connection = new CallEventArgs
            {
                SourcePhoneNumber = string.Empty,
                TargetPhoneNumber = string.Empty,
                State = CallState.None
            };
        }
        protected virtual void OnOutgoingCall(object sender, CallEventArgs arg)
        {
            OutgoingCall?.Invoke(sender, arg);
        }
        protected virtual void OnIncomingCall(object sender, CallEventArgs arg)
        {
            IncomingCall?.Invoke(sender, arg);
        }
        protected virtual void OnAnswerCall(object sender, CallEventArgs arg)
        {
            Answer?.Invoke(sender, arg);
        }
        protected virtual void OnDropCall(object sender, CallEventArgs arg)
        {
            Drop?.Invoke(sender, arg);
        }

        public void Call(string to)
        {
            if (Port != null && Port.State == PortState.Free)
            {
                Connection = new CallEventArgs
                {
                    TargetPhoneNumber = to,
                    SourcePhoneNumber = PhoneNumber,
                    State = CallState.None
                };
                OnOutgoingCall(this, Connection);
            }
        }
        public void IncomingCallFromPort(CallEventArgs arg)
        {
            if (Port != null && arg != null)
            {
                OnIncomingCall(this, arg);
            }
        }
        public void AnswerCall()
        {
            if (Port != null && Connection.SourcePhoneNumber != string.Empty)
            {
                OnAnswerCall(this, Connection);
            }
        }
        public void DropCall()
        {
            if (Port != null && Port.State == PortState.Busy)
            {
                OnDropCall(this, Connection);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Phone phone &&
                   EqualityComparer<IPort>.Default.Equals(Port, phone.Port) &&
                   PhoneNumber == phone.PhoneNumber;
        }

        public override int GetHashCode()
        {
            var hashCode = -249383132;
            hashCode = hashCode * -1521134295 + EqualityComparer<IPort>.Default.GetHashCode(Port);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PhoneNumber);
            return hashCode;
        }
    }
}
