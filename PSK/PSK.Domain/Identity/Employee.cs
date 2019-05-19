using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using PSK.Domain.Enums;

namespace PSK.Domain.Identity
{
    public class Employee : IdentityUser<int>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public IList<Trip> OrganizedTrips { get; set; }

        public IList<TripEmployee> Trips { get; set; }
    }
}