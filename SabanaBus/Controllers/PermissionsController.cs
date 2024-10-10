using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SabanaBus.DTOs;
using SabanaBus.Modelo;
using SabanaBus.Repository;
using SabanaBus.Services;
using SabanaBus.DTOs;
using System.Security;

namespace SabanaBus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {

        private readonly IPermissionsService service;

        private readonly ILogger<PermissionsController> logger;

        /// Constructor para la clase <see cref="PermissionsController"/>

        /// <param name="repository"></param>
        public PermissionsController(IPermissionsService service, ILogger<PermissionsController> logger)
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
            var data = await this.service.GetAllPermissionssAsync();

            this.logger.LogDebug("Consultando todos los permisos: {registros}", data);

            // Validamos que la data no sea nula o de otro tipo
            if (data == null || !data.Any())
            {
                return NotFound("No records found.");
            }

            // Mapeamos la lista de permisos a su DTO
            var result = data.Select(p => new PermissionsDto
            {
                IdPermission = p.IdPermission,
                Permission = p.Permission,
                IsDeleted = p.IsDeleted
            }).ToList();

            return Ok(result);

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
                return BadRequest("Invalid arguments");

            // Retornamos el resultado de la búsqueda
            var result = await service.GetPermissionsByIdAsync(id);

            if (result == null)
            {
                return NotFound("Record not found.");
            }

            // Mapeamos el permiso a su DTO
            var dto = new PermissionsDto
            {
                IdPermission = result.IdPermission,
                Permission = result.Permission,
                IsDeleted = result.IsDeleted
            };

            return Ok(dto);
        }

        /// <param name="data"></param>
        /// <returns>Retorna el Id de la compañia</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(PermissionsDto permissionsDto)
        {
            // Validamos que la data no sea nula
            if (permissionsDto == null)
                return BadRequest("Invalid arguments");

            // Creamos un nuevo objeto Permissions a partir del DTO
            var permissions = new Permissions
            {
                Permission = permissionsDto.Permission,
                IsDeleted = permissionsDto.IsDeleted
            };

            // Creamos
            var dataCreate = await this.service.CreatePermissionsAsync(permissions);

            // Verifica si la creación fue exitosa
            if (!dataCreate)
            {
                return BadRequest("Failed to create the record.");
            }

            this.logger.LogDebug("Creando registro - [Data: {data}]", permissionsDto);
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
            public async Task<IActionResult> Update(int id, PermissionsDto permissionsDto)
            {
                if (id < 0)
                    return BadRequest("Invalid ID");

                if (permissionsDto == null)
                    return BadRequest("Invalid data");

                // Creamos un nuevo objeto Permissions a partir del DTO
                var permissions = new Permissions
                {
                    IdPermission = id, // Asegúrate de que IdPermission esté definido en el DTO si es necesario
                    Permission = permissionsDto.Permission,
                    IsDeleted = permissionsDto.IsDeleted
                };

                var success = await service.UpdatePermissionsAsync(id, permissions);

                // Registra la información de la actualización
                this.logger.LogDebug("Actualizando registro - [Id: {Id}, Success: {Success}, Permissions: {Permissions}]", id, success, permissions);

                // Devuelve el resultado de la actualización
                if (!success)
                {
                    return NotFound("Record not found or could not be updated");
                }

                return Ok(success);
            }
        }
    }



