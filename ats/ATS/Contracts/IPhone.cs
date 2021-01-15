using ats.ATS;
using System;

namespace ats.ats.Contracts
{
    public interface IPhone
    {
        IPort Port { get; set; }
        string PhoneNumber { get; }
        CallEventArgs Connection { set; }

        event EventHandler<CallEventArgs> OutgoingCall;

        event EventHandler<CallEventArgs> IncomingCall;

        event EventHandler<CallEventArgs> Answer;

        event EventHandler<CallEventArgs> Drop;
        void Call(string to);
        void IncomingCallFromPort(CallEventArgs arg);
        void AnswerCall();
        void DropCall();
    }
}
