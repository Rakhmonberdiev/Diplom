using Diplom.DTO.TicketDtos;
using Diplom.Entities;
using Diplom.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepo _repo;
        public TicketController(ITicketRepo repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateTicket(TicketCreateDto dto)
        {
            string urlToReturn =  await _repo.Create(dto);
            return Ok(urlToReturn);
        }
        [HttpGet]
        public async Task<ActionResult<Ticket>> GetById(Guid id)
        {
            var model = await _repo.GetById(id);
            return Ok(model);
        }
    }
}
