using FamilyTracker.Application.Interfaces;
using FamilyTracker.Domain.Entities;
using FamilyTracker.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyTracker.Infrastructure.Seeders;

public class DataSeeder
{
    private readonly IPasswordHashService _passwordHashService;
    private readonly ILogger<DataSeeder> _logger;

    public DataSeeder(IPasswordHashService passwordHashService, ILogger<DataSeeder> logger)
    {
        _passwordHashService = passwordHashService;
        _logger = logger;
    }

    public async Task SeedAsync(DbContext context)
    {
        try
        {
            var users = await SeedUsersAsync(context);
            _logger.LogInformation("Database seeding completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }

    private async Task<List<User>> SeedUsersAsync(DbContext context)
    {
        var usersSet = context.Set<User>();
        
        if (await usersSet.AnyAsync())
        {
            _logger.LogInformation("Users already exist. Skipping user seeding.");
            return await usersSet.ToListAsync();
        }

        var users = new List<User>
        {
            new User
            {
                UserName = "Sergey",
                Birthday = DateTime.SpecifyKind(new DateTime(1986, 6, 21), DateTimeKind.Utc),
                Email = "sergey.solokho@gmail.com",
                PasswordHash = _passwordHashService.HashPassword("210686"),
                UserRole = UserRole.User | UserRole.AdminUser
            },
            new User
            {
                UserName = "Natallia",
                Birthday = DateTime.SpecifyKind(new DateTime(1993, 9, 19), DateTimeKind.Utc),
                Email = "natalia19932011@gmail.com",
                PasswordHash = _passwordHashService.HashPassword("190993"),
                UserRole = UserRole.User
            },
            new User
            {
                UserName = "Dasha",
                Birthday = DateTime.SpecifyKind(new DateTime(2020, 5, 18), DateTimeKind.Utc),
                Email = null,
                PasswordHash = _passwordHashService.HashPassword("180520"),
                UserRole = UserRole.User
            },
            new User
            {
                UserName = "Alex",
                Birthday = DateTime.SpecifyKind(new DateTime(2025, 8, 15), DateTimeKind.Utc),
                Email = null,
                PasswordHash = _passwordHashService.HashPassword("150525"),
                UserRole = UserRole.User
            },
            new User
            {
                UserName = "Home",
                Birthday = DateTime.SpecifyKind(new DateTime(2024, 8, 15), DateTimeKind.Utc),
                Email = null,
                PasswordHash = _passwordHashService.HashPassword("111111"),
                UserRole = UserRole.TabletUser | UserRole.User
            }
        };

        await usersSet.AddRangeAsync(users);
        await context.SaveChangesAsync();

        _logger.LogInformation("Seeded {Count} users", users.Count);
        
        return users;
    }
}
