using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class TripDto : DefaultDto
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
        public Guid OrganizerId { get; set; }

        [Required]
        public string StartLocationId { get; set; }

        [Required]
        public string EndLocationId { get; set; }

        [Required]
        public IList<TripEmployeeDto> Employees { get; set; }

        public IList<EmployeeDto> AllEmployees { get; set; }

        [Required]
        public IList<OfficeDto> Offices { get; set; }
    }
}
