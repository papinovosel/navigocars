using Core.Domain.Entities;
using Core.Domain.IRepositories;
using Core.Dto;
using Core.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Services
{
    public class CarsService : ICarsService
    {
        private readonly ILogger<CarsService> _logger;
        private readonly ICarsRepository _carsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CarsService(ILogger<CarsService> logger, ICarsRepository carsRepository, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _carsRepository = carsRepository;
            _webHostEnvironment = webHostEnvironment;
        }


        #region GET

        public async Task<List<CarResponse>> GetCarsAsync()
        {
            _logger.LogInformation("GetCarsAsync method in CarsService");

            List<Car> cars = await _carsRepository.GetCarsAsync();

            List<CarResponse> carResponses = cars.Select(c => c.ToCarResponse()).ToList();

            return carResponses;
        }

        public async Task<CarResponse?> GetCarByIdAsync(Guid carId)
        {
            _logger.LogInformation("GetCarByIdAsync method in CarsService");

            Car? car = await _carsRepository.GetCarByIdAsync(carId);

            if(car  == null)
            {
                return null;
            }

            return car.ToCarResponse();
        }

        #endregion

        #region POST

        public async Task<CarResponse?> PostCarAsync(CarAddRequest carAddRequest)
        {
            _logger.LogInformation("PostCarAsync method in CarsService");

            Car car = new Car()
            {
                Name = carAddRequest.Name,
                Description = carAddRequest.Description,
                Year = carAddRequest.Year,
                Quantity = carAddRequest.Quantity,
                RentType = carAddRequest.RentType,
                ImageFile = carAddRequest.ImageFile
            };

            string filePath = await CreateFile(car.ImageFile!);

            car.FilePath = filePath;
            car.FileName = filePath.Split('\\')[filePath.Split('\\').Length - 1];

            await _carsRepository.PostCarAsync(car);
            if (! await _carsRepository.IsSavedAsync())
            {
                _logger.LogError("Error occurred while storing the car in the database.");
                return null;
            }

            return car.ToCarResponse();
        }

        private async Task<string> CreateFile(IFormFile file)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string imageUploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

            string filePath = Path.Combine(imageUploadFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filePath;
        }

        #endregion

        #region DELETE

        public async Task<bool> DeleteCarAsync(Guid carId)
        {
            _logger.LogInformation("DeleteCarAsync method in CarsService");

            Car? car = await _carsRepository.GetCarByIdAsync(carId);

            if (car == null)
            {
                _logger.LogError($"Order was not found, ID: {carId}");
                return false;
            }

            _carsRepository.DeleteCar(car);
            if(! await _carsRepository.IsSavedAsync())
            {
                _logger.LogError("Error occurred while deleting the car from the database.");
                return false;
            }

            return true;
        }

        #endregion
    }
}
