using System.Collections.Generic;

namespace PSK.Domain
{
    public class Office
    {
        public int Id { get; set; }

        public Address Address { get; set; }

        public IList<Accommodation> Accommodations { get; set; }
    }
}