using Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Core.Dto
{
    public class OrderAddRequest
    {
        [Required(ErrorMessage = "CarId is required")]
        public Guid CarId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? PhoneNumber { get; set; }

        public string? Comment { get; set; }

        public bool Insurance { get; set; } = false;

        [Required(ErrorMessage = "Date of start is required")]
        public DateTime StartOrder { get; set; }

        [Required(ErrorMessage = "Date of end is required")]
        public DateTime EndOrder { get; set; }
    }
}
