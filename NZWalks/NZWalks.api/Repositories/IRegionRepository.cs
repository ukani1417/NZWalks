using NZWalks.api.Models.Domains;

namespace NZWalks.api.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
         
    }
}
