using System;
using System.Collections.Generic;

namespace PSK.Domain
{
    public class Trip
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Comment { get; set; }

        public Employee Organizer { get; set; }

        public Office StartLocation { get; set; }

        public Office EndLocation { get; set; }

        public IList<TripEmployee> Employees { get; set; }
    }
}