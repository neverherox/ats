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
        private IPortController portController;
        private ICallController callController;
        private IPhoneController phoneController;

        public event EventHandler<CallInfo> CallHappened;
        public Station()
        {
            portController = new PortController();
            callController = new CallController();
            phoneController = new PhoneController();
        }

        public void RegisterPhone(IPhone phone)
        {
            phoneController.Add(phone);
            IPort port = portController.CreatePort();
            portController.MapPhoneToPort(phone, port);

            RegisterPortEventHandlers(port);
            RegisterPhoneEventHandlers(phone);
        }

        public virtual void RegisterPhoneEventHandlers(IPhone phone)
        {
            phone.OutgoingCall += OnOutgoingCall;
            phone.Answer += OnIncomingCallAnswer;
            phone.Drop += OnIncomingCallDrop;
            phone.Drop += OnOutgoingCallDrop;

        }
        public virtual void RegisterPortEventHandlers(IPort port)
        {
            port.StateChanged += (sender, EventArgs) =>
            {
                Console.WriteLine("Port detected the State is changed to " + port.State);
            };
        }

        private void OnOutgoingCall(object sender, string to)
        {
            var port = portController.GetPortByPhoneNumber(to);
            var answerer = phoneController.GetPhoneByPhoneNumber(to);
            var caller = sender as IPhone;
            if (port != null && answerer != null)
            {
                if (port.State == PortState.Free)
                {
                    answerer.IncomingCallFrom(caller.PhoneNumber);
                    CallInfo unprocessedCall = new CallInfo
                    {
                        From = answerer.IncomingCallPhoneNumber,
                        To = answerer.PhoneNumber,
                        CallDate = DateTime.Now,
                        Duration = TimeSpan.Zero,
                        CallState = CallState.Unprocessed
                    };
                    callController.AddCall(unprocessedCall);
                    OnCallHappened(this, unprocessedCall);
                }
            }
        }
        private void OnIncomingCallAnswer(object sender, EventArgs args)
        {
            var answerer = sender as IPhone;
            CallInfo processedCall = callController.GetCall(answerer.IncomingCallPhoneNumber, answerer.PhoneNumber);
            if (processedCall != null)
            {
                processedCall.CallDate = DateTime.Now;
                processedCall.CallState = CallState.Processed;
            }
        }
        private void OnIncomingCallDrop(object sender, EventArgs args)
        {
            var phone = sender as IPhone;
            CallInfo call = callController.GetCall(phone.IncomingCallPhoneNumber, phone.PhoneNumber);
            if (call != null)
            {
                call.Duration = DateTime.Now - call.CallDate;
                callController.Remove(call);
                var callerPort = portController.GetPortByPhoneNumber(phone.IncomingCallPhoneNumber);
                callerPort.State = PortState.Free;
            }
            if (phone.Port.State == PortState.Busy)
            {
                phone.Port.State = PortState.Free;
            }
            phone.IncomingCallPhoneNumber = string.Empty;
        }
        private void OnOutgoingCallDrop(object sender, EventArgs args)
        {
            var phone = sender as IPhone;
            CallInfo call = callController.GetCall(phone.PhoneNumber, phone.OutgoingCallPhoneNumber);
            if (call != null)
            {
                call.Duration = DateTime.Now - call.CallDate;
                callController.Remove(call);
                var answererPort = portController.GetPortByPhoneNumber(phone.OutgoingCallPhoneNumber);
                answererPort.State = PortState.Free;
            }
            if (phone.Port.State == PortState.Busy)
            {
                phone.Port.State = PortState.Free;
            }
            phone.OutgoingCallPhoneNumber = string.Empty;
        }

        protected virtual void OnCallHappened(object sender, CallInfo callInfo)
        {
            CallHappened?.Invoke(sender, callInfo);
        }
    }
}
