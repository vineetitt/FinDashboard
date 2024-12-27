using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinDashboard.API.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var userRoleId = "7b4a7311-2735-4a8f-b173-dfd5a1e8349f";
            var adminRoleId = "876bf57e-6adf-45ff-b453-8ca9b9054b93";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id= userRoleId,
                    ConcurrencyStamp =  userRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                },
                new IdentityRole
                {
                    Id= adminRoleId,
                    ConcurrencyStamp =  adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
