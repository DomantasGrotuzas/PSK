using System;
using System.Collections.Generic;
using System.Text;
using PSK.Domain;

namespace Contracts
{
    public class TripMergeDto
    {
        public Trip PrimaryTrip { get; set; }
        public Trip SecondaryTrip { get; set; }
    }
}
