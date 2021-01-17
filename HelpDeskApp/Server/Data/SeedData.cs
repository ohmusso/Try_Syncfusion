using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDeskApp.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HelpDeskApp.Server.Data
{
    public class TestUser{
        public string Name {get; set; }
        public string Password { get; set;}
        public string Role { get; set;}
    }

    public static class SeedData
    {
        public static async Task Initialize(
            IServiceProvider serviceProvider,
            IEnumerable<TestUser> userList
        )
        {
            var userManager= serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager= serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach( var user in userList)
            {
                var id = await EnsureUser(userManager, user.Name, user.Password);
                await EnsureRole(userManager, roleManager, id, user.Role);
            }
        }

        private static async Task<string> EnsureUser(
            UserManager<ApplicationUser> userManager,
            string userName,
            string userPassword
        )
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new ApplicationUser(userName)
                {
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, userPassword);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            string uid,
            string role
        )
        {
            IdentityResult IR = null;

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));

                var user = await userManager.FindByIdAsync(uid);

                if(user == null)
                {
                    throw new Exception("The testUserPw password was probably not strong enough!");
                }
                
                IR = await userManager.AddToRoleAsync(user, role);
            }

            return IR;
        }
    }
}