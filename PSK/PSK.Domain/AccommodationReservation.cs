using System;
using PSK.Domain.Enums;

namespace PSK.Domain
{
    public class AccommodationReservation : Entity
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal? Price { get; set; }

        public PurchasableStatus Status { get; set; }

        public Accommodation Accommodation { get; set; }

        public Guid TripEmployeeId { get; set; }

        public TripEmployee TripEmployee { get; set; }
    }
}