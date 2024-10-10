using SabanaBus.Context;
using SabanaBus.Modelo;
using Microsoft.EntityFrameworkCore;
using SabanaBus.Services;

namespace SabanaBus.Repositories

{
 
    public class BusRepositories : IBusService
    {
        private readonly SabanaBusDbContext _dbContext;

        public BusRepositories(SabanaBusDbContext dbContext)
        {
            _dbContext = dbContext;

        }



        public async Task<bool> CreateBusAsync(Bus Bus)
        {
            var dataBus = await _dbContext.Bus.AddAsync(Bus);
            var result = await _dbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }

            return true;
        }

        public async Task DeleteBusAsync(int id)
        {

            var Bus = await _dbContext.Bus.FindAsync(id);


            if (Bus == null)
            {

                throw new KeyNotFoundException($"Bus with ID {id} not found.");
            }

            _dbContext.Bus.Remove(Bus);


            await _dbContext.SaveChangesAsync();

        }


        public async Task<IEnumerable<Bus>> GetAllBusAsync()
        {
            return await _dbContext.Bus
            .Where(s => s.IsDeleted)
            .ToListAsync();
        }




        public async Task<Bus> GetBusByIdAsync(int id)
        {
            var Bus = await _dbContext.Bus
               .FirstOrDefaultAsync(s => s.IdBus == id && s.IsDeleted);

            if (Bus == null)
            {
                throw new Exception("Bus no encontrado.");
            }

            return Bus;

        }

        public async Task<bool> UpdateBusAsync(int id,Bus Bus)
        {
            try
            {
                var entity = await this._dbContext.Bus.FindAsync(id);
                if (entity != null)
                {

                    entity.LicensePlate = Bus.LicensePlate;
                    entity.Capacity = Bus.Capacity;
                    entity.Status = Bus.Status;
                    entity.IsDeleted = Bus.IsDeleted;


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