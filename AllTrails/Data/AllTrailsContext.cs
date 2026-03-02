using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AllTrails.Models;

namespace AllTrails.Data
{
    public class AllTrailsContext : DbContext
    {
        public AllTrailsContext (DbContextOptions<AllTrailsContext> options)
            : base(options)
        {
        }

        public DbSet<AllTrails.Models.Trail> Trail { get; set; } = default!;
        public DbSet<AllTrails.Models.TripReport> TripReport { get; set; } = default!;
    }
}
