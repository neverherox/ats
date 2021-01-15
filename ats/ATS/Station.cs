using System;
using ats.ats;
using ats.ats.Contracts;
using ats.ATS;
using ats.ATS.Contracts;
using ats.ATS.Controllers;
using ats.ATS.Controllers.Contracts;
using ats.ATS.States;

namespace ats
{
    public class Station : IStation
    {
        private IPortService portService;
        private ICallService callService;
        public ICallService CallService { get => callService; }

        public Station()
        {
            portService = new PortService();
            callService = new CallService();
        }

        public void RegisterPhone(IPhone phone)
        {
            IPort port = portService.CreatePort();
            portService.MapPhoneToPort(phone, port);
            RegisterPortEventHandlers(port);
        }
        public void UnregisterPhone(IPhone phone)
        {
            portService.RemovePort(phone.PhoneNumber);
            phone.Port = null;
        }
        public virtual void RegisterPortEventHandlers(IPort port)
        {
            port.OutgoingCall += OnOutgoingCall;
            port.Answer += OnIncomingCallAnswer;
            port.Drop += OnCallDrop;
            port.StateChanged += (sender, state) =>
            {
                Console.WriteLine("Station registered a change in the port state to " + state);
            };
        }

        private void OnOutgoingCall(object sender, CallEventArgs arg)
        {
            var receiverPort = portService.GetPortByPhoneNumber(arg.TargetPhoneNumber);
            if (receiverPort != null)
            {
                if (receiverPort.State == PortState.Free)
                {
                    receiverPort.IncomingCallFromStation(arg);
                    arg.State = CallState.Unprocessed;
                    callService.RegisterUnprocessedCall(arg);
                }
                else
                {
                    Console.WriteLine("Receiver port is " + receiverPort.State);
                }
            }
        }
        private void OnIncomingCallAnswer(object sender, CallEventArgs arg)
        {
            arg.State = CallState.Processed;
            callService.RegisterProcessedCall(arg);
        }
        private void OnCallDrop(object sender, CallEventArgs arg)
        {
            if (arg.State == CallState.Processed || arg.State == CallState.Unprocessed)
            {
                var callerPort = portService.GetPortByPhoneNumber(arg.SourcePhoneNumber);
                var receiverPort = portService.GetPortByPhoneNumber(arg.TargetPhoneNumber);
                if (callerPort.State == PortState.Busy)
                {
                    callerPort.State = PortState.Free;
                }
                if (receiverPort.State == PortState.Busy)
                {
                    receiverPort.State = PortState.Free;
                }
                callService.RegisterDroppedCall(arg);
            }
            arg.SourcePhoneNumber = string.Empty;
            arg.TargetPhoneNumber = string.Empty;
        }
    }
}
