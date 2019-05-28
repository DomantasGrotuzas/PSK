using System.Collections.Generic;

namespace Contracts
{
    public class TripEmployeeDto : DefaultDto
    {
        public string PlaneTicketStatus { get; set; }

        public decimal? PlaneTicketPrice { get; set; }

        public string CarReservationStatus { get; set; }

        public decimal? CarReservationPrice { get; set; }

        public TripDto Trip { get; set; }

        public EmployeeDto Employee { get; set; }

        public AccommodationReservationDto AccommodationReservation { get; set; }

        public string Comment { get; set; }

        public string EmployeeId { get; set; }

        public IEnumerable<EmployeeDto> AllEmployees { get; set; }

        public IEnumerable<AccommodationDto> AvailableAccommodations { get; set; }
    }
}