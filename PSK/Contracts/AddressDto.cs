using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class AddressDto : DefaultDto
        {
        [Range(-90D, -90D)]
        public double Latitude { get; set; }
        
        [Range(-180D, -180D)]
        public double Longitude { get; set; }

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