using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PSK.DataAccess.Interfaces;
using PSK.Domain;
using PSK.Persistence;

namespace PSK.DataAccess
{
    public class AccommodationDataAccess : IDataAccess<Accommodation>
    {
        private readonly IDataContext _context;

        public AccommodationDataAccess(IDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Accommodation>> GetAll()
        {
            return await _context.Accommodations.Include(x => x.Address).Include(x => x.Office).ToListAsync();
        }

        public async Task<Accommodation> Get(Guid id)
        {
            return await _context.Accommodations.Include(x => x.Address).Include(x => x.Office).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Accommodation> Add(Accommodation accommodation)
        {
            var addedAccommodation = await _context.Accommodations.AddAsync(accommodation);
            await _context.SaveChangesAsync();
            return addedAccommodation.Entity;
        }

        public async Task Update(Accommodation accommodation)
        {
            _context.Accommodations.Update(accommodation);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            var accommodationToRemove = await Get(id);
            _context.Accommodations.Remove(accommodationToRemove);
            await _context.SaveChangesAsync();
        }
    }
}