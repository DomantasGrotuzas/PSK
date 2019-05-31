using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PSK.Domain;

namespace PSK.DataAccess.Interfaces
{
    public interface IFileDataAccess
    {
        Task<File> Add(IFormFile formFile, string path, TripEmployee tripEmployee);

        Task<IList<File>> GetForTE(Guid tripEmployeeId);

        Task<File> Get(Guid id);

        Task Delete(Guid id);

        Task DeleteForTE(Guid tripEmployeeId);
    }
}
