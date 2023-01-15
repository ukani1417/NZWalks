using Microsoft.EntityFrameworkCore;
using NZWalks.api.Data;
using NZWalks.api.Models.Domain;

namespace NZWalks.api.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            if (walk == null)
            {
                return null;
            }
            walk.Id = Guid.NewGuid();
            nZWalksDbContext.Walks.Add(walk);
            await nZWalksDbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walk = await nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walk == null)
            {
                return null;
            }

            nZWalksDbContext.Walks.Remove(walk);
            await nZWalksDbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await nZWalksDbContext.Walks.Include(x => x.Region)
                                               .Include(x => x.walkDiffculty)
                                               .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            var walk = await nZWalksDbContext.Walks.Include(x => x.Region)
                                                   .Include(x => x.walkDiffculty)
                                                   .FirstOrDefaultAsync(x => x.Id == id);

            if (walk == null)
            {
                return null;
            }
            return walk;
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var request = await nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (request == null) { return null; }

            request.Length = walk.Length;
            request.WalkDiffcultyId = walk.WalkDiffcultyId;
            request.Name = walk.Name;
            request.RegionId = walk.RegionId;

            nZWalksDbContext.SaveChanges();
            return request;
        }
    }
}
