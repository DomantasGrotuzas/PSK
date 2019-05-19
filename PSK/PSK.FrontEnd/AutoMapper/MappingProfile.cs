using System;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Identity;
using PSK.Domain;
using PSK.Domain.Enums;
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
        }
    }
}