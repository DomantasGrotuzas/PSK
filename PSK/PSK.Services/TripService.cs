using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSK.DataAccess.Interfaces;
using PSK.Domain;
using PSK.Services.Interfaces;

namespace PSK.Services
{
    public class TripService : ITripService
    {
        private readonly IDataAccess<Trip> _tripData;

        public TripService(IDataAccess<Trip> tripData)
        {
            _tripData = tripData;
        }

        public async Task<IEnumerable<Trip>> GetAll()
        {
            return await _tripData.GetAll();
        }

        public async Task<Trip> Get(Guid id)
        {
            return await _tripData.Get(id);
        }

        public async Task<Trip> Create(Trip trip)
        {
            return await _tripData.Add(trip);
        }

        public async Task<Trip> Update(Guid id, Trip trip)
        {
            trip.Id = id;
            await _tripData.Update(trip);

            return trip;
        }

        public async Task Delete(Guid id)
        {
            await _tripData.Remove(id);
        }

        public async Task<Trip> Merge(Trip trip1, Trip trip2)
        {
            return trip1;
        }
    }
}