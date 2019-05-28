using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class AccommodationDto : DefaultDto
    {
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        public int? TotalSpaces { get; set; }

        public AddressDto Address { get; set; }
    }
}
