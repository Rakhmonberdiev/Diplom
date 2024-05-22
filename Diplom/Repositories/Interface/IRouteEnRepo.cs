using Diplom.DTO.RouteEnDtos;
using Diplom.Entities;
using Diplom.Helpers;

namespace Diplom.Repositories.Interface
{
    public interface IRouteEnRepo
    {
        Task<PagedList<RouteEnDto>> GetAllRoutes(PaginationParams pageParams,string search);

        Task<IEnumerable<RouteEn>> GetLast8Routes();
        Task<RouteEn> GetRouteById(Guid id); 
        Task<RouteEn> GetByDistrictId(Guid fromId,Guid toId);
        Task Create(RouteEn route);

        Task<bool> IsRouteExist(Guid startPointId, Guid endPointId);

        Task Update (RouteEn route);
        Task Delete(RouteEn route);
        Task SaveAsync();
    }
}
