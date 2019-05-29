using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSK.Domain;

namespace PSK.DataAccess.Interfaces
{
    public interface IAccommodationDataAccess : IDataAccess<Accommodation>
    {
        Task<IEnumerable<Accommodation>> GetAllForOfficeWithReservations(Guid officeId);
    }
}