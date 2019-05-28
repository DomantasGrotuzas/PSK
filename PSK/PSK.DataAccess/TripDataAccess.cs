using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PSK.DataAccess.Interfaces;
using PSK.Domain;
using PSK.Persistence;

namespace PSK.DataAccess
{
    public class TripDataAccess : IDataAccess<Trip>
    {
        private readonly IDataContext _context;

        public TripDataAccess(IDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trip>> GetAll()
        {
            return await _context.Trips.Include(x => x.Employees).Include(x => x.EndLocation).Include(x => x.StartLocation).ToListAsync();
        }

        public async Task<Trip> Get(Guid id)
        {
            return await _context.Trips.Include(x => x.Employees).Include(x => x.EndLocation).Include(x => x.StartLocation).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Trip> Add(Trip trip)
        {
            var addedTrip = await _context.Trips.AddAsync(trip);
            await _context.SaveChangesAsync();
            return addedTrip.Entity;
        }

        public async Task Update(Trip trip)
        {
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