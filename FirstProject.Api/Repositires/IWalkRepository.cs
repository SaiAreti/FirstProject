using FirstProject.Api.Model.Domain;

namespace FirstProject.Api.Repositires
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllWalkAsync(int pageNumbet = 1, int PageSize =1000);
        Task<Walk> GetByIdAsync(Guid id);
        Task<Walk> UpdateAsync(Guid id, Walk walk);
        Task<Walk> DeleteByIdAsync(Guid id);


    }
}
