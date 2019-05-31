using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PSK.Domain;

namespace PSK.DataAccess.Interfaces
{
    public interface IFileDataAccess
    {
        Task<File> Add(IFormFile formFile, string path, TripEmployee tripEmployee);
    }
}
