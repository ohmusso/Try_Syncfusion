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
    }

    public static class SeedData
    {
        public static async Task Initialize(
            IServiceProvider serviceProvider,
            IEnumerable<TestUser> userList
        )
        {
            var userManager= serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            foreach( var user in userList)
            {
                await EnsureUser(userManager, user.Name, user.Password);
            }
        }

        private static async Task EnsureUser(
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
        }
    }
}