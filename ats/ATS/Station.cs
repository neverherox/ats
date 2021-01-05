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

        public IPhone CreatePhone(string phoneNumber)
        {
            IPhone phone = phoneController.CreatePhone(phoneNumber);
            IPort port = portController.CreatePort();
            portController.MapPhoneToPort(phone, port);

            RegisterPortEventHandlers(port);
            RegisterPhoneEventHandlers(phone);
            return phone;
        }

        public virtual void RegisterPhoneEventHandlers(IPhone phone)
        {
            phone.OutgoingCall += OnOutgoingCall;
            phone.Answer += OnIncomingCallAnswer;
            phone.Drop += OnIncomingCallDrop;
        }
        public virtual void RegisterPortEventHandlers(IPort port)
        {
            port.StateChanged += (sender, EventArgs) =>
            {
                Console.WriteLine("Port detected the State is changed to " + port.PortState);
            };
        }

        private void OnOutgoingCall(object sender, string to)
        {
            var port = portController.GetPortByPhoneNumber(to);
            var answerer = phoneController.GetPhoneByPhoneNumber(to);
            var caller = sender as IPhone;
            if (port != null && answerer != null)
            {
                if (port.PortState == PortState.Free)
                {
                    answerer.IncomingCallFrom(caller.PhoneNumber);
                    CallInfo processedCall = new CallInfo
                    {
                        From = answerer.IncomingCallPhoneNumber,
                        To = answerer.PhoneNumber,
                        CallDate = DateTime.Now,
                        Duration = TimeSpan.Zero,
                        CallState = CallState.Unprocessed
                    };
                    callController.AddCall(processedCall);
                    OnCallHappened(this, processedCall);
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
            CallInfo processedCall = callController.GetCall(phone.IncomingCallPhoneNumber, phone.PhoneNumber);
            if (processedCall != null)
            {
                processedCall.Duration = DateTime.Now - processedCall.CallDate;
            }
            phone.IncomingCallPhoneNumber = string.Empty;
            phone.Port.PortState = PortState.Free;
        }
        protected virtual void OnCallHappened(object sender, CallInfo callInfo)
        {
            CallHappened?.Invoke(sender, callInfo);
        }
    }
}
