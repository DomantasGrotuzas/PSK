using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PSK.DataAccess.Interfaces;
using PSK.Domain;
using PSK.Persistence;
using Contracts.Extensions;

namespace PSK.DataAccess
{
    public class TripDataAccess : ITripDataAccess
    {
        private readonly IDataContext _context;

        public TripDataAccess(IDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trip>> GetMergeable(Trip trip)
        {
            trip.StartDate = trip.StartDate.ClearHours();
            trip.EndDate = trip.EndDate.ClearHours();

            var startDate = trip.StartDate;
            var endDate = trip.EndDate;
            return await _context.Trips.Include(t => t.Organizer)
                .Where(t => t.StartDate <= startDate.AddDays(1) &&
                            t.StartDate >= startDate.AddDays(-1) &&
                            t.EndDate <= endDate.AddDays(1) &&
                            t.EndDate >= endDate.AddDays(-1) &&
                            t.Id != trip.Id &&
                            t.StartDate > DateTime.Now.Date)
                .OrderByDescending(x => x.StartDate).ToListAsync();
        }

        public async Task<IEnumerable<Trip>> GetAll()
        {
            return await _context.Trips.Include(x => x.Employees).Include(x => x.EndLocation).Include(x => x.StartLocation)
                .Include(x => x.Organizer).OrderByDescending(x => x.StartDate).ToListAsync();
        }

        public async Task<Trip> Get(Guid id)
        {
            return await _context.Trips
                .Include(x => x.Employees)
                    .ThenInclude(e => e.AccommodationReservation)
                .Include(x => x.EndLocation)
                .Include(x => x.StartLocation)
                .Include(x => x.Organizer)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Trip>> GetTripsForEmployee(Guid employeeId)
        {
            return await _context.Trips.Include(x => x.Employees).ThenInclude(x => x.Employee)
                .Include(x => x.EndLocation).Include(x => x.StartLocation).Include(x => x.Organizer)
                .Where(x => x.Employees.Any(y => y.Employee.Id == employeeId))
                .OrderByDescending(x => x.StartDate).ToListAsync();
        }

        public async Task<IEnumerable<Trip>> GetTripsForOrganizator(Guid employeeId)
        {
            return await _context.Trips.Include(x => x.Employees).ThenInclude(x => x.Employee)
                .Include(x => x.EndLocation).Include(x => x.StartLocation).Include(x => x.Organizer)
                .Where(x => x.Organizer.Id == employeeId)
                .OrderByDescending(x => x.StartDate).ToListAsync();
        }

        public async Task<Trip> GetWithEmployees(Guid id)
        {
            return await _context.Trips.Include(x => x.Employees).ThenInclude(x => x.Employee)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Trip> Add(Trip trip)
        {
            trip.StartDate = trip.StartDate.ClearHours();
            trip.EndDate = trip.EndDate.ClearHours();

            var addedTrip = await _context.Trips.AddAsync(trip);
            await _context.SaveChangesAsync();
            return addedTrip.Entity;
        }

        public async Task Update(Trip trip)
        {
            trip.StartDate = trip.StartDate.ClearHours();
            trip.EndDate = trip.EndDate.ClearHours();

            _context.Trips.Update(trip);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            var tripToRemove = await Get(id);
            _context.Trips.Remove(tripToRemove);
            await _context.SaveChangesAsync();
        }
    }
}