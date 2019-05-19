using Microsoft.AspNetCore.Identity;
using PSK.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PSK.Persistence
{
    public class DataInitializer : IDataInitializer
    {
        private readonly RoleManager<UserRole> _roleManager;

        public DataInitializer(RoleManager<UserRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public void InitializeRoles()
        {
            if (!_roleManager.RoleExistsAsync("User").Result)
                _roleManager.CreateAsync(new UserRole { Name = "User" }).Wait();
            if (!_roleManager.RoleExistsAsync("Organizer").Result)
                _roleManager.CreateAsync(new UserRole { Name = "Organizer" }).Wait();
            if (! _roleManager.RoleExistsAsync("Admin").Result)
                _roleManager.CreateAsync(new UserRole { Name = "Admin" }).Wait();
        }
    }
}
