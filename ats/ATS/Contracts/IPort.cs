using ats.ATS;
using System;

namespace ats.ats.Contracts
{
    public interface IPort
    {
        PortState State { get; set; }

        event EventHandler<PortState> StateChanged;

        event EventHandler<CallEventArgs> OutgoingCall;

        event EventHandler<CallEventArgs> IncomingCall;

        event EventHandler<CallEventArgs> Answer;

        event EventHandler<CallEventArgs> Drop;

        void IncomingCallFromStation(CallEventArgs arg);

        void RegisterEventHandlersForPhone(IPhone phone);
    }
}
