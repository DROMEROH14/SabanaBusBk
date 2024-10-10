using SabanaBus.Context;
using SabanaBus.Modelo;
using Microsoft.EntityFrameworkCore;
using SabanaBus.Services;

namespace SabanaBus.Repositories

{

    public class AssignmentRepositories : IAssignmentService
    {
        private readonly SabanaBusDbContext _dbContext;

        public AssignmentRepositories(SabanaBusDbContext dbContext)
        {
            _dbContext = dbContext;

        }



        public async Task<bool> CreateAssignmentAsync(Assignment Assignment)
        {
            var dataAssignment = await _dbContext.Assignment.AddAsync(Assignment);
            var result = await _dbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }

            return true;
        }



        public async Task DeleteAssignmentAsync(int id)
        {

            var Assignment = await _dbContext.Assignment.FindAsync(id);


            if (Assignment == null)
            {

                throw new KeyNotFoundException($"Assignment with ID {id} not found.");
            }

            _dbContext.Assignment.Remove(Assignment);


            await _dbContext.SaveChangesAsync();

        }


        public async Task<IEnumerable<Assignment>> GetAllAssignmentAsync()
        {
            return await _dbContext.Assignment
            .Where(s => s.IsDeleted)
            .ToListAsync();
        }



        public async Task<Assignment> GetAssignmentByIdAsync(int id)
        {
            var Assignment = await _dbContext.Assignment
               .FirstOrDefaultAsync(s => s.AssignmentID == id && s.IsDeleted);

            if (Assignment == null)
            {
                throw new Exception("Assignment no encontrado.");
            }

            return Assignment;

        }


        public async Task<bool> UpdateAssignmentAsync(int id, Assignment Assignment)
        {
            try
            {
                var entity = await this._dbContext.Assignment.FindAsync(id);
                if (entity != null)
                {

                    entity.FkBusID = Assignment.FkBusID;
                    entity.FkRouteID = Assignment.FkRouteID;
                    entity.AssignmentDate = Assignment.AssignmentDate;
                    entity.IsDeleted = Assignment.IsDeleted;


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