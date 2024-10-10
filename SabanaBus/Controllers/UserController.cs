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
    public class UserController : ControllerBase
    {
        private readonly IUserService service;

        private readonly ILogger<UserController> logger;

        /// Constructor para la clase <see cref="UserController"/>

        /// <param name="repository"></param>
        public UserController(IUserService service, ILogger<UserController> logger)
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
            var data = await this.service.GetAllUsersAsync();

            this.logger.LogDebug("Consultando todos los {registros}", data);

            // Validamos que la data no sea null o de otro tipo
            if (data == null || !data.Any())
            {
                return NotFound("No records found.");
            }

            // Mapear la lista de usuarios a UserDTO
            var userDTOs = data.Select(user => new UserDTO
            {
                IdUser = user.IdUser,
                FKIdUserType = user.FKIdUserType,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                DateCreated = user.DateCreated,
                ModifiedDate = user.ModifiedDate,
                ModifiedBy = user.ModifiedBy,
                IsDeleted = user.IsDeleted
            }).ToList();

            return Ok(userDTOs);

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
            var result = await service.GetUserByIdAsync(id);

            if (result == null)
            {
                return NotFound("User not found.");
            }

            // Mapear el usuario a UserDTO
            var userDTO = new UserDTO
            {
                IdUser = result.IdUser,
                FKIdUserType = result.FKIdUserType,
                Name = result.Name,
                LastName = result.LastName,
                Email = result.Email,
                DateCreated = result.DateCreated,
                ModifiedDate = result.ModifiedDate,
                ModifiedBy = result.ModifiedBy,
                IsDeleted = result.IsDeleted
            };

            return Ok(userDTO);
        }

        /// <param name="data"></param>
        /// <returns>Retorna el Id de la compañia</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(UserDTO userDTO)
        {
            // Validamos que la data no sea nula
            if (userDTO == null)
                return NotFound("Arguments invalids");

            // Mapear UserDTO a User
            var user = new User
            {
                FKIdUserType = userDTO.FKIdUserType,
                Name = userDTO.Name,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                DateCreated = userDTO.DateCreated,
                ModifiedDate = userDTO.ModifiedDate,
                ModifiedBy = userDTO.ModifiedBy,
                IsDeleted = userDTO.IsDeleted
            };

            // Creamos
            var dataCreate = await this.service.CreateUserAsync(user);

            // Verifica si la creación fue exitosa (si la creación retorna algún valor que indique éxito)
            if (!dataCreate)
            {
                return NotFound("Failed to create the record");
            }

            this.logger.LogDebug("Creando registro - [Data: {data}]", user);
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
        public async Task<IActionResult> Update(int id, UserDTO userDTO)
        {
            if (id < 0)
                return NotFound("id is not valid");
            if (userDTO == null)
                return NotFound("Data is not valid");

            // Mapear UserDTO a User
            var user = new User
            {
                IdUser = id, // Asignar el ID desde la ruta
                FKIdUserType = userDTO.FKIdUserType,
                Name = userDTO.Name,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                DateCreated = userDTO.DateCreated,
                ModifiedDate = userDTO.ModifiedDate,
                ModifiedBy = userDTO.ModifiedBy,
                IsDeleted = userDTO.IsDeleted
            };

            var success = await service.UpdateUserAsync(id, user);

            // Registra la información de la actualización
            this.logger.LogDebug("Actualizando registro - [Id: {Id}, Success: {Success}, Data: {Data}]", id, success, user);

            // Devuelve el resultado de la actualización
            if (!success)
            {
                return NotFound("Record not found or could not be updated");
            }

            return Ok(success);
        }
    }
}

    

