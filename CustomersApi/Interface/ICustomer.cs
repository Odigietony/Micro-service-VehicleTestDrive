using CustomersApi.Models;

namespace CustomersApi.Interface
{
    public interface ICustomer
    {
        Task AddCustomer(Customer  customer);
    }
}
