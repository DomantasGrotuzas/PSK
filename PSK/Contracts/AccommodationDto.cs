using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class AccommodationDto : DefaultDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(0, 100)]
        public int? TotalSpaces { get; set; }

        [Required]
        public AddressDto Address { get; set; }

        [Required]
        public OfficeDto Office { get; set; }

        [Required]
        public Guid AddressId { get; set; }

        public IList<AccommodationReservationDto> Reservations { get; set; }

        [Required]
        public string OfficeId { get; set; }

        public IEnumerable<OfficeDto> AllOffices { get; set; }
    }
}