using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PSK.DataAccess.Interfaces;
using PSK.Domain;
using PSK.Persistence;

namespace PSK.DataAccess
{
    public class OfficeDataAccess : IDataAccess<Office>
    {
        private readonly IDataContext _context;

        public OfficeDataAccess(IDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Office>> GetAll()
        {
            return await _context.Offices.Include(x => x.Address).ToListAsync();
        }

        public async Task<Office> Get(Guid id)
        {
            return await _context.Offices.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Office> Add(Office office)
        {
            var addedOffice = await _context.Offices.AddAsync(office);
            await _context.SaveChangesAsync();
            return addedOffice.Entity;
        }

        public async Task Update(Office office)
        {
            _context.Offices.Update(office);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            var officeToRemove = await Get(id);
            _context.Offices.Remove(officeToRemove);
            await _context.SaveChangesAsync();
        }
    }
}