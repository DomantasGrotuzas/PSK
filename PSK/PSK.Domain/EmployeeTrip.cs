using PSK.Domain.Enums;
using PSK.Domain.Identity;

namespace PSK.Domain
{
    public class EmployeeTrip : Entity
    {
        public PurchasableStatus PlaneTicketStatus { get; set; }

        public decimal? PlaneTicketPrice { get; set; }

        public PurchasableStatus CarReservationStatus { get; set; }

        public decimal? CarReservationPrice { get; set; }

        public Trip Trip { get; set; }

        public Employee Employee { get; set; }
    }
}