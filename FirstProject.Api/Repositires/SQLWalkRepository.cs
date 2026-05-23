using FirstProject.Api.Data;
using FirstProject.Api.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Api.Repositires
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly WalkDbContext dbContext;

        public SQLWalkRepository(WalkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
           await dbContext.Walks.AddAsync(walk);
           await dbContext.SaveChangesAsync();
           return walk;
        }


        public async Task<Walk> DeleteByIdAsync(Guid id)
        {
            var existingWalk = dbContext.Walks.FirstOrDefault(x => x.Id == id);
            if (existingWalk == null) 
            {
                return null;

            }

            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<List<Walk>> GetAllWalkAsync(int pageNumber =1, int pageSize =1000)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();


            //pagination
            return await walks.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();  
            //return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = dbContext.Walks.FirstOrDefault(x => x.Id == id);
            if (existingWalk == null) 
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();

            return existingWalk;

        }
    }
}
