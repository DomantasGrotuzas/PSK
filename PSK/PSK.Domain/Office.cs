using System.Collections.Generic;

namespace PSK.Domain
{
    public class Office : Entity
    {
        public Address Address { get; set; }

        public IList<Accommodation> Accommodations { get; set; }
    }
}