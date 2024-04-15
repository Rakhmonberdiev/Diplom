using AutoMapper;
using Diplom.DTO.DistrictDtos;
using Diplom.DTO.RouteEnDtos;
using Diplom.Entities;


namespace Diplom.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DistrictsEn, DistrictDto>();
            CreateMap<DistrictCreateUpdateDto, DistrictsEn>();


            CreateMap<RouteEn, RouteEnDto>()
                .ForMember(d => d.StarPoint, o => o.MapFrom(s => s.StartPoint.Title))
                .ForMember(d => d.EndPoint, o => o.MapFrom(s => s.EndPoint.Title));

            CreateMap<RouteCreateDto, RouteEn>();
        }
    }
}
