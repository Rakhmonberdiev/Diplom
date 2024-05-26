using AutoMapper;
using Diplom.DTO.RouteEnDtos;
using Diplom.Entities;
using Diplom.Extensions;
using Diplom.Helpers;
using Diplom.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    [Route("api/routes")]
    [ApiController]
    public class RoutesEnController : ControllerBase
    {
        // Приватное поле для хранения экземпляра репозитория IRouteEnRepo
        private readonly IRouteEnRepo _repo;
        // Приватное поле для хранения экземпляра маппера IMapper
        private readonly IMapper _mapper;


        // Конструктор класса RoutesEnController
        public RoutesEnController(IRouteEnRepo routeEnRepo, IMapper mapper)
        {
            // Инициализация поля _mapper с переданным экземпляром маппера
            _mapper = mapper;

            // Инициализация поля _repo с переданным экземпляром репозитория routeEnRepo
            _repo = routeEnRepo;
        }

        [HttpGet("GetRouteByDistrictId")]
        public async Task<ActionResult<RouteEnDto>> GetRouteByDistrictId(Guid fromId,Guid toId)
        {
            var rout = await _repo.GetByDistrictId(fromId, toId);
            var routtoReturn = _mapper.Map<RouteEnDto>(rout);
            return Ok(routtoReturn);
        }


        [Authorize(Policy = "AdminRole")]
        // Обработчик HTTP GET запроса для получения всех маршрутов
        [HttpGet("GetAll")]
        public async Task<ActionResult<PagedList<RouteEnDto>>> GetAllRoutes([FromQuery] PaginationParams pageParams, string search)
        {
            // Получение всех маршрутов из репозитория
            var routes = await _repo.GetAllRoutes(pageParams,search);

            Response.AddPaginationHeader(new PaginationHeader(routes.CurrentPage, routes.PageSize,
                routes.TotalCount, routes.TotalPages));

            // Возвращение результата выполнения запроса с кодом 200 (OK) и коллекцией маршрутов
            return Ok(routes);
        }


        [HttpGet("GetAllForHome")]
        public async Task<ActionResult<IEnumerable<RouteEnDto>>> GetAllRoutesForHome()
        {
          
            var routes = await _repo.GetLast8Routes();

            var routesToReturn = _mapper.Map<IEnumerable<RouteEnDto>>(routes);
            return Ok(routesToReturn);
        }

        [Authorize(Policy = "AdminRole")]
        // Обработчик HTTP GET запроса для получения маршрута по идентификатору
        [HttpGet("{id}")]
        public async Task<ActionResult<RouteEnDto>> GetRouteId(Guid id)
        {
            // Получение маршрута из репозитория по указанному идентификатору
            var route = await _repo.GetRouteById(id);

            // Преобразование полученного маршрута в объект типа RouteEnDto
            var routeToReturn = _mapper.Map<RouteEnDto>(route);

            // Возвращение результата выполнения запроса с кодом 200 (OK) и маршрутом
            return Ok(routeToReturn);
        }


        [Authorize(Policy = "AdminRole")]
        // Обработчик HTTP POST запроса для создания нового маршрута
        [HttpPost]
        public async Task<IActionResult> Create(RouteCreateDto dto)
        {
            try
            {
                // Проверка, существует ли уже маршрут с указанными начальным и конечным пунктами
                bool routeExists = await _repo.IsRouteExist(dto.StartPointId, dto.EndPointId);


                if (routeExists)
                {
                    // Если маршрут уже существует, возвращается код 400 (BadRequest) с сообщением об ошибке
                    return BadRequest("Маршрут с такими начальным и конечным пунктами уже существует");
                }

                // Преобразование объекта типа RouteCreateDto в объект типа RouteEn с помощью маппера
                var map = _mapper.Map<RouteEn>(dto);

                // Вызов метода Create репозитория для создания нового маршрута
                await _repo.Create(map);

                // Возвращение кода 200 (OK) с сообщением об успешном добавлении маршрута
                return Ok();
            }
            catch (Exception ex)
            {
                // Если произошла ошибка, возвращается код 500 (Internal Server Error) с сообщением об ошибке
                return StatusCode(500, $"Произошла ошибка при добавлении маршрута: {ex.Message}");
            }
        }

        // Обработчик HTTP PUT запроса для обновления маршрута
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,RouteUpdateDto dto)
        {
            try
            {
                // Получение маршрута по указанному идентификатору
                var routeExists = await _repo.GetRouteById(id);
                if (routeExists == null)
                {
                    // Если маршрут не найден, возвращается код 404 (Not Found)
                    return NotFound();
                }
                // Применение изменений из объекта dto на объект routeExists с помощью маппера
                var upd = _mapper.Map(dto, routeExists);

                // Вызов метода Update репозитория для обновления маршрута
                await _repo.Update(upd);
                // Возвращение кода 204 (No Content) для указания успешного выполнения операции
                return NoContent();

            }
            catch (Exception ex)
            {
                // Если произошла ошибка при выполнении операции, возвращается код 500 (Internal Server Error) с сообщением об ошибке
                return StatusCode(500, $"Произошла ошибка при редактирование маршрута: {ex.Message}");
            }
        }
        [Authorize(Policy = "AdminRole")]
        // Обработчик HTTP DELETE запроса для удаления маршрута
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Обработчик HTTP DELETE запроса для удаления маршрута
            var routeExists = await _repo.GetRouteById(id);

            // Проверка, существует ли маршрут с указанным идентификатором
            if (routeExists == null)
            {
                // Если маршрут не найден, возвращается код 404 (Not Found)
                return NotFound();
            }
            // Вызов метода Delete репозитория для удаления маршрута
            await _repo.Delete(routeExists);

            // Возвращение кода 204 (No Content) для указания успешного выполнения операции
            return NoContent();
        }
    }
}
