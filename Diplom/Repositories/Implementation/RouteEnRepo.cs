﻿using Diplom.Data;
using Diplom.Entities;
using Diplom.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Repositories.Implementation
{
    public class RouteEnRepo : IRouteEnRepo
    {
        private readonly AppDbContext _db;
        public RouteEnRepo(AppDbContext db)
        {
            _db = db;
        }

        public async Task Create(RouteEn route)
        {
            await _db.Routes.AddAsync(route);
            await SaveAsync();
        }

        public async Task<IEnumerable<RouteEn>> GetAllRoutes()
        {
            return await _db.Routes
                .Include(x=>x.StartPoint)
                .Include(x=>x.EndPoint)
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
    }
}
