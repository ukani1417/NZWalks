using Microsoft.EntityFrameworkCore;
using NZWalks.api.Data;
using NZWalks.api.Models.Domain;

namespace NZWalks.api.Repositories
{
    public class WalkDiffcultyRepository : IWalkDiffcultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;
        public WalkDiffcultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDiffculty> AddAsync(WalkDiffculty walkDiffculty)
        {

            if (walkDiffculty == null)
            {
                return null;
            }
            walkDiffculty.Id = Guid.NewGuid();
            nZWalksDbContext.WalkDiffculties.Add(walkDiffculty);
            await nZWalksDbContext.SaveChangesAsync();

            return walkDiffculty;
        }
        public async Task<WalkDiffculty> DeleteAsync(Guid id)
        {
            var wd = await nZWalksDbContext.WalkDiffculties.FirstOrDefaultAsync(x => x.Id == id);

            if (wd == null)
            {
                return null;
            }

            nZWalksDbContext.WalkDiffculties.Remove(wd);
            await nZWalksDbContext.SaveChangesAsync();

            return wd;
        }

        public async Task<IEnumerable<WalkDiffculty>> GetAllAsync()
        {
            return await nZWalksDbContext.WalkDiffculties.ToListAsync();
        }

        public async Task<WalkDiffculty> GetAsync(Guid id)
        {
            var wd = await nZWalksDbContext.WalkDiffculties.FirstOrDefaultAsync(x => x.Id == id);

            if (wd == null)
            {
                return null;
            }

            return wd;

        }

        public async Task<WalkDiffculty> UpdateAsync(Guid id, WalkDiffculty walkDiffculty)
        {
            var wd = await nZWalksDbContext.WalkDiffculties.FirstOrDefaultAsync(x => x.Id == id);

            if (wd == null)
            {
                return null;
            }
            wd.Code = walkDiffculty.Code;

            await nZWalksDbContext.SaveChangesAsync();

            return wd;
        }
    }
}
