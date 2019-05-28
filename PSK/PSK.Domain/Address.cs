using System.ComponentModel.DataAnnotations;

namespace PSK.Domain
{
    public class Address : Entity
    {
        [StringLength(20, MinimumLength = 3)]
        public string Latitude { get; set; }

        [StringLength(20, MinimumLength = 3)]
        public string Longitude { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Country { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Street { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string HouseNumber { get; set; }

        public Office Office { get; set; }

        public Accommodation Accommodation { get; set; }
    }
}