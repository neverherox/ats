using ats.ats.Contracts;

namespace ats.ATS.Controllers.Contracts
{
    public interface IPortService
    {
        void MapPhoneToPort(IPhone phone, IPort port);
        IPort GetPortByPhoneNumber(string phoneNumber);
        IPort CreatePort();
    }
}
