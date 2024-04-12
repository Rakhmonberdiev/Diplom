using Diplom.Entities;
using System.Linq.Expressions;

namespace Diplom.Repositories.Interface
{
    public interface IDistrictRepo
    {
        Task<IEnumerable<DistrictsEn>> GetAll(string search);
        Task<DistrictsEn> GetById(Guid id);
        Task Create(DistrictsEn district);
        Task Update(DistrictsEn district);
        Task Delete(DistrictsEn district);
        
    }
}
