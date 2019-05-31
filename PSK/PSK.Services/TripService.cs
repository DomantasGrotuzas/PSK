using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Identity;
using PSK.DataAccess.Interfaces;
using PSK.Domain;
using PSK.Domain.Identity;
using PSK.Services.Interfaces;

namespace PSK.Services
{
    public class TripService : ITripService
    {
        private readonly ITripDataAccess _tripDataAccess;
        private readonly ITripEmployeeDataAccess _tripEmployeeDataAccess;
        private readonly UserManager<Employee> _userManager;

        public TripService(ITripDataAccess tripData, ITripEmployeeDataAccess tripEmployeeDataAccess, UserManager<Employee> userManager)
        {
            _tripDataAccess = tripData;
            _tripEmployeeDataAccess = tripEmployeeDataAccess;
            _userManager = userManager;
        }

        public async Task Merge(TripMergeDto dto, Guid newOrganizerId)
        {
            var primaryTrip = await _tripDataAccess.Get(dto.PrimaryTrip.Id);
            var secondaryTrip = await _tripDataAccess.GetWithEmployees(dto.SecondaryTrip.Id);

            foreach (var tripEmployee in secondaryTrip.Employees.ToList())
            {
                tripEmployee.TripId = dto.PrimaryTrip.Id;
                await _tripEmployeeDataAccess.Update(tripEmployee);
            }

            await _tripDataAccess.Remove(dto.SecondaryTrip.Id);

            primaryTrip.OrganizerId = newOrganizerId;
            primaryTrip.Comment = dto.PrimaryTrip.Comment;
            primaryTrip.StartDate = dto.PrimaryTrip.StartDate;
            primaryTrip.EndDate = dto.PrimaryTrip.EndDate;

            await _tripDataAccess.Update(primaryTrip);
        }
    }
}