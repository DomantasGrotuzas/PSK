using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PSK.Domain.Identity;
using PSK.Persistence;
using PSK.Services.Interfaces;

namespace PSK.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IDataContext _dataContext;
        private readonly UserManager<Employee> _userManager;

        public StatisticsService(IDataContext dataContext, UserManager<Employee>userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }
        public async Task<StatisticsDto> GetStatistics(StatisticsDto dto)
        {
            dto.AllEmployees = await _userManager.Users.ToListAsync();

            dto.PeriodTripCounter.TripCount = await _dataContext.Trips.CountAsync(t =>
                (t.StartDate >= dto.PeriodTripCounter.StartDate && t.StartDate < dto.PeriodTripCounter.EndDate) ||
                (t.EndDate > dto.PeriodTripCounter.StartDate && t.EndDate <= dto.PeriodTripCounter.EndDate) ||
                (t.StartDate <= dto.PeriodTripCounter.StartDate && t.EndDate >= dto.PeriodTripCounter.EndDate));

            dto.EmployeeTripCounter.TripCount = await _dataContext.Trips
                .CountAsync(t => t.Employees.Any(te => te.EmployeeId == dto.EmployeeTripCounter.SelectedEmployeeId));

            dto.MostPopularOffices = _dataContext.Trips.Include(x => x.EndLocation).ToList().GroupBy(
                t => t.EndLocation.Id,
                (officeId, trips) => new OfficeStatistics
                {
                    Office = trips.FirstOrDefault().EndLocation,
                    TripCount = trips.Count()
                }).ToList();

            var tripStatistics = _dataContext.Trips
                .Select(t => new TripStatistic
                {
                    TripId = t.Id,
                    Price = t.Employees.Sum(te =>
                        (te.CarReservationPrice ?? 0) +
                        (te.PlaneTicketPrice ?? 0) +
                        (te.AccommodationReservation.Price ?? 0)),
                    Duration = (t.EndDate - t.StartDate).Days + 1
                });

            switch (dto.TripStatistics.TripSortType)
            {
                case TripSortType.Cheapest:
                    dto.TripStatistics.TripStatistics = await tripStatistics.OrderBy(t => t.Price).ToListAsync();
                    break;
                case TripSortType.MostExpensive:
                    dto.TripStatistics.TripStatistics = await tripStatistics.OrderByDescending(t => t.Price).ToListAsync();
                    break;
                case TripSortType.Shortest:
                    dto.TripStatistics.TripStatistics = await tripStatistics.OrderBy(t => t.Duration).ToListAsync();
                    break;
                case TripSortType.Longest:
                    dto.TripStatistics.TripStatistics = await tripStatistics.OrderByDescending(t => t.Duration).ToListAsync();
                    break;
            }

            return dto;
        }
    }
}
