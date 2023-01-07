using Microsoft.EntityFrameworkCore;
using NZWalks.api.Models.Domains;

namespace NZWalks.api.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext>options):base(options)
        {

        }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<WalkDiffculty> WalkDiffculties { get; set; }
    }
}
