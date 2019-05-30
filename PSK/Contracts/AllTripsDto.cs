using System;
using System.Collections.Generic;
using System.Text;
using PSK.Domain;

namespace Contracts
{
    public class AllTripsDto
    {
        public IEnumerable<Trip> AllTrips { get; set; }

        public DateFilter DateFilter { get; set; }
    }
}
