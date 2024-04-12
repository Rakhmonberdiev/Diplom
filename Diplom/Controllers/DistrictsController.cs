using AutoMapper;
using Diplom.DTO.DistrictDtos;
using Diplom.Entities;
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

            return Ok(districtDto);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistrictDto>>> GetAll(string search)
        {
            var districts = await _districtRepo.GetAll(search);
            var districtDtos = _mapper.Map<IEnumerable<DistrictDto>>(districts);
            return Ok(districtDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DistrictCreateUpdateDto createUpdateDto)
        {
            var district = _mapper.Map<DistrictsEn>(createUpdateDto);
            await _districtRepo.Create(district);
            return CreatedAtAction(nameof(GetById), new { id = district.Id }, district);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, DistrictCreateUpdateDto createUpdateDto)
        {
            var existingDistrict = await _districtRepo.GetById(id);
            if (existingDistrict == null)
            {
                return NotFound();
            }
            var updatedDistrict = _mapper.Map(createUpdateDto,existingDistrict);
            await _districtRepo.Update(updatedDistrict);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingDistrict = await _districtRepo.GetById(id);
            if (existingDistrict == null)
            {
                return NotFound();
            }

            await _districtRepo.Delete(existingDistrict);
            return NoContent();
        }
    }
}
