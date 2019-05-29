using PSK.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PSK.Domain
{
    public class Trip : Entity
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Comment { get; set; }

        [Required]
        public Employee Organizer { get; set; }

        [Required]
        public Guid OrganizerId { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public Office StartLocation { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public Office EndLocation { get; set; }

        public IList<TripEmployee> Employees { get; set; }
    }
}