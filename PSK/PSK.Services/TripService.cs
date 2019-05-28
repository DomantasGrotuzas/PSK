using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSK.DataAccess;
using PSK.DataAccess.Interfaces;
using PSK.Domain;
using PSK.Domain.Identity;

namespace PSK.Services
{
    public class TripService : IService<Trip>
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
            var existingTrip = await _tripData.Get(id);
            existingTrip.Comment = trip.Comment;
            existingTrip.StartDate = trip.StartDate;
            existingTrip.EndDate = trip.EndDate;
            existingTrip.StartLocation = trip.StartLocation;

            await _tripData.Update(existingTrip);

            return trip;
        }

        public async Task Delete(Guid id)
        {
            await _tripData.Remove(id);
        }
    }
}