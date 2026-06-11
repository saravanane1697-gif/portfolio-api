using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Models;

namespace PortfolioAPI.Data
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<Experience> Experiences { get; set; }

        public DbSet<ContactMessage> ContactMessages { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<AdminUser> AdminUsers { get; set; }
    }
}
