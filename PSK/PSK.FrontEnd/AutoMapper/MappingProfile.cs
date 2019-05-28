using AutoMapper;
using Contracts;
using PSK.Domain;
using PSK.Domain.Identity;
using System.Linq;

namespace PSK.FrontEnd.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.MapFrom(src => src.Version));
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.ConcurrencyStamp));

            CreateMap<Trip, TripDto>()
                .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees.Select(em => em.Id)));
            CreateMap<TripDto, Trip>();

            CreateMap<OfficeDto, Office>();
            CreateMap<Office, OfficeDto>();

            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressDto>();

            CreateMap<AccommodationDto, Accommodation>();
            CreateMap<Accommodation, AccommodationDto>();

            CreateMap<AccommodationReservation, AccommodationReservationDto>();
            CreateMap<AccommodationReservationDto, AccommodationReservation>();

            CreateMap<TripEmployee, TripEmployeeDto>();
            CreateMap<TripEmployeeDto, TripEmployee>();

            CreateMap<Entity, DefaultDto>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version));
            CreateMap<DefaultDto, Entity>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version));
        }
    }
}