using PSK.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;

namespace PSK.Services.Interfaces
{
    public interface ITripService
    {
        //Task<IEnumerable<Trip>> GetAll();
        //Task<Trip> Get(Guid id);
        //Task<Trip> Create(Trip trip);
        //Task<Trip> Update(Guid id, Trip trip);
        //Task Delete(Guid id);
        Task Merge(TripMergeDto dto, Guid newOrganizerId);
    }
}
