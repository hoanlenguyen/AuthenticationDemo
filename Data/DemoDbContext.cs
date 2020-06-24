using AuthenticationDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationDemo.Data
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        //public DbSet<IdentityUser> IdentityUsers { get; set; }
        //public DbSet<IdentityRole> IdentityRoles { get; set; }
        public DbSet<IdentityUserClaim<string>> UserClaims { get; set; }

        public DbSet<IdentityUserRole<string>> UserRoles { get; set; }

        public DbSet<IdentityRoleClaim<string>> RoleClaims { get; set; }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUser>().ToTable("User");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");

            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Student>().HasOne(p => p.User).WithOne().HasForeignKey<Student>(q => q.UserId);

            modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
        }
    }
}