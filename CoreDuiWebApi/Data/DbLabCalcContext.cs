using System;
using System.Linq;
using CoreDuiWebApi.Authentication.Data;
using CoreDuiWebApi.Email.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CoreDuiWebApi.Data
{
    public class DbLabCalcContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DbLabCalcContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<DbUser> DbUsers { get; set; }
        public DbSet<DbEmail> DbEmails { get; set; }
        public DbSet<DbUserRole> DbUserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbUserRole>()
            .HasIndex(p => new { p.Role, p.DbUserId }).IsUnique();
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.Now;                

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                }

                // var userId = _httpContextAccessor.HttpContext.User.Claims.Select(claim => claim.Type === ClaimTypes)
            }
            return base.SaveChanges();
        }

    }

}
