using AutoMapper;
using AutoMapper.QueryableExtensions;
using Diplom.Data;
using Diplom.DTO.RouteEnDtos;
using Diplom.Entities;
using Diplom.Helpers;
using Diplom.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Repositories.Implementation
{
    public class RouteEnRepo : IRouteEnRepo
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public RouteEnRepo(AppDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task Create(RouteEn route)
        {
            await _db.Routes.AddAsync(route);
            await SaveAsync();
        }

        public async Task Delete(RouteEn route)
        {
            _db.Routes.Remove(route);
            await SaveAsync();
        }

        public async Task<PagedList<RouteEnDto>> GetAllRoutes(PaginationParams pageParams,string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var stringQuery = _db.Routes
                    .Where(
                    x => x.StartPoint.Title.ToLower().Contains(search.ToLower()) ||
                    x.EndPoint.Title.ToLower().Contains(search.ToLower()))
                    .Include(x => x.StartPoint)
                    .Include(x => x.EndPoint)
                    .ProjectTo<RouteEnDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .OrderByDescending(s => s.Created);
                return await PagedList<RouteEnDto>.CreateAsync(stringQuery, pageParams.PageNumber, pageParams.PageSize);
            }
            var query = _db.Routes
                .Include(x=>x.StartPoint)
                .Include(x=>x.EndPoint)
                .ProjectTo<RouteEnDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .OrderByDescending(_ => _.Created);
            return await PagedList<RouteEnDto>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<RouteEn> GetByDistrictId(Guid fromId, Guid toId)
        {
            return await _db.Routes
                .Include(x=>x.StartPoint)
                .Include(x=>x.EndPoint)
                .SingleOrDefaultAsync(r => r.StartPointId == fromId && r.EndPointId == toId);
        }

        public async Task<IEnumerable<RouteEn>> GetLast8Routes()
        {
           return await _db.Routes
                .Include(x=>x.StartPoint)
                .Include(x=>x.EndPoint)
                .OrderByDescending(x=>x.Created)
                .Take(8)
                .ToListAsync();
        }

        public async Task<RouteEn> GetRouteById(Guid id)
        {
            return await _db.Routes
                .Include(x=>x.StartPoint)
                .Include(x=>x.EndPoint)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsRouteExist(Guid startPointId, Guid endPointId)
        {
            bool exist = await _db.Routes.AnyAsync(r=>r.StartPointId==startPointId && r.EndPointId==endPointId);
            return exist;
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task Update(RouteEn route)
        {
            _db.Routes.Update(route);
            await SaveAsync();
        }
    }
}
