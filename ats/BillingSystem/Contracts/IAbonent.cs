using ats.ats.Contracts;

namespace ats.BillingSys.Contracts
{
    public interface IAbonent
    {
        string Name { get; }
        IPhone Phone { get; }
        double Balance { get; }
    }
}
