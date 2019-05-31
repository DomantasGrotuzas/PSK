using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class EmployeeDto : DefaultDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Last name field is required.")]
        [StringLength(60, MinimumLength = 3)]
        public string Surname { get; set; }
        
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Password { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public string TelephoneNumber { get; set; }

        [Required]
        public IList<RoleSelection> Roles { get; set; }

        public string FullName => $"{Name} {Surname}";

        public bool IsBusy;
    }

    public class RoleSelection
    {
        public string Role { get; set; }
        public bool IsSelected { get; set; }
    }
}
