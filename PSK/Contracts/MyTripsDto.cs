using System;
using System.Collections.Generic;
using PSK.Domain;

namespace Contracts
{
    public class MyTripsDto
    {
        public IEnumerable<Trip> MyTrips { get; set; }

        public IEnumerable<Trip> MyOrganizedTrips { get; set; }

        public DateFilter DateFilter { get; set; }

        public bool IsOrganizer { get; set; }

        public Guid CurrentUserId { get; set; }
    }
}