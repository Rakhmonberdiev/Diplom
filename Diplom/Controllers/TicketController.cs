using AutoMapper;
using Diplom.DTO.TicketDtos;
using Diplom.Entities;
using Diplom.Extensions;
using Diplom.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepo _repo;
        private readonly IMapper _mapper;
        public TicketController(ITicketRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Ticket>> CreateTicket(TicketCreateDto dto)
        {
            var userId = User.GetUserId();
            dto.UserId = userId;
            var model = await _repo.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = model.Id },model);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetById(Guid id)
        {
            var userId = User.GetUserId();
            var model = await _repo.GetById(id,userId);
            var mappingToReturn = _mapper.Map<TicketDto>(model);
            return Ok(mappingToReturn);
        }

        [Authorize]
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetAll()
        {
            var userId = User.GetUserId();
            var models = await _repo.GetAll(userId);
            var mappingToRet = _mapper.Map<IEnumerable<TicketDto>>(models);
            return Ok(mappingToRet);
        }


        [Authorize(Policy = "AdminRole")]
        [HttpGet("admin")]
        public async Task<ActionResult<IEnumerable<TicketAdmin>>> GetAllAdmin(string search)
        {
            var models = await _repo.GetAllAdmin(search);
            var mapping = _mapper.Map<IEnumerable<TicketAdmin>>(models);
            return Ok(mapping);
        }

        [Authorize(Policy = "AdminRole")]
        // Обработчик HTTP DELETE запроса для удаления билета
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Обработчик HTTP DELETE запроса для удаления билета
            var ticketExists = await _repo.GetForAdmin(id);

            // Проверка, существует ли билет с указанным идентификатором
            if (ticketExists == null)
            {
                // Если билет не найден, возвращается код 404 (Not Found)
                return NotFound();
            }
            // Вызов метода Delete репозитория для удаления билета
            await _repo.Delete(ticketExists);

            // Возвращение кода 204 (No Content) для указания успешного выполнения операции
            return NoContent();
        }
    }
}
