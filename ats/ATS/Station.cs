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

        private void OnOutgoingCall(object sender, CallEventArg arg)
        {
            var receiverPort = portService.GetPortByPhoneNumber(arg.TargetPhoneNumber);
            if (receiverPort != null)
            {
                if (receiverPort.State == PortState.Free)
                {
                    receiverPort.IncomingCallFromStation(arg);
                    CallInfo unprocessedCall = new CallInfo
                    {
                        From = arg.SourcePhoneNumber,
                        To = arg.TargetPhoneNumber,
                        CallDate = DateTime.Now,
                        Duration = TimeSpan.Zero,
                        CallState = CallState.Unprocessed
                    };
                    callService.Add(unprocessedCall);
                }
                else
                {
                    Console.WriteLine("Receiver port is " + receiverPort.State);
                }
            }
        }
        private void OnIncomingCallAnswer(object sender, CallEventArg arg)
        {
            CallInfo processedCall = callService.GetCall(arg.SourcePhoneNumber, arg.TargetPhoneNumber);
            if (processedCall != null)
            {
                processedCall.CallDate = DateTime.Now;
                processedCall.CallState = CallState.Processed;
            }
        }
        private void OnCallDrop(object sender, CallEventArg arg)
        {
            if (arg.SourcePhoneNumber != string.Empty && arg.TargetPhoneNumber != string.Empty)
            {
                CallInfo call = callService.GetCall(arg.SourcePhoneNumber, arg.TargetPhoneNumber);
                if (call != null)
                {
                    call.Duration = DateTime.Now - call.CallDate;
                    callService.Remove(call);
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
                    callService.RegisterCall(call);
                }
                arg.SourcePhoneNumber = string.Empty;
                arg.TargetPhoneNumber = string.Empty;
            }
        }
    }
}
