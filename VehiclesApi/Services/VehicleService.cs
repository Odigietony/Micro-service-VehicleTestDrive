using Microsoft.EntityFrameworkCore;
using VehiclesApi.Data;
using VehiclesApi.Interfaces;
using VehiclesApi.Models;

namespace VehiclesApi.Services
{
    public class VehicleService : IVehicle
    {
        private ApiDbContext _dbContext;

        public VehicleService()
        {
            _dbContext = new ApiDbContext();
        }
        public async Task<List<Vehicle>> GetAllVehicles()
        {
            return await _dbContext.Vehicles.ToListAsync();
        }

        public async Task<Vehicle> GetVehicleById(int id)
        {
            return await _dbContext.Vehicles.FindAsync(id);
        }

        public async Task AddVehicle(Vehicle vehicle)
        {
            await _dbContext.Vehicles.AddAsync(vehicle);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateVehicle(int id, Vehicle vehicle)
        {
            var vehicleToUpdate = await _dbContext.Vehicles.FindAsync(id);
            vehicleToUpdate.Name = vehicle.Name;
            vehicleToUpdate.Displacement = vehicle.Displacement;
            vehicleToUpdate.ImageUrl = vehicle.ImageUrl;
            vehicleToUpdate.Length = vehicle.Length;
            vehicleToUpdate.Height = vehicle.Height;
            vehicleToUpdate.Width = vehicle.Width;
            vehicleToUpdate.Price = vehicle.Price;
            vehicleToUpdate.MaxSpeed = vehicle.MaxSpeed;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteVehicle(int id)
        {
            var vehicle = await _dbContext.Vehicles.FindAsync(id);
            _dbContext.Vehicles.Remove(vehicle);
            await _dbContext.SaveChangesAsync();
        }
    }
}
