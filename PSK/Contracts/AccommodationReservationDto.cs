using System;

namespace Contracts
{
    public class AccommodationReservationDto : DefaultDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal? Price { get; set; }

        public string Status { get; set; }

        public AccommodationDto Accommodation { get; set; }

        public TripEmployeeDto TripEmployee { get; set; }
    }
}