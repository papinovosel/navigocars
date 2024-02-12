using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.Dto
{
    public class CarResponse
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Year is required")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Rent type is required")]
        public string? RentType { get; set; }

        public string? FilePath { get; set; }
    }

    public static class CarExtension
    {
        public static CarResponse ToCarResponse(this Car car)
        {
            return new CarResponse()
            {
                Id = car.Id,
                Name = car.Name,
                Year = car.Year,
                Description = car.Description,
                Quantity = car.Quantity,
                RentType = car.RentType,
                FilePath = car.FilePath
            };
        }
    }
}
