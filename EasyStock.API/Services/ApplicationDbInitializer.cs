using EasyStock.API.EntityFramework;
using EasyStock.Library.Entities;
using EasyStock.Library.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EasyStock.API.Services
{
    public static class ApplicationDbInitializer
    {
        public static async Task SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, EasyStockContext context)
        {
            // Check if the "Admin" role exists
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                // Create the "Admin" role using ApplicationRole
                await roleManager.CreateAsync(new ApplicationRole { Name = "Admin" });
            }

            // Check if there are any users
            if (!userManager.Users.Any())
            {
                // Handle Company Creation
                var defaultCompany = await context.Companies.FirstOrDefaultAsync();
                if (defaultCompany == null)
                {
                    defaultCompany = new Company { Name = "HF Agency" };
                    context.Companies.Add(defaultCompany);
                    await context.SaveChangesAsync();
                }

                var user = new ApplicationUser
                {
                    UserName = "admin@hfagency.com",
                    Email = "admin@hfagency.com",
                    EmailConfirmed = true,
                    CompanyId = defaultCompany.Id
                };

                // Set a secure password here
                var result = await userManager.CreateAsync(user, "Admin123!");

                if (result.Succeeded)
                {
                    // Assign the "Admin" role to the user
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    // Handle errors
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creating the user: {error.Description}");
                    }
                }
            }
        }
    }
}
