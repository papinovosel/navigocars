using Api.Filters.ActionFilters;
using Core.Dto;
using Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("/api/cars/")]
    public class CarsController : ControllerBase
    {
        private readonly ILogger<CarsController> _logger;
        private readonly ICarsService _carsService;

        public CarsController(ILogger<CarsController> logger, ICarsService carsService)
        {
            _logger = logger;
            _carsService = carsService;
        }


        //GET: api/cars

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            _logger.LogInformation("GetCars action method in CarsController.");

            List<CarResponse> cars = await _carsService.GetCarsAsync();

            return Ok(cars);
        }


        //GET: api/cars/6FE3DB68-B315-46CA-B0AF-08DC1A688A3F

        [HttpGet("{carId}")]
        public async Task<IActionResult> GetCarById(Guid carId)
        {
            _logger.LogInformation("GetCarById action method in CarsController.");

            CarResponse? carResponse = await _carsService.GetCarByIdAsync(carId);
            if(carResponse == null)
            {
                _logger.LogError($"Car not found |ID: {carId}|");
                return Problem(detail: "Car not found", statusCode: 404, title: "Car search");
            }

            return Ok(carResponse);
        }


        //POST: api/cars/admin/post

        [HttpPost("admin/post")]
        [TypeFilter(typeof(CheckModelStateFilter))]
        public async Task<IActionResult> PostCar(CarAddRequest carAddRequest)
        {
            _logger.LogInformation("PostCar action method in CarsController.");

            CarResponse? carResponse = await _carsService.PostCarAsync(carAddRequest);

            if(carResponse == null)
            {
                _logger.LogError("Something went wrong while storing the order into the database");
                return Problem("Something went wrong while storing the order into the database", statusCode: 500);
            }

            return Ok(carResponse);
        }


        //DELETE: api/cars/admin/delete/6FE3DB68-B315-46CA-B0AF-08DC1A688A3F

        [HttpDelete("admin/delete/{carId}")]
        public async Task<IActionResult> DeleteCar(Guid carId)
        {
            _logger.LogInformation("DeleteCar action method in CarsController");

            if(! await _carsService.DeleteCarAsync(carId))
            {
                return Problem("Delete failed.", statusCode: 404, title: "Delete car");
            }

            return Ok("Delete successful");
        }
    }
}
