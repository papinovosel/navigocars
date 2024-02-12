using Core.Domain.Entities;
using Core.Dto;

namespace Core.IServices
{
    public interface ICarsService
    {
        public Task<List<CarResponse>> GetCarsAsync();
        public Task<CarResponse?> GetCarByIdAsync(Guid carId);

        public Task<CarResponse?> PostCarAsync(CarAddRequest carDto);

        public Task<bool> DeleteCarAsync(Guid carId);
    }
}
