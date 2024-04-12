using Diplom.Data;
using Diplom.Entities;
using Diplom.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Repositories.Implementation
{
    public class DistrictRepo : IDistrictRepo
    {
        private readonly AppDbContext _context;
        public DistrictRepo(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task Create(DistrictsEn district)
        {
            await _context.Districts.AddAsync(district);
            await SaveAsync();
        }

        public async Task Delete(DistrictsEn district)
        {
            _context.Districts.Remove(district);
            await SaveAsync();
        }

        public async Task<IEnumerable<DistrictsEn>> GetAll(string search)
        {
            if(!string.IsNullOrEmpty(search))
            {
                return await _context.Districts.Where(x=>x.Title.Contains(search)).ToListAsync();
            }
            return await _context.Districts.ToListAsync();
        }

        public async Task<DistrictsEn> GetById(Guid id)
        {
            return await _context.Districts.FirstOrDefaultAsync(d=>d.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(DistrictsEn district)
        {
            _context.Districts.Update(district);
            await SaveAsync();
        }
    }
}
