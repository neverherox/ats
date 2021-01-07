using ats.ATS.States;
using System;

namespace ats.ats.Contracts
{
    public interface IPhone
    {
        string PhoneNumber { get; }
        string IncomingCallPhoneNumber { get; set; }
        string OutgoingCallPhoneNumber { get; set; }
        IPort Port { get; set; }

        event EventHandler<string> OutgoingCall;

        event EventHandler<string> IncomingCall;

        event EventHandler Answer;

        event EventHandler Drop;

        void Call(string to);
        void IncomingCallFrom(string from);
        void AnswerCall();
        void DropCall();
    }
}
