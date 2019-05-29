using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using PSK.DataAccess.Interfaces;
using PSK.Domain;

namespace PSK.Services
{
    public class AccommodationService : IAccommodationService
    {
        private readonly IAccommodationDataAccess _accommodationDataAccess;

        private readonly IDataAccess<Trip> _tripDataAccess;

        private readonly IMapper _mapper;

        public AccommodationService(IAccommodationDataAccess accommodationDataAccess, IMapper mapper, IDataAccess<Trip> tripDataAccess)
        {
            _accommodationDataAccess = accommodationDataAccess;
            _mapper = mapper;
            _tripDataAccess = tripDataAccess;
        }

        public async Task<IEnumerable<AccommodationDto>> GetAvailableAccommodations(Guid tripId)
        {
            var trip = await _tripDataAccess.Get(tripId);
            var accommodationsForOffice = (await _accommodationDataAccess
                .GetAllForOfficeWithReservations(trip.EndLocation.Id)).ToList();

            var accommodationsDto = _mapper.Map<List<AccommodationDto>>(accommodationsForOffice);

            foreach (var accommodationDto in accommodationsDto)
            {
                var accommodation = accommodationsForOffice.FirstOrDefault(x => x.Id == accommodationDto.Id);
                if (accommodation != null)
                {
                    if (accommodation.TotalSpaces == 0)
                    {
                        accommodationDto.SpacesAvailable = null;
                    }
                    else
                    {
                        var spacesTaken = accommodation.Reservations.Count(x => 
                            (x.StartDate >= trip.StartDate && x.StartDate < trip.EndDate) || 
                            (x.EndDate > trip.StartDate && x.EndDate <= x.StartDate));
                        accommodationDto.SpacesAvailable = accommodation.TotalSpaces - spacesTaken;
                    }
                }
            }

            return accommodationsDto.OrderByDescending(x => x.SpacesAvailable);
        }
    }
}
