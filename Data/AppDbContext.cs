using Microsoft.EntityFrameworkCore;
using NcnApi.Models;

namespace NcnApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<NomTask> Tasks { get; set; } = default!;
    }
}
