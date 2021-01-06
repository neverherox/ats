using ats.ats.Contracts;
using ats.BillingSys.Contracts;

namespace ats.BillingSys
{
    public class Client : IClient
    {
        public string Name { get; set; }
        public IPhone Phone { get; set; }
        public double Balance { get; set; }
    }
}
