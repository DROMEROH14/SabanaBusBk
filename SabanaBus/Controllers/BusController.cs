using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SabanaBus.DTOs;
using SabanaBus.Modelo;
using SabanaBus.Services;

namespace SabanaBus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusController : ControllerBase
    {

        private readonly IBusService service;

        private readonly ILogger<BusController> logger;

        /// Constructor para la clase <see cref="BusController"/>

        /// <param name="service"></param>
        public BusController(IBusService service, ILogger<BusController> logger)
        {
            this.service = service;
            this.logger = logger;
        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> all()
        {
            // Obtenemos todos los registros de Bus
            var data = await this.service.GetAllBusAsync();

            this.logger.LogDebug("Consultando todos los registros de bus: {data}", data);

            // Validamos que la data no sea nula o vacía
            if (data == null || !data.Any())
            {
                return NotFound("No records found.");
            }

            // Mapeamos la data a una lista de BusDto
            var busDtoList = data.Select(bus => new BusDto
            {
                IdBus = bus.IdBus,
                LicensePlate = bus.LicensePlate,
                Capacity = bus.Capacity,
                Status = bus.Status,
                IsDeleted = bus.IsDeleted
            }).ToList();

            return Ok(busDtoList);

        }
        /// <summary>
        /// Servicio encargado de consultar un registro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Find(int id)
        {
            // Validamos que el id no sea menor a 0
            if (id <= 0)
                return BadRequest("ID is not valid.");

            // Retornamos el resultado de la búsqueda
            var result = await service.GetBusByIdAsync(id);

            if (result == null)
            {
                return NotFound("Bus not found.");
            }

            // Mapeamos el resultado a BusDto
            var busDto = new BusDto
            {
                IdBus = result.IdBus,
                LicensePlate = result.LicensePlate,
                Capacity = result.Capacity,
                Status = result.Status,
                IsDeleted = result.IsDeleted
            };

            return Ok(busDto);
        }

        /// <param name="data"></param>
        /// <returns>Retorna el Id de la compañia</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] BusDto busDto)
        {
            // Validamos que la data no sea nula
            if (busDto == null)
                return BadRequest("Invalid data.");

            // Mapeamos el BusDto a la entidad Bus
            var bus = new Bus
            {
                LicensePlate = busDto.LicensePlate,
                Capacity = busDto.Capacity,
                Status = busDto.Status,
                IsDeleted = busDto.IsDeleted
            };

            // Creamos el bus
            var success = await this.service.CreateBusAsync(bus);

            // Verifica si la creación fue exitosa
            if (!success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create the record.");
            }

            this.logger.LogDebug("Creando registro - [Data: {data}]", bus);
            return Ok("Bus created successfully.");
        }
        /// <summary>
        /// Servicio encargado de la actualización de un registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns>Retorna true si fue exitoso de lo contrario un false</returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] BusDto busDto)
        {
            // Validamos el ID
            if (id <= 0)
                return BadRequest("ID is not valid.");

            // Validamos que la data no sea nula
            if (busDto == null)
                return BadRequest("Invalid data.");

            // Mapeamos el BusDto a la entidad Bus
            var bus = new Bus
            {
                LicensePlate = busDto.LicensePlate,
                Capacity = busDto.Capacity,
                Status = busDto.Status,
                IsDeleted = busDto.IsDeleted
            };

            var success = await service.UpdateBusAsync(id, bus);

            // Registra la información de la actualización
            this.logger.LogDebug("Actualizando registro - [Id: {Id}, Success: {Success}, Bus: {Bus}]", id, success, bus);

            // Devuelve el resultado de la actualización
            if (!success)
            {
                return NotFound("Record not found or could not be updated.");
            }

            return Ok("Bus updated successfully.");
        }
    }
}



