using PSK.Domain.Enums;

namespace PSK.Domain
{
    public class TripEmployee
    {
        public int Id { get; set; }

        public PurchasableStatus PlaneTicketStatus { get; set; }

        public decimal? PlaneTicketPrice { get; set; }

        public PurchasableStatus CarReservationStatus { get; set; }

        public decimal? CarReservationPrice { get; set; }

        public Trip Trip { get; set; }

        public Employee Employee { get; set; }

        public AccommodationReservation AccommodationReservation { get; set; }

        public string Comment { get; set; }
    }
}