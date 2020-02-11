using Microsoft.EntityFrameworkCore;

namespace CoreDuiWebApi.Authentication.DbUserEf
{
    public class DbUserContext : DbContext
    {
        public DbUserContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<DbUser> DbUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DbUser>(entity => {
                entity.HasIndex(e => e.EmailAddress).IsUnique();
            });
        }

    }
}
