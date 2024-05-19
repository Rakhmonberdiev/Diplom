using AutoMapper;
using AutoMapper.QueryableExtensions;
using Diplom.Data;
using Diplom.DTO.DistrictDtos;
using Diplom.Entities;
using Diplom.Helpers;
using Diplom.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Repositories.Implementation
{
    public class DistrictRepo : IDistrictRepo
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public DistrictRepo(AppDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
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

        public async Task<PagedList<DistrictDto>> GetAll(PaginationParams pageParams, string search)
        {
            if(!string.IsNullOrEmpty(search))
            {
                var stringQuery = _context.Districts.Where(x => x.Title.ToLower().Contains(search.ToLower()))
                    .ProjectTo<DistrictDto>(_mapper.ConfigurationProvider).AsNoTracking().OrderBy(x => x.Id);
                return await PagedList<DistrictDto>.CreateAsync(stringQuery, pageParams.PageNumber, pageParams.PageSize);
            }
            var query = _context.Districts.ProjectTo<DistrictDto>(_mapper.ConfigurationProvider).AsNoTracking().OrderBy(x=>x.Id);

            return await PagedList<DistrictDto>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
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
