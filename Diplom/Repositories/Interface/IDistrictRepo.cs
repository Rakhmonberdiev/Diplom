using Diplom.DTO.DistrictDtos;
using Diplom.Entities;
using Diplom.Helpers;
using System.Linq.Expressions;

namespace Diplom.Repositories.Interface
{
    public interface IDistrictRepo
    {
        Task<PagedList<DistrictDto>> GetAll(PaginationParams pageParams, string search);
        Task<DistrictsEn> GetById(Guid id);
        Task Create(DistrictsEn district);
        Task Update(DistrictsEn district);
        Task Delete(DistrictsEn district);
        Task SaveAsync();
    }
}
