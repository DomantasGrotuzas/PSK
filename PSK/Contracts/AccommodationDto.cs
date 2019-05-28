using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class AccommodationDto : DefaultDto
    {
        [StringLength(60, MinimumLength = 3)] public string Name { get; set; }

        public int? TotalSpaces { get; set; }

        public AddressDto Address { get; set; }

        public OfficeDto Office { get; set; }

        public Guid AddressId { get; set; }

        public IList<AccommodationReservationDto> Reservations { get; set; }

        public string OfficeId { get; set; }

        public IEnumerable<OfficeDto> AllOffices { get; set; }
    }
}