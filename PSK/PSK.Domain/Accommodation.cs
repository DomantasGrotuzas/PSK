using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PSK.Domain
{
    public class Accommodation : Entity
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(0, 100)]
        public int TotalSpaces { get; set; }

        [Required]
        public Office Office { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        public Guid AddressId { get; set; }

        public IList<AccommodationReservation> Reservations { get; set; }
    }
}