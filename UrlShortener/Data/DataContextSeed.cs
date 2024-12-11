using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Models;

public static class DataContextSeed
{
    public static async Task SeedAsync(
        DataContext context, 
        UserManager<User> userManager, 
        RoleManager<IdentityRole<int>> roleManager)
    {
        await context.Database.MigrateAsync();

        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole<int>("Admin"));

        if (!await roleManager.RoleExistsAsync("User"))
            await roleManager.CreateAsync(new IdentityRole<int>("User"));

        if (await userManager.FindByNameAsync("admin") == null)
        {
            var adminUser = new User
            {
                UserName = "admin",
                Email = "admin@example.com",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "AdminPassword123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        if (await userManager.FindByNameAsync("user") == null)
        {
            var normalUser = new User
            {
                UserName = "user",
                Email = "user@example.com",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(normalUser, "UserPassword123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(normalUser, "User");
            }
        }

        if (!context.Description.Any())
        {
            var description = new Description
            {
                Content = "This is the URL Shortener algorithm: The original URL is hashed using SHA-256, and the first 8 characters of the hash are used to create the short URL."
            };

            context.Description.Add(description);
            await context.SaveChangesAsync();
        }
    }
}
