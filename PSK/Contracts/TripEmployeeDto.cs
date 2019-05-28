using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class TripEmployeeDto : DefaultDto
    {

        public string PlaneTicketStatus { get; set; }

        public decimal? PlaneTicketPrice { get; set; }

        public string CarReservationStatus { get; set; }

        public decimal? CarReservationPrice { get; set; }

        [Required]
        public TripDto Trip { get; set; }

        [Required]
        public EmployeeDto Employee { get; set; }

        public AccommodationReservationDto AccommodationReservation { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Comment { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        public IEnumerable<EmployeeDto> AllEmployees { get; set; }

        public string AccommodationId { get; set; }

        public IEnumerable<AccommodationDto> AvailableAccommodations { get; set; }
    }
}