using ats.ats.Contracts;

namespace ats.ATS.Controllers.Contracts
{
    public interface IPhoneController
    {
        void Add(IPhone phone);

        IPhone GetPhoneByPhoneNumber(string phoneNumber);
    }
}
