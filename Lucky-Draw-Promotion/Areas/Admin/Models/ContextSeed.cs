using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucky_Draw_Promotion.Models;
using Lucky_Draw_Promotion.Models.Account;

namespace Lucky_Draw_Promotion.Areas.Admin.Models
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(enumRoles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(enumRoles.Administrator.ToString()));
           
        }
        public static async Task SeedSuperAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new User
            {
                UserName = "khanhthoai99@gmail.com",
                Email = "khanhthoai99@gmail.com",                     
                EmailConfirmed = true,
                PhoneNumberConfirmed=true,
            };
           
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Thoai9123@");
                    await userManager.AddToRoleAsync(defaultUser, enumRoles.SuperAdmin.ToString());
                }
            }
        }
    }
}
