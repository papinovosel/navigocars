using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }

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

        public string? FileName { get; set; }

        public string? FilePath { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Image is required")]
        public IFormFile? ImageFile { get; set; }

        public virtual List<Order> Orders { get; set; } = [];
    }
}
