using Diplom.Data;
using Diplom.Entities;
using Diplom.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Repositories.Implementation
{
    public class ScheduleRepo : IScheduleRepo
    {
        private readonly AppDbContext _db;
        public ScheduleRepo(AppDbContext db)
        {
            _db = db;
        }
        public async Task Create(Schedule schedule)
        {
            await _db.Schedules.AddAsync(schedule);
            await SaveAsync();

        }

        public async Task Delete(Schedule schedule)
        {
            _db.Remove(schedule);
            await SaveAsync();
        }

        public async Task<IEnumerable<Schedule>> GetAll()
        {
            return await _db.Schedules.OrderBy(s=>s.Title).ToListAsync();
        }

        public async Task<Schedule> GetById(Guid id)
        {
            return await _db.Schedules.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task Update(Schedule schedule)
        {
            _db.Schedules.Update(schedule);
            await SaveAsync();
        }
    }
}
