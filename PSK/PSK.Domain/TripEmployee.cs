using System.Collections.Generic;
using PSK.Domain.Enums;
using PSK.Domain.Identity;

namespace PSK.Domain
{
    public class TripEmployee : Entity
    {
        public PurchasableStatus PlaneTicketStatus { get; set; }

        public decimal? PlaneTicketPrice { get; set; }

        public PurchasableStatus CarReservationStatus { get; set; }

        public decimal? CarReservationPrice { get; set; }

        public Trip Trip { get; set; }

        public Employee Employee { get; set; }

        public AccommodationReservation AccommodationReservation { get; set; }

        public string Comment { get; set; }

        public IEnumerable<Employee> AllEmployees { get; set; }

        public IEnumerable<Accommodation> AvailableAccommodations { get; set; }
    }
}