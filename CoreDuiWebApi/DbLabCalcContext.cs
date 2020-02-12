using CoreDuiWebApi.Authentication.DbUserEf;
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
    }
}
