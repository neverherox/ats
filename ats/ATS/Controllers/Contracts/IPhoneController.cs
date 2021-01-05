using ats.ats.Contracts;

namespace ats.ATS.Controllers.Contracts
{
    public interface IPhoneController
    {
        IPhone CreatePhone(string phoneNumber);
        IPhone GetPhoneByPhoneNumber(string phoneNumber);
    }
}
