using System;
using System.ComponentModel.DataAnnotations;
using PSK.Domain.Enums;

namespace PSK.Domain
{
    public class AccommodationReservation : Entity
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public decimal? Price { get; set; }

        public PurchasableStatus Status { get; set; }

        public Accommodation Accommodation { get; set; }

        [Required]
        public Guid TripEmployeeId { get; set; }

        public TripEmployee TripEmployee { get; set; }
    }
}