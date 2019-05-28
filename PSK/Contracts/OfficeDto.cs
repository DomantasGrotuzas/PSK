using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class OfficeDto : DefaultDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public AddressDto Address { get; set; }
    }
}