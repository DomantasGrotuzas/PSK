using System;
using System.Collections.Generic;
using System.Text;
using PSK.Domain;

namespace Contracts
{
    public class TripMergeSelectionDto
    {
        public Trip PrimaryTrip { get; set; }
        public IList<Trip> Trips { get; set; }
    }
}
