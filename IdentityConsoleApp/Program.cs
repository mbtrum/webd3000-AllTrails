using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AllTrails.Data;

var host = Host.CreateDefaultBuilder(args)

    // use the connection string and DbContext in your main project
    // ensure the path and naming matches your project
    .ConfigureAppConfiguration((context, config) =>
    {
        config.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "AllTrails"));
        config.AddJsonFile("appsettings.json");
    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AllTrailsContext>(options =>
            options.UseSqlServer(context.Configuration.GetConnectionString("AllTrailsContext")));

        services.AddIdentityCore<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AllTrailsContext>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

Console.WriteLine("=== Identity Provisioning Tool ===");

while (true)
{
    Console.Write("\nAvailable commands (add-role | add-account | exit): ");
    switch (Console.ReadLine()?.Trim().ToLower())
    {
        case "add-role":
            Console.Write("Role name: ");
            var role = Console.ReadLine()?.Trim();
            if (await roleManager.RoleExistsAsync(role))
                Console.WriteLine("Role already exists.");
            else
                Print(await roleManager.CreateAsync(new IdentityRole(role)));
            break;

        case "add-account":
            Console.Write("Email: ");
            var email = Console.ReadLine()?.Trim();
            Console.Write("Password: ");
            var password = Console.ReadLine()?.Trim();
            Console.Write("Role: ");
            var assignRole = Console.ReadLine()?.Trim();

            var user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded && !string.IsNullOrEmpty(assignRole))
                await userManager.AddToRoleAsync(user, assignRole);
            Print(result);
            break;

        case "exit":
            return;

        default:
            Console.WriteLine("Unknown command.");
            break;
    }
}

static void Print(IdentityResult result)
{
    if (result.Succeeded)
        Console.WriteLine("Success.");
    else
        foreach (var e in result.Errors)
            Console.WriteLine($"Error: {e.Description}");
}