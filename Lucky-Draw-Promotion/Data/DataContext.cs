using Lucky_Draw_Promotion.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lucky_Draw_Promotion.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(

               new IdentityRole
               {
                   Name = "SuperAdmin",
                   NormalizedName = "SuperAdmin"
               },
               new IdentityRole
               {
                   Name = "Administrator",
                   NormalizedName = "Administrator"
               });

            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "User");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });




        }



    }
}
