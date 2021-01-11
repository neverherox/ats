using ats.ATS;
using System;

namespace ats.ats.Contracts
{
    public interface IPort
    {
        PortState State { get; set; }

        event EventHandler<PortState> StateChanged;

        event EventHandler<CallEventArg> OutgoingCall;

        event EventHandler<CallEventArg> IncomingCall;

        event EventHandler<CallEventArg> Answer;

        event EventHandler<CallEventArg> Drop;

        void IncomingCallFromStation(CallEventArg arg);

        void RegisterEventHandlersForPhone(IPhone phone);
    }
}
