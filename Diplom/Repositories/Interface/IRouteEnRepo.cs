using Diplom.Entities;

namespace Diplom.Repositories.Interface
{
    public interface IRouteEnRepo
    {
        Task<IEnumerable<RouteEn>> GetAllRoutes();
        Task<RouteEn> GetRouteById(Guid id); 
    }
}
