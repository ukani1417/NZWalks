using NZWalks.api.Models.Domain;

namespace NZWalks.api.Repositories
{
    public interface IWalkDiffcultyRepository
    {
        Task<IEnumerable<WalkDiffculty>> GetAllAsync();
        Task<WalkDiffculty> GetAsync(Guid id);

        Task<WalkDiffculty> AddAsync(WalkDiffculty walkDiffculty);

        Task<WalkDiffculty> DeleteAsync(Guid id);

        Task<WalkDiffculty> UpdateAsync(Guid id, WalkDiffculty walkDiffculty);
    }
}
