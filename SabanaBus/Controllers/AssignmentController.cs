using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SabanaBus.Modelo;
using SabanaBus.Repositories;
using SabanaBus.Repository;
using SabanaBus.Services;
using SabanaBus.DTOs;

namespace SabanaBus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {

        private readonly IAssignmentService service;

        private readonly ILogger<AssignmentController> logger;

        /// Constructor para la clase <see cref="AssignmentController"/>

        /// <param name="repository"></param>
        public AssignmentController(IAssignmentService service, ILogger<AssignmentController> logger)
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
            var data = await this.service.GetAllAssignmentAsync();

            this.logger.LogDebug("Consultando todos los {registros}", data);

            // Validamos que la data no sea nula o vacía
            if (data == null || !data.Any())
            {
                return NotFound("No records found.");
            }

            // Mapeamos la lista de Assignment a AssignmentDto
            var dtoList = data.Select(assignment => new AssignmentDto
            {
                AssignmentID = assignment.AssignmentID,
                FkBusID = assignment.FkBusID,
                FkRouteID = assignment.FkRouteID,
                AssignmentDate = assignment.AssignmentDate,
                IsDeleted = assignment.IsDeleted
            }).ToList();

            return Ok(dtoList);

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
            {
                return NotFound("Arguments invalid");
            }

            // Retornamos el resultado de la búsqueda
            var result = await service.GetAssignmentByIdAsync(id);

            // Verificamos si el resultado es nulo
            if (result == null)
            {
                return NotFound("Record not found.");
            }

            // Mapeamos la entidad Assignment a AssignmentDto
            var assignmentDto = new AssignmentDto
            {
                AssignmentID = result.AssignmentID,
                FkBusID = result.FkBusID,
                FkRouteID = result.FkRouteID,
                AssignmentDate = result.AssignmentDate,
                IsDeleted = result.IsDeleted
            };

            return Ok(assignmentDto);
        }

        /// <param name="data"></param>
        /// <returns>Retorna el Id de la compañia</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AssignmentDto assignmentDto)
        {
            // Validamos que la data no sea nula
            if (assignmentDto == null)
            {
                return NotFound("Arguments invalid");
            }

            // Mapeamos el DTO a la entidad Assignment
            var assignment = new Assignment
            {
                FkBusID = assignmentDto.FkBusID,
                FkRouteID = assignmentDto.FkRouteID,
                AssignmentDate = assignmentDto.AssignmentDate,
                IsDeleted = assignmentDto.IsDeleted
            };

            // Creamos
            var dataCreate = await this.service.CreateAssignmentAsync(assignment);

            // Verificamos si la creación fue exitosa
            if (!dataCreate)
            {
                return NotFound("Failed to create the record");
            }

            this.logger.LogDebug("Creando registro - [Data: {data}]", assignment);
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
        public async Task<IActionResult> Update(int id, [FromBody] AssignmentDto assignmentDto)
        {
            // Validamos el ID
            if (id < 0)
            {
                return NotFound("ID is not valid");
            }

            // Validamos que el DTO no sea nulo
            if (assignmentDto == null)
            {
                return BadRequest("Data is not valid");
            }

            // Mapeamos el DTO a la entidad Assignment
            var assignment = new Assignment
            {
                FkBusID = assignmentDto.FkBusID,
                FkRouteID = assignmentDto.FkRouteID,
                AssignmentDate = assignmentDto.AssignmentDate,
                IsDeleted = assignmentDto.IsDeleted
            };

            // Actualizamos el registro en el servicio
            var success = await service.UpdateAssignmentAsync(id, assignment);

            // Registra la información de la actualización
            this.logger.LogDebug("Actualizando registro - [Id: {Id}, Success: {Success}, Assignment: {Assignment}]", id, success, assignment);

            // Verificamos si la actualización fue exitosa
            if (!success)
            {
                return NotFound("Record not found or could not be updated");
            }

            // Mapeamos la entidad actualizada a un DTO para devolver solo los campos que necesitamos
            var updatedAssignmentDto = new AssignmentDto
            {
                AssignmentID = id,
                FkBusID = assignment.FkBusID,
                FkRouteID = assignment.FkRouteID,
                AssignmentDate = assignment.AssignmentDate,
                IsDeleted = assignment.IsDeleted
            };

            // Devolver solo los campos del DTO, no las relaciones complejas
            return Ok(updatedAssignmentDto);
        }
    }
}