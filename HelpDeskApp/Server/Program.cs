using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using HelpDeskApp.Server.Data;

namespace HelpDeskApp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Initialize the database
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context =services.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();

                    // requires using Microsoft.Extensions.Configuration;
                    // Set password with the Secret Manager tool.
                    // dotnet user-secrets set SeedUserPW <pw>
                    //var config = host.Services.GetRequiredService<IConfiguration>();
                    //var testUserPw = config["SeedUserPW"];
                    var config = host.Services.GetRequiredService<IConfiguration>();
                    var testUsersSection = config.GetSection("TestUsers");
                    var userList = new List<TestUser>();

                    foreach (IConfigurationSection testUserSection in testUsersSection.GetChildren())
                    {
                        var user = new TestUser()
                        {
                            Name = testUserSection.GetValue<string>("Name"),
                            Password = testUserSection.GetValue<string>("Password"),
                            Role = testUserSection.GetValue<string>("Role")
                        };
                        userList.Add(user);
                    }

                    SeedData.Initialize(services, userList).Wait();
                }
                catch(Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred createing SeedData.");
                }
            }
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
