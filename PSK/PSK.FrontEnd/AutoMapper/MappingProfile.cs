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
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.TelephoneNumber));
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.ConcurrencyStamp))
                .ForMember(dest => dest.TelephoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<Trip, TripDto>()
                .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees.Select(em => em.Id)))
                .ForMember(dest => dest.StartLocationId, opt => opt.MapFrom(src => src.StartLocation.Id.ToString()))
                .ForMember(dest => dest.EndLocationId, opt => opt.MapFrom(src => src.EndLocation.Id.ToString()))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.ToBase64String(src.Version)))
                .ForMember(dest => dest.OrganizerId, opt => opt.MapFrom(src => src.Organizer.Id))
                .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees));
            CreateMap<TripDto, Trip>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.FromBase64String(src.Version)));

            CreateMap<OfficeDto, Office>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.FromBase64String(src.Version)));
            CreateMap<Office, OfficeDto>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.ToBase64String(src.Version)));

            CreateMap<AddressDto, Address>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.FromBase64String(src.Version)));
            CreateMap<Address, AddressDto>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.ToBase64String(src.Version)));

            CreateMap<Accommodation, AccommodationDto>()
                .ForMember(dest => dest.OfficeId, opt => opt.MapFrom(src => src.Office.Id))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.ToBase64String(src.Version)));
            CreateMap<AccommodationDto, Accommodation>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.FromBase64String(src.Version)));
            
            CreateMap<AccommodationReservation, AccommodationReservationDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.ToBase64String(src.Version)));
            CreateMap<AccommodationReservationDto, AccommodationReservation>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<PurchasableStatus>(src.Status)))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.FromBase64String(src.Version)));

            CreateMap<TripEmployee, TripEmployeeDto>()
                .ForMember(dest => dest.CarReservationStatus, opt => opt.MapFrom(src => src.CarReservationStatus.ToString()))
                .ForMember(dest => dest.PlaneTicketStatus, opt => opt.MapFrom(src => src.PlaneTicketStatus.ToString()))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Employee.Id))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.ToBase64String(src.Version)))
                .ForMember(dest => dest.AccommodationId, opt => opt.MapFrom(src => 
                    src.AccommodationReservation.Accommodation != null ? src.AccommodationReservation.Accommodation.Id : Guid.Empty))
                .ForMember(dest => dest.Files, opt => opt.Ignore())
                .ForMember(dest => dest.ExistingFiles, opt => opt.MapFrom(src => src.Files))
                .ForMember(dest => dest.AccommodationName, opt => opt.MapFrom(src => src.AccommodationReservation.Accommodation.Name))
                .ForMember(dest => dest.EmployeeName, opt=>opt.MapFrom(src => src.Employee.FullName));
            CreateMap<TripEmployeeDto, TripEmployee>()
                .ForMember(dest => dest.CarReservationStatus,
                    opt => opt.MapFrom(src => Enum.Parse<PurchasableStatus>(src.CarReservationStatus)))
                .ForMember(dest => dest.PlaneTicketStatus,
                    opt => opt.MapFrom(src => Enum.Parse<PurchasableStatus>(src.PlaneTicketStatus)))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.FromBase64String(src.Version)))
                .ForMember(dest => dest.TripId, opt => opt.MapFrom(src => src.Trip.Id))
                .ForMember(dest => dest.Files, opt => opt.Ignore());

            CreateMap<File, FileDto>()
                .ForMember(dest => dest.IsSelected, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.ToBase64String(src.Version)));
            CreateMap<FileDto, File>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.FromBase64String(src.Version)))
                .ForMember(dest => dest.FullName, opt => opt.Ignore());

            CreateMap<Entity, DefaultDto>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.ToBase64String(src.Version)));
            CreateMap<DefaultDto, Entity>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.FromBase64String(src.Version)));
        }
    }
}