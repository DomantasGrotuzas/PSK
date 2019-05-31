using System.Threading.Tasks;
using Contracts;

namespace PSK.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<StatisticsDto> GetStatistics(StatisticsDto dto);
    }
}