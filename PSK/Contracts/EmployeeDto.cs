using System.Collections.Generic;

namespace Contracts
{
    public class EmployeeDto : DefaultDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string TelephoneNumber { get; set; }

        public IList<RoleSelection> Roles { get; set; }

        public string FullName => $"{Name} {Surname}";
    }

    public class RoleSelection
    {
        public string Role { get; set; }
        public bool IsSelected { get; set; }
    }
}
