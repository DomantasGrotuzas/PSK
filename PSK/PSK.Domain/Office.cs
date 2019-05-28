using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PSK.Domain
{
    public class Office : Entity
    {
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        public Address Address { get; set; }

        public Guid AddressId { get; set; }

        public IList<Accommodation> Accommodations { get; set; }
    }
}