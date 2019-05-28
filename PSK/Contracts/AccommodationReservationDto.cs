using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class AccommodationReservationDto : DefaultDto
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public decimal? Price { get; set; }

        public string Status { get; set; }

        public AccommodationDto Accommodation { get; set; }

        [Required]
        public TripEmployeeDto TripEmployee { get; set; }
    }
}