using FamilyTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTracker.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Birthday)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasMaxLength(200);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(u => u.UserRole)
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .IsRequired();

        builder.Ignore(u => u.UserAge);

        builder.HasMany(u => u.AppointmentsCreated)
            .WithOne(a => a.CreatedByUser)
            .HasForeignKey(a => a.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.AppointmentsFor)
            .WithOne(a => a.AppointmentForUser)
            .HasForeignKey(a => a.AppointmentForUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.ShoppingItemsCreated)
            .WithOne(s => s.CreatedBy)
            .HasForeignKey(s => s.CreatedByUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(u => u.UserName).IsUnique();
    }
}
