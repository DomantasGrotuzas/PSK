using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PSK.Services
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(Guid id);

        Task<T> Create(T employee);

        Task<T> Update(Guid id, T employee);

        Task Delete(Guid id);
    }
}