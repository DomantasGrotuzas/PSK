﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PSK.DataAccess.Interfaces;
using PSK.Domain;
using PSK.Persistence;

namespace PSK.DataAccess
{
    public class TripEmployeeDataAccess : IDataAccess<TripEmployee>
    {
        private readonly IDataContext _context;

        public TripEmployeeDataAccess(IDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TripEmployee>> GetAll()
        {
            return await _context.TripEmployees.Include(x => x.Trip).Include(x => x.Employee)
                .Include(x => x.AccommodationReservation)
                .ToListAsync();
        }

        public async Task<TripEmployee> Get(Guid id)
        {
            return await _context.TripEmployees.Include(x => x.Trip).Include(x => x.Employee)
                .Include(x => x.AccommodationReservation)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TripEmployee> Add(TripEmployee tripEmployee)
        {
            var addedEmpoyee = await _context.TripEmployees.AddAsync(tripEmployee);
            await _context.SaveChangesAsync();
            return addedEmpoyee.Entity;
        }

        public async Task Update(TripEmployee tripEmployee)
        {
            _context.TripEmployees.Update(tripEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            var tripEmployeeToRemove = await Get(id);
            _context.TripEmployees.Remove(tripEmployeeToRemove);
            await _context.SaveChangesAsync();
        }
    }
}