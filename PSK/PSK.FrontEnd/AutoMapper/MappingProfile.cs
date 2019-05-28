using System;
using AutoMapper;
using Contracts;
using PSK.Domain;
using PSK.Domain.Identity;
using System.Linq;
using PSK.Domain.Enums;

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
                .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees.Select(em => em.Id)))
                .ForMember(dest => dest.StartLocationId, opt => opt.MapFrom(src => src.StartLocation.Id.ToString()));
            CreateMap<TripDto, Trip>();

            CreateMap<OfficeDto, Office>();
            CreateMap<Office, OfficeDto>();

            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressDto>();

            CreateMap<AccommodationDto, Accommodation>();
            CreateMap<Accommodation, AccommodationDto>();

            CreateMap<AccommodationReservation, AccommodationReservationDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<AccommodationReservationDto, AccommodationReservation>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<PurchasableStatus>(src.Status)));

            CreateMap<TripEmployee, TripEmployeeDto>()
                .ForMember(dest => dest.CarReservationStatus, opt => opt.MapFrom(src => src.CarReservationStatus.ToString()))
                .ForMember(dest => dest.PlaneTicketStatus, opt => opt.MapFrom(src => src.PlaneTicketStatus.ToString()));
            CreateMap<TripEmployeeDto, TripEmployee>()
                .ForMember(dest => dest.CarReservationStatus, opt => opt.MapFrom(src => Enum.Parse<PurchasableStatus>(src.CarReservationStatus)))
                .ForMember(dest => dest.PlaneTicketStatus, opt => opt.MapFrom(src => Enum.Parse<PurchasableStatus>(src.PlaneTicketStatus))); ;

            CreateMap<Entity, DefaultDto>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version));
            CreateMap<DefaultDto, Entity>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version));
        }
    }
}