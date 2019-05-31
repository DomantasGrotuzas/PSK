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

        public async Task<Trip> Merge(Trip trip1, Trip trip2)
        {
            return trip1;
        }
    }
}