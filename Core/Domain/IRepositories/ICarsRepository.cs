using Core.Domain.Entities;

namespace Core.Domain.IRepositories
{
    public interface ICarsRepository
    {
        public Task<List<Car>> GetCarsAsync();
        public Task<Car?> GetCarByIdAsync(Guid carId);

        public Task PostCarAsync(Car car);

        public void DeleteCar(Car car);

        public Task<bool> IsSavedAsync();
    }
}
