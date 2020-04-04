using CoreDuiWebApi.Authentication.DbUserEf;
using CoreDuiWebApi.Authentication.DbUserRoleEf;
using CoreDuiWebApi.Email.DbEmailEf;
using Microsoft.EntityFrameworkCore;

namespace CoreDuiWebApi
{
    public class DbLabCalcContext : DbContext
    {
        public DbLabCalcContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<DbUser> DbUsers { get; set; }
        public DbSet<DbEmail> DbEmails { get; set; }
        public DbSet<DbUserRole> DbUserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbUserRole>()
            .HasIndex(p => new { p.Role, p.DbUserId }).IsUnique();
        }

    }

}
