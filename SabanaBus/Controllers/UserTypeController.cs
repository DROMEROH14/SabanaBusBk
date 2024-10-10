using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SabanaBus.DTOs;
using SabanaBus.Modelo;
using SabanaBus.Repositories;
using SabanaBus.Services;


namespace SabanaBus.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
  
        private readonly IUserTypeService service;
   
        private readonly ILogger<UserTypeController> logger;

        /// Constructor para la clase <see cref="AreaController"/>
      
        /// <param name="repository"></param>
        public UserTypeController(IUserTypeService service, ILogger<UserTypeController> logger)
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
            var data = await this.service.GetAllUserTypesAsync();

            this.logger.LogDebug("Consultando todos los {registros}", data);

            // Validamos que la data no sea null o de otro tipo
            if (data == null || !data.Any())
            {
                return NotFound("No records found.");
            }

            // Mapear el resultado a DTO
            var result = data.Select(ut => new UserTypeDTO
            {
                IdUserType = ut.IdUserType,
                UserTypeName = ut.UserTypeName,
              
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
                return NotFound("Arguments invalids");

            // Retornamos el resultado de la búsqueda
            var userType = await service.GetUserTypeByIdAsync(id);
            if (userType == null)
                return NotFound("UserType not found.");

            // Mapear el resultado a DTO
            var result = new UserTypeDTO
            {
                IdUserType = userType.IdUserType,
                UserTypeName = userType.UserTypeName,

            };

            return Ok(result);
        }

        /// <param name="data"></param>
        /// <returns>Retorna el Id de la compañia</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(UserTypeDTO data)
        {
            // Validamos que la data no sea nula
            if (data == null)
                return NotFound("Arguments invalids");

            // Creamos el modelo de dominio a partir del DTO
            var userType = new UserType
            {
                UserTypeName = data.UserTypeName,
                // Aquí puedes agregar lógica para manejar UserIds y PermissionIds
            };

            // Creamos
            var dataCreate = await this.service.CreateUserTypeAsync(userType);

            // Verifica si la creación fue exitosa
            if (!dataCreate)
            {
                return NotFound("Failed to create the record");
            }

            this.logger.LogDebug("Creando registro - [Data: {data}]", data);
            return Ok(dataCreate);
        }
        /// <summary>
        /// Servicio encargado de la actualización de un registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns>Retorna true si fue exitoso de lo contrario un false</returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, UserTypeDTO data)
        {
            if (id < 0)
                return NotFound("id is not valid");
            if (data == null)
                return NotFound("Data is not valid");

            // Obtener el modelo existente
            var userType = await service.GetUserTypeByIdAsync(id);
            if (userType == null)
                return NotFound("UserType not found.");

            // Actualizar las propiedades del modelo
            userType.UserTypeName = data.UserTypeName;
            // Aquí puedes agregar lógica para manejar UserIds y PermissionIds

            var success = await service.UpdateUserTypeAsync(id, userType);

            // Registra la información de la actualización
            this.logger.LogDebug("Actualizando registro - [Id: {Id}, Success: {Success}, Data: {Data}]", id, success, data);

            // Devuelve el resultado de la actualización
            if (!success)
            {
                return NotFound("Record not found or could not be updated");
            }

            return Ok(success);
        }
    }
}
