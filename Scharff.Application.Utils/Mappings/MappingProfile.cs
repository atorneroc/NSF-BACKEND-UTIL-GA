using AutoMapper;
using Scharff.Application.Queries.Parameter.ValidateExternalBaseStructure;
using Scharff.Domain.Entities;

namespace Scharff.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ValidateExternalBaseStructureQuery, ExternalBaseStructure>()
            .ForMember(dest => dest.DocumentNumber, opt => opt.MapFrom(src => src.DocumentNumber))
            .ForMember(dest => dest.BranchCodeIntOF, opt => opt.MapFrom(src => src.BranchCodeIntOF.ToUpper()))
            .ForMember(dest => dest.BusinessUnitCodeIntOF, opt => opt.MapFrom(src => src.BusinessUnitCodeIntOF.ToUpper()))
            .ForMember(dest => dest.ProductCodeIntOF, opt => opt.MapFrom(src => src.ProductCodeIntOF.ToUpper()))
            .ForMember(dest => dest.ServiceCodeIntOF, opt => opt.MapFrom(src => src.ServiceCodeIntOF.ToUpper()));
        }
    }
}
