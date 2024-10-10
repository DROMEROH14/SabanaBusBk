using SabanaBus.Context;
using SabanaBus.Modelo;
using Microsoft.EntityFrameworkCore;
using SabanaBus.Services;

namespace SabanaBus.Repositories

{
   
    public class RouteRepositories : IRouteService
    {
        private readonly SabanaBusDbContext _dbContext;

        public RouteRepositories(SabanaBusDbContext dbContext)
        {
            _dbContext = dbContext;

        }



        public async Task<bool> CreateRouteAsync(Modelo.Route Route)
        {
            var dataRoute = await _dbContext.Routes.AddAsync(Route);
            var result = await _dbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }

            return true;
        }

        public async Task DeleteRouteAsync(int id)
        {

            var Route = await _dbContext.Routes.FindAsync(id);


            if (Route == null)
            {

                throw new KeyNotFoundException($"Route with ID {id} not found.");
            }

            _dbContext.Routes.Remove(Route);


            await _dbContext.SaveChangesAsync();

        }


        public async Task<IEnumerable<Modelo.Route>> GetAllRoutesAsync()
        {
            return await _dbContext.Routes
            .Where(s => s.IsDeleted)
            .ToListAsync();
        }


        public async Task<Modelo.Route> GetRouteByIdAsync(int id)
        {
            var Route = await _dbContext.Routes
               .FirstOrDefaultAsync(s => s.IdRoute == id && s.IsDeleted);

            if (Route == null)
            {
                throw new Exception("Route no encontrado.");
            }

            return Route;

        }

        public async Task<bool> UpdateRouteAsync(int id, Modelo.Route Route)
        {
            try
            {
                var entity = await this._dbContext.Routes.FindAsync(id);
                if (entity != null)
                {
                    entity.RouteName = Route.RouteName;
                    entity.Origin = Route.Origin;
                    entity.Destination = Route.Destination;
                    entity.EstimatedDuration = Route.EstimatedDuration;
                    entity.IsDeleted = Route.IsDeleted;


                    this._dbContext.Update(entity);

                    return (await this._dbContext.SaveChangesAsync()) > 0;
                }
                return false;

            }
            catch (Exception)
            {

                throw;


            }

        }


    }
}