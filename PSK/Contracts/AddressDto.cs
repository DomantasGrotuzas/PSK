using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class AddressDto : DefaultDto
    {
        [StringLength(20, MinimumLength = 3)]
        public string Latitude { get; set; }

        [StringLength(20, MinimumLength = 3)]
        public string Longitude { get; set; }

        [Required]
        [StringLength(60)]
        public string Country { get; set; }

        [Required]
        [StringLength(60)]
        public string City { get; set; }

        [Required]
        [StringLength(60)]
        public string Street { get; set; }

        [Required(ErrorMessage = "The House number field is required.")]
        [StringLength(20)]
        public string HouseNumber { get; set; }
    }
}