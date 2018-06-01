using FlipSideModels;
using Microsoft.EntityFrameworkCore;

namespace FlipSideMVC.Data
{
    public class FlipSideDataContext : DbContext
    {
        public FlipSideDataContext(DbContextOptions<FlipSideDataContext> options)
            : base(options)
        {
        }

        public DbSet<Story> Story { get; set; }
        public DbSet<Topics> Topics { get; set; }
    }
}
