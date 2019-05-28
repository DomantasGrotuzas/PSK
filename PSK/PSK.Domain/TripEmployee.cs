using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public Trip Trip { get; set; }

        [Required]
        public Employee Employee { get; set; }

        public AccommodationReservation AccommodationReservation { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Comment { get; set; }
    }
}