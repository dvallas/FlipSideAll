using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlipSideModels;

namespace FlipsideMVC
{
    public class FlipSideDataContext : DbContext
    {
        public FlipSideDataContext (DbContextOptions<FlipSideDataContext> options)
            : base(options)
        {
        }

        public DbSet<Story> Story { get; set; }
        public DbSet<Topics> Topics { get; set; }
    }
}
