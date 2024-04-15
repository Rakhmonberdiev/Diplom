using AutoMapper;
using Diplom.DTO.DistrictDtos;
using Diplom.Entities;
using Diplom.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    [Route("api/rayon")]
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



        // Этот метод является обработчиком HTTP GET запроса по маршруту с параметром "id"
        [HttpGet("{id}")]
        public async Task<ActionResult<DistrictDto>> GetById(Guid id)
        {
            // Получаем район из репозитория по указанному идентификатору
            var district = await _districtRepo.GetById(id);
            // Если район не найден, возвращаем код ответа 404 "Not Found"
            if (district == null)
            {
                return NotFound();
            }
            // Преобразуем район в объект DistrictDto с помощью AutoMapper
            var districtDto = _mapper.Map<DistrictDto>(district);
            // Возвращаем код ответа 200 "OK" и объект DistrictDto
            return Ok(districtDto);
        }


        // Этот метод является обработчиком HTTP GET запроса по маршруту "GetAll" с параметром "search"
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<DistrictDto>>> GetAll(string search)
        {
            // Получаем все районы из репозитория с использованием параметра "search"
            var districts = await _districtRepo.GetAll(search);

            // Преобразуем список районов в список объектов DistrictDto с помощью AutoMapper
            var districtDtos = _mapper.Map<IEnumerable<DistrictDto>>(districts);
            // Возвращаем код ответа 200 "OK" и список объектов DistrictDto
            return Ok(districtDtos);
        }

        // Обработчик HTTP POST запроса для создания нового объекта
        [HttpPost]
        public async Task<IActionResult> Create(DistrictCreateUpdateDto createUpdateDto)
        {
            // Преобразование объекта createUpdateDto в объект типа DistrictsEn
            var district = _mapper.Map<DistrictsEn>(createUpdateDto);
            // Вызов метода Create репозитория для сохранения объекта district
            await _districtRepo.Create(district);
            // Возвращение результата действия CreatedAtAction
            // с указанием URL действия GetById для получения информации о только что созданном район
            return CreatedAtAction(nameof(GetById), new { id = district.Id }, district);
        }

        // Обработчик HTTP PUT запроса для обновления существующего объекта по идентификатору
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, DistrictCreateUpdateDto createUpdateDto)
        {   
            
            // Получение существующего объекта района по идентификатору
            var existingDistrict = await _districtRepo.GetById(id);

            // Проверка, существует ли объект района
            if (existingDistrict == null)
            {
                // Если объект не найден, возвращается код 404 (Not Found)
                return NotFound();
            }
            // Обновление существующего объекта района с помощью данных из createUpdateDto
            var updatedDistrict = _mapper.Map(createUpdateDto,existingDistrict);
            // Вызов метода Update репозитория для сохранения обновленного объекта района
            await _districtRepo.Update(updatedDistrict);
            // Возвращение кода 204 (No Content) для указания успешного выполнения без возвращаемых данных
            return NoContent();
        }

        // Обработчик HTTP DELETE запроса для удаления существующего объекта по идентификатору
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Получение существующего объекта района по идентификатору
            var existingDistrict = await _districtRepo.GetById(id);
            // Проверка, существует ли объект района
            if (existingDistrict == null)
            {
                // Если объект не найден, возвращается код 404 (Not Found)
                return NotFound();
            }
            // Вызов метода Delete репозитория для удаления объекта района
            await _districtRepo.Delete(existingDistrict);

            // Возвращение кода 204 (No Content) для указания успешного выполнения без возвращаемых данных
            return NoContent();
        }
    }
}
