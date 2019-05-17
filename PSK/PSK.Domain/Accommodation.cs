using System.Collections.Generic;

namespace PSK.Domain
{
    public class Accommodation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? TotalSpaces { get; set; }

        public Office Office { get; set; }

        public Address Address { get; set; }

        public IList<AccommodationReservation> Reservations { get; set; }
    }
}