using ats.ats.Contracts;

namespace ats.ATS.Controllers.Contracts
{
    public interface IPortController
    {
        void MapPhoneToPort(IPhone phone, IPort port);
        IPort GetPortByPhoneNumber(string phoneNumber);
        IPort CreatePort();
    }
}
