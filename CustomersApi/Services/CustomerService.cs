using Azure.Messaging.ServiceBus;
using CustomersApi.Data;
using CustomersApi.Interface;
using CustomersApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

            string connectionString = "<connection_string>";
            string queueName = "<queue_name>";
            // since ServiceBusClient implements IAsyncDisposable we create it with "await using"
            await using var client = new ServiceBusClient(connectionString);

            var objectmessage = JsonConvert.SerializeObject(customer);

            // create the sender
            ServiceBusSender sender = client.CreateSender(queueName);

            // create a message that we can send. UTF-8 encoding is used when providing a string.
            ServiceBusMessage message = new ServiceBusMessage(objectmessage);

            // send the message
            await sender.SendMessageAsync(message);
        }

    }
}
