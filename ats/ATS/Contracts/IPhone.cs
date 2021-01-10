using ats.ATS;
using System;

namespace ats.ats.Contracts
{
    public interface IPhone
    {
        IPort Port { set; }
        string PhoneNumber { get; }
        CallEventArg Connection { set; }

        event EventHandler<CallEventArg> OutgoingCall;

        event EventHandler<CallEventArg> IncomingCall;

        event EventHandler<CallEventArg> Answer;

        event EventHandler<CallEventArg> Drop;
        void Call(string to);
        void IncomingCallFromPort(CallEventArg arg);
        void AnswerCall();
        void DropCall();
    }
}
