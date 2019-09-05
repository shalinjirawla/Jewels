using Inventory.Core.Models.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory.EntityFrameworkCore.DbContext
{
    public class SeedDatabase
    {
        public static void initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            context.Database.EnsureCreated();
            if (!context.Users.Any())
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "hemant@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "hemant",
                };
                userManager.CreateAsync(user, "Password@123");
            }
        }
    }
}
