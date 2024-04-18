using AutoMapper;
using Diplom.DTO.ScheduleDtos;
using Diplom.Entities;
using Diplom.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepo _repo;
        private readonly IMapper _mapper;
        public ScheduleController(IScheduleRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<ScheduleDto>> GetById(Guid id)
        {
            var sch = await _repo.GetById(id);
            if (sch == null)
            {
                return NotFound();
            }
            var rs = _mapper.Map<ScheduleDto>(sch);
            return Ok(rs);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetAll()
        {
            var sch = await _repo.GetAll();

            var rs = _mapper.Map<IEnumerable<ScheduleDto>>(sch);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ScheduleCreateUpdateDto dto)
        {
            var map = _mapper.Map<Schedule>(dto);
            await _repo.Create(map);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, ScheduleCreateUpdateDto dto)
        {
            var sch = await _repo.GetById(id);
            if(sch == null)
            {
                return BadRequest("Элемент не найден!!!");
            }
            var map = _mapper.Map(dto, sch);
            await _repo.Update(map);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var sch = await _repo.GetById(id);
            if(sch == null)
            {
                return BadRequest("Элемент не найден");
            }
            await _repo.Delete(sch);
            return NoContent();
        }
    }
}
