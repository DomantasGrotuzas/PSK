using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSK.Domain;

namespace PSK.DataAccess.Interfaces
{
    public interface IDataAccess<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(Guid id);

        Task<T> Add(T obj);

        Task Update(T obj);

        Task Remove(Guid id);
    }
}