using System.Collections.Generic;
using System.Threading.Tasks;
using PSK.Domain;

namespace PSK.DataAccess.Interfaces
{
    public interface IOfficeDataAccess
    {
        Task<IEnumerable<Office>> GetAll();

        Task<Office> Get(int id);

        Task<Office> Add(Office office);

        Task Update(Office office);

        Task Remove(int id);
    }
}