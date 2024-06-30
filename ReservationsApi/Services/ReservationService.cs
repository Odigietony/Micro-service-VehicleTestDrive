using System.Net;
using System.Net.Mail;
using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReservationsApi.Data;
using ReservationsApi.Interfaces;
using ReservationsApi.Models;

namespace ReservationsApi.Services
{
    public class ReservationService : IReservation
    {
        private ApiDbContext _context;
        public ReservationService()
        {
            _context = new ApiDbContext();
        }

        public async Task<List<Reservation>> GetReservations()
        {
            var connectionString = "<connection_string>";
            var queueName = "<queue_name>"; 
            await using var client = new ServiceBusClient(connectionString); 
            ServiceBusReceiver receiver = client.CreateReceiver(queueName); 

            IReadOnlyList<ServiceBusReceivedMessage> receivedMessages = await receiver.ReceiveMessagesAsync(10); 
            if(receivedMessages == null) return new List<Reservation>();

            foreach (var receivedMessage in receivedMessages)
            {
                var body = receivedMessage.Body.ToString();
                var createdMessage = JsonConvert.DeserializeObject<Reservation>(body);
                await _context.Reservations.AddAsync(createdMessage);
                await _context.SaveChangesAsync();
                await receiver.CompleteMessageAsync(receivedMessage);
            }

            return await _context.Reservations.ToListAsync();
        }

        public async Task UpdateMailStatus(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null && reservation.IsMailSent == false)
            {
                var smtpClient = new SmtpClient("smtp.live.com")
                {
                    Port = 578,
                    Credentials = new NetworkCredential("noreply-test-vehicletestdrive@outlook.com", "********"),
                    EnableSsl = true,
                };
                smtpClient.Send("noreply-test-vehicletestdrive@outlook.com", reservation.Email, "Vehicle Test Drive", "Your test drive has been confirmed.");
                reservation.IsMailSent = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
