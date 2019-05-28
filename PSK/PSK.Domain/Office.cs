using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PSK.Domain
{
    public class Office : Entity
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        public Guid AddressId { get; set; }

        public IList<Accommodation> Accommodations { get; set; }
    }
}