using ats.BillingSys;
using ats.BillingSys.Contracts;

namespace ats.BillingSys.Controllers.Contracts
{
    public interface IAbonentService
    {
        void Add(IAbonent abonent);
        IAbonent GetAbonentByPhoneNumber(string phoneNumber);
    }
}
