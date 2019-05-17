using System.Collections.Generic;
using PSK.Domain.Enums;

namespace PSK.Domain
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }

        public string Email { get; set; }

        public string TelephoneNumber { get; set; }

        public IList<Trip> OrganizedTrips { get; set; }

        public IList<TripEmployee> Trips { get; set; }
    }
}