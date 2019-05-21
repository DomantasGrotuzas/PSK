using AutoMapper;
using Contracts;
using PSK.Domain;
using PSK.Domain.Identity;

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

            CreateMap<OfficeDto, Office>();
            CreateMap<Office, OfficeDto>();
            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressDto>();

            CreateMap<Entity, DefaultDto>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version));
            CreateMap<DefaultDto, Entity>()
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version));
        }
    }
}