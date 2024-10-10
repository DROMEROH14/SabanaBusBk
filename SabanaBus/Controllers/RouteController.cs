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
    public class RouteController : ControllerBase
    {

        private readonly IRouteService service;

        private readonly ILogger<RouteController> logger;

        /// Constructor para la clase <see cref="RouteController"/>

        /// <param name="repository"></param>
        public RouteController(IRouteService service, ILogger<RouteController> logger)
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
            var data = await this.service.GetAllRoutesAsync();
            this.logger.LogDebug("Consultando todos los {registros}", data);

            if (data == null || !data.Any())
            {
                return NotFound("No records found.");
            }

            // Convertir el modelo a DTO
            var routesDto = data.Select(route => new RouteDTO
            {
                IdRoute = route.IdRoute,
                RouteName = route.RouteName,
                Origin = route.Origin,
                Destination = route.Destination,
                EstimatedDuration = route.EstimatedDuration,
                IsDeleted = route.IsDeleted
            }).ToList();

            return Ok(routesDto);

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
            if (id < 0)
                return NotFound("Arguments invalids");

            var result = await service.GetRouteByIdAsync(id);
            if (result == null)
            {
                return NotFound("Route not found.");
            }

            var routeDto = new RouteDTO
            {
                IdRoute = result.IdRoute,
                RouteName = result.RouteName,
                Origin = result.Origin,
                Destination = result.Destination,
                EstimatedDuration = result.EstimatedDuration,
                IsDeleted = result.IsDeleted
            };

            return Ok(routeDto);
        }

        /// <param name="data"></param>
        /// <returns>Retorna el Id de la compañia</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(RouteDTO routeDto)
        {
            if (routeDto == null)
                return NotFound("Arguments invalids");

            // Convertir el DTO al modelo
            var route = new SabanaBus.Modelo.Route
            {
                IdRoute = routeDto.IdRoute,
                RouteName = routeDto.RouteName,
                Origin = routeDto.Origin,
                Destination = routeDto.Destination,
                EstimatedDuration = routeDto.EstimatedDuration,
                IsDeleted = routeDto.IsDeleted
            };

            var dataCreate = await this.service.CreateRouteAsync(route);
            if (!dataCreate)
            {
                return NotFound("Failed to create the record");
            }

            this.logger.LogDebug("Creando registro - [Data: {data}]", routeDto);
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
        public async Task<IActionResult> Update(int id, RouteDTO routeDto)
        {
            if (id < 0)
                return NotFound("id is not valid");
            if (routeDto == null)
                return NotFound("Data is not valid");

            var route = new SabanaBus.Modelo.Route
            {
                IdRoute = routeDto.IdRoute,
                RouteName = routeDto.RouteName,
                Origin = routeDto.Origin,
                Destination = routeDto.Destination,
                EstimatedDuration = routeDto.EstimatedDuration,
                IsDeleted = routeDto.IsDeleted
            };

            var success = await service.UpdateRouteAsync(id, route);

            this.logger.LogDebug("Actualizando registro - [Id: {Id}, Success: {Success}, Route: {Route}]", id, success, routeDto);

            if (!success)
            {
                return NotFound("Record not found or could not be updated");
            }

            return Ok(success);
        }
    }
}



