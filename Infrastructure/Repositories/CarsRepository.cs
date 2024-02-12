using Core.Domain.Entities;
using Core.Domain.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CarsRepository : ICarsRepository
    {
        private readonly ApplicationDbContext _db;

        public CarsRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        //GET

        public async Task<List<Car>> GetCarsAsync()
        {
            return await _db.Cars.ToListAsync();
        }

        public async Task<Car?> GetCarByIdAsync(Guid carId)
        {
            return await _db.Cars.FirstOrDefaultAsync(c => c.Id == carId);
        }


        //POST

        public async Task PostCarAsync(Car car)
        {
            await _db.Cars.AddAsync(car);
        }


        //DELETE

        public void DeleteCar(Car car)
        {
            _db.Cars.Remove(car);
        }


        //VALIDATION

        public async Task<bool> CarExistsAsync(Guid carId)
        {
            return await _db.Cars.AnyAsync(c => c.Id == carId);
        }

        public async Task<bool> IsSavedAsync()
        {
            int saved = await _db.SaveChangesAsync();

            return saved > 0;
        }
    }
}
