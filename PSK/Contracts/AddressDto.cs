using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class AddressDto : DefaultDto
    {
        public string Latitude { get; set; }


        public string Longitude { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Country { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string City { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Street { get; set; }

        [StringLength(20, MinimumLength = 3)]
        public string HouseNumber { get; set; }
    }
}