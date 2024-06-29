using CustomersApi.Data;
using CustomersApi.Interface;
using CustomersApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomersApi.Services
{
    public class CustomerService : ICustomer
    {
        private ApiDbContext _dbContext;
        public CustomerService()
        {
            _dbContext = new ApiDbContext();
        }
        public async Task AddCustomer(Customer customer)
        {
            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(v => v.Id == customer.VehicleId);
            if (vehicle == null)
            {
                await _dbContext.Vehicles.AddAsync(customer.Vehicle);
                await _dbContext.SaveChangesAsync();
            }

            customer.Vehicle = null;
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
        }

    }
}
