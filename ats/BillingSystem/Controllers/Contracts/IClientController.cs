using ats.BillingSys;
using ats.BillingSys.Contracts;

namespace ats.BillingSys.Controllers.Contracts
{
    public interface IClientController
    {
        void Add(IClient client);
        IClient GetClientByPhoneNumber(string phoneNumber);
    }
}
