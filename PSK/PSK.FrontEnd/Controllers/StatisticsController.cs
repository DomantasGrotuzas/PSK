using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PSK.Domain.Identity;
using PSK.Services.Interfaces;

namespace PSK.FrontEnd.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;
        private readonly UserManager<Employee> _userManager;

        public StatisticsController(IStatisticsService statisticsService, UserManager<Employee> userManager)
        {
            _statisticsService = statisticsService;
            _userManager = userManager;
        }
        
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Statistics(StatisticsDto dto)
        {
            if(dto == null || 
               (dto.PeriodTripCounter == null && 
                dto.EmployeeTripCounter == null && 
                dto.MostPopularOffices == null && 
                dto.TripStatistics == null))
                dto = new StatisticsDto
                {
                    PeriodTripCounter = new PeriodTripCounter { StartDate = DateTime.Now, EndDate = DateTime.Now},
                    EmployeeTripCounter = new EmployeeTripCounter { SelectedEmployeeId = (await _userManager.GetUserAsync(User)).Id},
                    TripStatistics = new AllTripStatistics { TripSortType = TripSortType.MostExpensive},
                };
            return View(await _statisticsService.GetStatistics(dto));
        }
    }
}