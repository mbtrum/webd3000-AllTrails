using AllTrails.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllTrails.Data
{
    public class AllTrailsContext : IdentityDbContext<IdentityUser, IdentityRole, string> // inherit from IdentityDbContext instead of DbContext
    {
        public AllTrailsContext (DbContextOptions<AllTrailsContext> options)
            : base(options)
        {
        }

        public DbSet<AllTrails.Models.Trail> Trail { get; set; } = default!;
        public DbSet<AllTrails.Models.TripReport> TripReport { get; set; } = default!;
        public DbSet<AllTrails.Models.UserProfile> UserProfile { get; set; } = default!;
    }
}
