using FamilyTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTracker.Infrastructure.Persistence.Configurations;

public class DoctorAppointmentConfiguration : IEntityTypeConfiguration<DoctorAppointment>
{
    public void Configure(EntityTypeBuilder<DoctorAppointment> builder)
    {
        builder.ToTable("DoctorAppointments");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.AppointmentDateTime)
            .IsRequired();

        builder.Property(a => a.IsCompleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        builder.Property(a => a.UpdatedAt)
            .IsRequired();

        builder.HasOne(a => a.CreatedByUser)
            .WithMany()
            .HasForeignKey(a => a.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.AppointmentForUser)
            .WithMany()
            .HasForeignKey(a => a.AppointmentForUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(a => a.AppointmentDateTime);
        builder.HasIndex(a => a.IsCompleted);
        builder.HasIndex(a => a.CreatedByUserId);
        builder.HasIndex(a => a.AppointmentForUserId);
    }
}
