using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SabanaBus.DTOs;
using SabanaBus.Modelo;
using SabanaBus.Services;

namespace SabanaBus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {

        private readonly INotificationService service;

        private readonly ILogger<NotificationController> logger;

        /// Constructor para la clase <see cref="NotificationController"/>

        /// <param name="service"></param>
        public NotificationController(INotificationService service, ILogger<NotificationController> logger)
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
            var data = await this.service.GetAllNotificationAsync();

            this.logger.LogDebug("Consultando todos los registros: {registros}", data);

            // Validamos que la data no sea nula o de otro tipo
            if (data == null || !data.Any())
            {
                return NotFound("No records found.");
            }

            // Convertir a DTO antes de devolver
            var notificationDtos = data.Select(n => new NotificationDto
            {
                IDNotification = n.IDNotification,
                FkIdSchedule = n.FkIdSchedule,
                Message = n.Message,
                DateTime = n.DateTime,
                IsDeleted = n.IsDeleted
            }).ToList();

            return Ok(notificationDtos);

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
            var result = await service.GetNotificationByIdAsync(id);
            if (result == null)
            {
                return NotFound("Notification not found.");
            }

            // Convertir a DTO antes de devolver
            var notificationDto = new NotificationDto
            {
                IDNotification = result.IDNotification,
                FkIdSchedule = result.FkIdSchedule,
                Message = result.Message,
                DateTime = result.DateTime,
                IsDeleted = result.IsDeleted
            };

            return Ok(notificationDto);
        }

        /// <param name="data"></param>
        /// <returns>Retorna el Id de la compañia</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] NotificationDto notificationDto)
        {
            // Validamos que la data no sea nula
            if (notificationDto == null)
                return NotFound("Arguments invalids");

            // Mapeamos el DTO a la entidad Notification
            var notification = new Notification
            {
                FkIdSchedule = notificationDto.FkIdSchedule,
                Message = notificationDto.Message,
                DateTime = notificationDto.DateTime,
                IsDeleted = notificationDto.IsDeleted
            };

            // Creamos
            var dataCreate = await this.service.CreateNotificationAsync(notification);

            // Verifica si la creación fue exitosa
            if (!dataCreate)
            {
                return NotFound("Failed to create the record");
            }

            this.logger.LogDebug("Creando registro - [Data: {data}]", notification);
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
        public async Task<IActionResult> Update(int id, [FromBody] NotificationDto notificationDto)
        {
            if (id < 0)
                return NotFound("ID is not valid");
            if (notificationDto == null)
                return NotFound("Data is not valid");

            // Mapeamos el DTO a la entidad Notification
            var notification = new Notification
            {
                FkIdSchedule = notificationDto.FkIdSchedule,
                Message = notificationDto.Message,
                DateTime = notificationDto.DateTime,
                IsDeleted = notificationDto.IsDeleted
            };

            var success = await service.UpdateNotificationAsync(id, notification);

            // Registra la información de la actualización
            this.logger.LogDebug("Actualizando registro - [Id: {Id}, Success: {Success}, Notification: {Notification}]", id, success, notification);

            // Verificamos si la actualización fue exitosa
            if (!success)
            {
                return NotFound("Record not found or could not be updated");
            }

            return Ok(success);
        }
    }
}



