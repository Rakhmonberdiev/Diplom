using AutoMapper;
using Diplom.DTO.DistrictDtos;
using Diplom.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        private readonly IDistrictRepo _districtRepo;
        private readonly IMapper _mapper;

        public DistrictsController(IDistrictRepo districtRepo, IMapper mapper)
        {
            _districtRepo = districtRepo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DistrictDto>> GetById(Guid id)
        {
            var district = await _districtRepo.GetById(id);

            if (district == null)
            {
                return NotFound();
            }

            var districtDto = _mapper.Map<DistrictDto>(district);

            return districtDto;
        }

    }
}
