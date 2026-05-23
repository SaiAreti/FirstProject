using FirstProject.Api.Controllers;
using FirstProject.Api.Data;
using FirstProject.Api.Model.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;

namespace FirstProject.Api.Repositires
{
    public class SQLRegionRepository : IRepository
    {
        private readonly WalkDbContext dbContext;
        public SQLRegionRepository(WalkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
              var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
              if (existingRegion == null) 
              {
                 return null;
              }
              dbContext.Regions.Remove(existingRegion);
              await dbContext.SaveChangesAsync();
              return existingRegion;
        }

        public async Task<List<Region>> GetAllRegionsAsync(string? filterOn, string? filterQuery,string? sortBy, bool isAscending)
        {
            var regions =  dbContext.Regions.AsQueryable();

            //filtering 
            if(string.IsNullOrEmpty(filterOn) == false && string.IsNullOrEmpty(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase) ) 
                {
                    regions = regions.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //sorting
            if (string.IsNullOrEmpty(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    regions = isAscending ? regions.OrderBy(x => x.Name) : regions.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Code", StringComparison.OrdinalIgnoreCase)) 
                {
                    regions = isAscending ? regions.OrderBy(x=> x.Code) : regions.OrderByDescending(x => x.Code);
                }
            }



            return await regions.ToListAsync();
            //return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null) 
            {
                return null;
            }
            existingRegion.Name = region.Name;
            existingRegion.Code = region.Code;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
