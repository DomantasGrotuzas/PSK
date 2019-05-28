using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class OfficeDto : DefaultDto
    {
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        public AddressDto Address { get; set; }
    }
}