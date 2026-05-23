using FirstProject.Api.Model.Domain;

namespace FirstProject.Api.Repositires
{
    public interface IRepository
    {
        Task<List<Region>> GetAllRegionsAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true);
        Task<Region?>GetByIdAsync(Guid id);

        Task<Region> CreateAsync(Region region);
        Task<Region?> UpdateAsync(Guid id, Region region);
        Task<Region?> DeleteAsync(Guid id);
        
    }
}
