using AutoMapper;
using Diplom.DTO.DistrictDtos;
using Diplom.Entities;


namespace Diplom.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DistrictsEn, DistrictDto>();
            CreateMap<DistrictCreateUpdateDto, DistrictsEn>();
        }
    }
}
