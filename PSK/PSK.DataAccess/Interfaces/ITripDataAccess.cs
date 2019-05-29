using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSK.Domain;

namespace PSK.DataAccess.Interfaces
{
    public interface ITripDataAccess : IDataAccess<Trip>
    {
        Task<Trip> GetWithEmployees(Guid id);
    }
}