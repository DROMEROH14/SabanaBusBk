using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SabanaBus.DTOs;
using SabanaBus.Modelo;
using SabanaBus.Repositories;
using SabanaBus.Repository;
using SabanaBus.Services;

namespace SabanaBus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsXUserTypesController : ControllerBase
    {

        private readonly IPermissionsXUserTypesService service;

        private readonly ILogger<PermissionsXUserTypesController> logger;

        /// Constructor para la clase <see cref="PermissionsXUserTypesController"/>

        /// <param name="repository"></param>
        public PermissionsXUserTypesController(IPermissionsXUserTypesService service, ILogger<PermissionsXUserTypesController> logger)
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
            var data = await this.service.GetAllPermissionsXUserTypessAsync();

            this.logger.LogDebug("Consultando todos los {registros}", data);

            // Validamos que la data no se null o de otro tipo
            if (data == null || !data.Any())
            {
                return NotFound("No records found.");
            }

            // Convertimos el resultado a DTOs
            var dtoList = data.Select(p => new PermissionsXUserTypesDto
            {
                IdPermissionsXUserTypes = p.IdPermissionsXUserTypes,
                FKIdPermission = p.FKIdPermission,
                FKIdUserType = p.FKIdUserType,
                IsDeleted = p.IsDeleted
            });

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
                return NotFound("Arguments invalids");

            // Retornamos el resultado de la búsqueda
            var result = await service.GetPermissionsXUserTypesByIdAsync(id);
            if (result == null)
            {
                return NotFound("Record not found.");
            }

            // Convertimos a DTO
            var dto = new PermissionsXUserTypesDto
            {
                IdPermissionsXUserTypes = result.IdPermissionsXUserTypes,
                FKIdPermission = result.FKIdPermission,
                FKIdUserType = result.FKIdUserType,
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
        public async Task<IActionResult> Create(PermissionsXUserTypesDto permissionsXUserTypesDto)
        {
            // Validamos que la data no sea nula
            if (permissionsXUserTypesDto == null)
                return NotFound("Arguments invalids");

            // Convertimos DTO a modelo
            var permissionsXUserTypes = new PermissionsXUserTypes
            {
                FKIdPermission = permissionsXUserTypesDto.FKIdPermission,
                FKIdUserType = permissionsXUserTypesDto.FKIdUserType,
                IsDeleted = permissionsXUserTypesDto.IsDeleted
            };

            // Creamos
            var dataCreate = await this.service.CreatePermissionsXUserTypesAsync(permissionsXUserTypes);

            // Verifica si la creación fue exitosa
            if (!dataCreate)
            {
                return NotFound("Failed to create the record");
            }

            this.logger.LogDebug("Creando registro - [Data: {data}]", permissionsXUserTypes);
            return Ok(dataCreate);
        }
        /// <summary>
        /// Servicio encargado de la actualización de un registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Route"></param>
        /// <returns>Retorna true si fue exitoso de lo contrario un false</returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, PermissionsXUserTypesDto permissionsXUserTypesDto)
        {
            if (id < 0)
                return NotFound("id is not valid");
            if (permissionsXUserTypesDto == null)
                return NotFound("Data is not valid");

            // Convertimos DTO a modelo
            var permissionsXUserTypes = new PermissionsXUserTypes
            {
                FKIdPermission = permissionsXUserTypesDto.FKIdPermission,
                FKIdUserType = permissionsXUserTypesDto.FKIdUserType,
                IsDeleted = permissionsXUserTypesDto.IsDeleted
            };

            var success = await service.UpdatePermissionsXUserTypesAsync(id, permissionsXUserTypes);

            // Registra la información de la actualización
            this.logger.LogDebug("Actualizando registro - [Id: {Id}, Success: {Success}, PermissionsXUserTypes: {PermissionsXUserTypes}]", id, success, permissionsXUserTypes);

            // Devuelve el resultado de la actualización
            if (!success)
            {
                return NotFound("Record not found or could not be updated");
            }

            return Ok(success);
        }
    }
}



