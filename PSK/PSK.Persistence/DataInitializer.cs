using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IServiceProvider _serviceProvider;

        public DataInitializer(RoleManager<UserRole> roleManager, IServiceProvider serviceProvider)
        {
            _roleManager = roleManager;
            _serviceProvider = serviceProvider;
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
    }
}
