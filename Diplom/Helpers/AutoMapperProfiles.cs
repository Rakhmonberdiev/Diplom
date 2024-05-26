using AutoMapper;
using Diplom.DTO.DistrictDtos;
using Diplom.DTO.RouteEnDtos;
using Diplom.DTO.ScheduleDtos;
using Diplom.DTO.TicketDtos;
using Diplom.Entities;


namespace Diplom.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Маппинг из DistrictsEn в DistrictDto
            CreateMap<DistrictsEn, DistrictDto>();

            // Маппинг из DistrictCreateUpdateDto в DistrictsEn
            CreateMap<DistrictCreateUpdateDto, DistrictsEn>();

            // Маппинг из RouteEn в RouteEnDto
            // Переопределение некоторых полей с помощью ForMember
            CreateMap<RouteEn, RouteEnDto>()
                .ForMember(d => d.StarPoint, o => o.MapFrom(s => s.StartPoint.Title))
                .ForMember(d => d.EndPoint, o => o.MapFrom(s => s.EndPoint.Title));
            
            // Маппинг из RouteCreateDto в RouteEn
            CreateMap<RouteCreateDto, RouteEn>();

            // Маппинг из RouteUpdateDto в RouteEn
            CreateMap<RouteUpdateDto, RouteEn>();

            CreateMap<Schedule, ScheduleDto>();
            CreateMap<ScheduleCreateUpdateDto, Schedule>();

            CreateMap<Ticket, TicketDto>()
                .ForMember(d => d.Date, o => o.MapFrom(s => s.Date.Date))
                .ForMember(d => d.Start, o => o.MapFrom(s => s.Route.StartPoint.Title))
                .ForMember(d => d.End, o => o.MapFrom(s => s.Route.EndPoint.Title))
                .ForMember(d=>d.Price, o=>o.MapFrom(s => s.Route.Price.ToString()))
                .ForMember(d=>d.Schedule,o=>o.MapFrom(s => s.Schedule.Title));
        }
    }
}
