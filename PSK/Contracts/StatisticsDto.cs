using System;
using System.Collections.Generic;
using System.Text;
using PSK.Domain;
using PSK.Domain.Identity;

namespace Contracts
{
    public class StatisticsDto
    {
        public PeriodTripCounter PeriodTripCounter { get; set; }
        public EmployeeTripCounter EmployeeTripCounter { get; set; }
        public IList<OfficeStatistics> MostPopularOffices { get; set; }
        public AllTripStatistics TripStatistics { get; set; }
        public IEnumerable<Employee> AllEmployees { get; set; }
    }

    public class PeriodTripCounter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TripCount { get; set; }
    }

    public class EmployeeTripCounter
    {
        public Guid SelectedEmployeeId { get; set; }
        public int TripCount { get; set; }
    }

    public class OfficeStatistics
    {
        public Office Office { get; set; }
        public int TripCount { get; set; }
    }

    public class AllTripStatistics
    {
        public IList<TripStatistic> TripStatistics { get; set; }
        public TripSortType TripSortType { get; set; }
    }

    public class TripStatistic
    {
        public Guid TripId { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
    }

    public enum TripSortType
    {
        MostExpensive,
        Cheapest,
        Longest,
        Shortest
    }
}
