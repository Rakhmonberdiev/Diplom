using Diplom.Entities;

namespace Diplom.Repositories.Interface
{
    public interface IRouteEnRepo
    {
        Task<IEnumerable<RouteEn>> GetAllRoutes();
        Task<RouteEn> GetRouteById(Guid id); 

        Task Create(RouteEn route);

        Task<bool> IsRouteExist(Guid startPointId, Guid endPointId);

        Task SaveAsync();
    }
}
