using AutoMapper;
using Reservation.Application.Features.Services.Commands;
using Reservation.Application.Features.Services.Dtos;
using Reservation.Application.Features.Specialty.ViewModels;
using Reservation.Domain.Entities;

namespace Reservation.Application.Mapping
{
    public class SpecialtyProfile : Profile
    {
        public SpecialtyProfile()
        {
            CreateMap<CompanySpecialty, SpecialtyVM>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.SpecialtyId, opt => opt.MapFrom(src => src.SpecialtyId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Specialty.Name))
            .ForMember(dest => dest.RestMinute, opt => opt.MapFrom(src => src.Specialty.RestMinute))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<ServiceDto, ServiceCommand>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration));

            CreateMap<AddSpecialtyWithServicesDto, AddSpecialtyWithServicesCommand>()
            .ForMember(dest => dest.SpecialtyName, opt => opt.MapFrom(src => src.SpecialtyName))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId));

            CreateMap<AddServicesInSpecialtyDto, AddServicesInSpecialtyCommand>()
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.SpecialityId, opt => opt.MapFrom(src => src.SpecialityId));
        }
    }
}
