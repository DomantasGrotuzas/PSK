using System.Collections.Generic;

namespace PSK.Domain
{
    public class Office : Entity
    {
        public string Name { get; set; }

        public Address Address { get; set; }

        public int AddressId { get; set; }

        public IList<Accommodation> Accommodations { get; set; }
    }
}