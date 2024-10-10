using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SabanaBus.DTOs;
using SabanaBus.Modelo;
using SabanaBus.Repository;
using SabanaBus.Services;

namespace SabanaBus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
   
        private readonly IScheduleService service;

        private readonly ILogger<ScheduleController> logger;

        /// Constructor para la clase <see cref="ScheduleController"/>

        /// <param name="repository"></param>
        public ScheduleController(IScheduleService service, ILogger<ScheduleController> logger)
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
            // Asignamos la información consultada a una variable
            var data = await this.service.GetAllSchedulesAsync();

            this.logger.LogDebug("Consultando todos los {registros}", data);

            // Validamos que la data no sea null o de otro tipo
            if (data == null || !data.Any())
            {
                return NotFound("No records found.");
            }

            // Mapeamos a DTOs (asumiendo que tienes un método para hacer esto)
            var dtoData = data.Select(schedule => new ScheduleDTO
            {
                IDSchedule = schedule.IDSchedule,
                FkIDRoute = schedule.FkIDRoute,
                DepartureTime = schedule.DepartureTime,
                ArrivalTime = schedule.ArrivalTime,
                Frequency = schedule.Frequency,
                Status = schedule.Status,
                IsDeleted = schedule.IsDeleted
            }).ToList();

            return Ok(dtoData);

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
            if (id < 0)
                return NotFound("Arguments invalids");

            // Retornamos el resultado de la búsqueda
            var result = await service.GetScheduleByIdAsync(id);

            if (result == null)
                return NotFound("Schedule not found");

            // Mapeamos a DTO
            var dtoResult = new ScheduleDTO
            {
                IDSchedule = result.IDSchedule,
                FkIDRoute = result.FkIDRoute,
                DepartureTime = result.DepartureTime,
                ArrivalTime = result.ArrivalTime,
                Frequency = result.Frequency,
                Status = result.Status,
                IsDeleted = result.IsDeleted
            };

            return Ok(dtoResult);
        }

        /// <param name="data"></param>
        /// <returns>Retorna el Id de la compañia</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(ScheduleDTO scheduleDto)
        {
            // Validamos que la data no sea nula
            if (scheduleDto == null)
                return NotFound("Arguments invalids");

            // Creamos el modelo a partir del DTO
            var schedule = new Schedule
            {
                FkIDRoute = scheduleDto.FkIDRoute,
                DepartureTime = scheduleDto.DepartureTime,
                ArrivalTime = scheduleDto.ArrivalTime,
                Frequency = scheduleDto.Frequency,
                Status = scheduleDto.Status,
                IsDeleted = scheduleDto.IsDeleted
            };

            // Creamos
            var dataCreate = await this.service.CreateScheduleAsync(schedule);

            // Verifica si la creación fue exitosa
            if (!dataCreate)
            {
                return NotFound("Failed to create the record");
            }

            this.logger.LogDebug("Creando registro - [Data: {data}]", schedule);
            return Ok(dataCreate);
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
        public async Task<IActionResult> Update(int id, ScheduleDTO scheduleDto)
        {
            if (id < 0)
                return NotFound("id is not valid");
            if (scheduleDto == null)
                return NotFound("Data is not valid");

            // Creamos el modelo a partir del DTO
            var schedule = new Schedule
            {
                FkIDRoute = scheduleDto.FkIDRoute,
                DepartureTime = scheduleDto.DepartureTime,
                ArrivalTime = scheduleDto.ArrivalTime,
                Frequency = scheduleDto.Frequency,
                Status = scheduleDto.Status,
                IsDeleted = scheduleDto.IsDeleted
            };

            var success = await service.UpdateScheduleAsync(id, schedule);

            // Registra la información de la actualización
            this.logger.LogDebug("Actualizando registro - [Id: {Id}, Success: {Success}, Data: {Data}]", id, success, schedule);

            // Devuelve el resultado de la actualización
            if (!success)
            {
                return NotFound("Record not found or could not be updated");
            }

            return Ok(success);
        }
    }
}



