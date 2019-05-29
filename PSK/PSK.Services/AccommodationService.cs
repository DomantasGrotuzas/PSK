using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using PSK.DataAccess.Interfaces;
using PSK.Domain;

namespace PSK.Services
{
    public class AccommodationService
    {
        private readonly IAccommodationDataAccess _accommodationDataAccess;

        public AccommodationService(IAccommodationDataAccess accommodationDataAccess)
        {
            _accommodationDataAccess = accommodationDataAccess;
        }

        public async Task<IEnumerable<AccommodationDto>> GetAvailableAccommodations()
        {

        }
    }
}
