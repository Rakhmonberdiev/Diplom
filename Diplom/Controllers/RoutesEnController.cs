using AutoMapper;
using Diplom.DTO.RouteEnDtos;
using Diplom.Entities;
using Diplom.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesEnController : ControllerBase
    {
        private readonly IRouteEnRepo _repo;
        private readonly IMapper _mapper;

        public RoutesEnController(IRouteEnRepo routeEnRepo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = routeEnRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteEnDto>>> GetAllRoutes()
        {
            var routes = await _repo.GetAllRoutes();
            var routesToReturn = _mapper.Map<IEnumerable<RouteEnDto>>(routes);
            return Ok(routesToReturn);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RouteEnDto>> GetRouteId(Guid id)
        {
            var route = await _repo.GetRouteById(id);
            var routeToReturn = _mapper.Map<RouteEnDto>(route);
            return Ok(routeToReturn);
        }


        [HttpPost]
        public async Task<IActionResult> Create(RouteCreateDto dto)
        {
            try
            {
                bool routeExists = await _repo.IsRouteExist(dto.StartPointId, dto.EndPointId);
                if (routeExists)
                {
                    return BadRequest("Маршрут с такими начальным и конечным пунктами уже существует");
                }
                var map = _mapper.Map<RouteEn>(dto);
                await _repo.Create(map);
                return Ok("Маршрут успешно добавлен");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка при добавлении маршрута: {ex.Message}");
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(RouteUpdateDto dto)
        {
            try
            {
                
                var routeExists = await _repo.GetRouteById(dto.Id);
                if (routeExists == null)
                {
                    return NotFound();
                }
                var upd = _mapper.Map(dto, routeExists);
                await _repo.Update(upd);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка при редактирование маршрута: {ex.Message}");
            }
        }
    }
}
