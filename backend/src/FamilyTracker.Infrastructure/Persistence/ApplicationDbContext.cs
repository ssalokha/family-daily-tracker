using FamilyTracker.Domain.Entities;
using FamilyTracker.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FamilyTracker.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<DoctorAppointment> DoctorAppointments => Set<DoctorAppointment>();
    public DbSet<ShoppingItem> ShoppingItems => Set<ShoppingItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Configure Location value object globally
        modelBuilder.Entity<DoctorAppointment>()
            .Property(e => e.Location)
            .HasConversion(
                v => $"{v.Street}|{v.BuildingNumber}",
                v => ParseLocation(v));
    }

    private static Location ParseLocation(string value)
    {
        var parts = value.Split('|');
        return Location.Create(parts[0], parts[1]);
    }
}
