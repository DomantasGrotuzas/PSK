using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSK.Domain;

namespace PSK.DataAccess.Interfaces
{
    public interface ITripEmployeeDataAccess : IDataAccess<TripEmployee>
    {
        Task<IEnumerable<TripEmployee>> GetAll(Guid tripId);

        Task SetIsAccepted(Guid tripId, Guid employeeId);
    }
}