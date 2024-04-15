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
    }
}
