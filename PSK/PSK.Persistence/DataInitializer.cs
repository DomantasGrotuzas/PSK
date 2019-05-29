using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PSK.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSK.Persistence
{
    public class DataInitializer : IDataInitializer
    {
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly UserManager<Employee> _userManager;

        public DataInitializer(RoleManager<UserRole> roleManager, IServiceProvider serviceProvider, UserManager<Employee> userManager)
        {
            _roleManager = roleManager;
            _serviceProvider = serviceProvider;
            _userManager = userManager;
        }

        public void InitializeDatabase()
        {
            using (var db = _serviceProvider.GetRequiredService<DataContext>())
            {
                var sqlDb = db.Database;

                sqlDb.Migrate();

                if (!db.Roles.AnyAsync().Result)
                {
                    InitializeRoles();                   
                }

                if (!db.Users.AnyAsync().Result)
                {
                    AddDefaultUser();
                }
            }
        }

        private void InitializeRoles()
        {
            if (!_roleManager.RoleExistsAsync("User").Result)
                _roleManager.CreateAsync(new UserRole { Name = "User" }).Wait();
            if (!_roleManager.RoleExistsAsync("Organizer").Result)
                _roleManager.CreateAsync(new UserRole { Name = "Organizer" }).Wait();
            if (!_roleManager.RoleExistsAsync("Admin").Result)
                _roleManager.CreateAsync(new UserRole { Name = "Admin" }).Wait();
        }

        private void AddDefaultUser()
        {
            var user = _userManager.FindByEmailAsync("admin@admin.com").Result;
            if(user != null)
                return;
            _userManager.CreateAsync(new Employee
            {
                Name = "Admin",
                Surname = "Admin",
                Email = "admin@admin.com",
                UserName = "admin@admin.com"
            }).Wait();

            user = _userManager.FindByEmailAsync("admin@admin.com").Result;

            _userManager.AddPasswordAsync(user, "admin").Wait();

            _userManager.AddToRolesAsync(user, _roleManager.Roles.Select(r => r.Name)).Wait();
        }
    }
}
