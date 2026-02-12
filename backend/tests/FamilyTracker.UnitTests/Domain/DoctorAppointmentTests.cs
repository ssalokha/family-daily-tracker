using FamilyTracker.Domain.Entities;
using FamilyTracker.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace FamilyTracker.UnitTests.Domain;

public class DoctorAppointmentTests
{
    [Fact]
    public void DoctorAppointment_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var location = Location.Create("Main Street", "123");
        var appointment = new DoctorAppointment
        {
            Id = Guid.NewGuid(),
            CreatedByUserId = Guid.NewGuid(),
            AppointmentForUserId = Guid.NewGuid(),
            AppointmentDateTime = DateTime.UtcNow.AddDays(7),
            Location = Location.Create("Main Street", "123"),
            IsCompleted = false
        };

        // Assert
        appointment.Id.Should().NotBeEmpty();
        appointment.IsCompleted.Should().BeFalse();
        appointment.Location.ToString().Should().Be("Main Street 123");
        appointment.Location.ToString().Should().Be("Main Street 123");
    }

    [Fact]
    public void DoctorAppointment_CanBe_MarkedAsCompleted()
    {
        // Arrange
        var location = Location.Create("Main Street", "123");
        var appointment = new DoctorAppointment
        {
            Id = Guid.NewGuid(),
            CreatedByUserId = Guid.NewGuid(),
            AppointmentForUserId = Guid.NewGuid(),
            AppointmentDateTime = DateTime.UtcNow.AddDays(7),
            Location = Location.Create("Main Street", "123"),
            IsCompleted = false
        };

        // Act
        appointment.IsCompleted = true;

        // Assert
        appointment.IsCompleted.Should().BeTrue();
    }

    [Fact]
    public void DoctorAppointment_ShouldHave_CreatedAndUpdatedTimestamps()
    {
        // Arrange
        var now = DateTime.UtcNow;
        var location = Location.Create("Main Street", "123");
        var appointment = new DoctorAppointment
        {
            Id = Guid.NewGuid(),
            CreatedByUserId = Guid.NewGuid(),
            AppointmentForUserId = Guid.NewGuid(),
            AppointmentDateTime = now.AddDays(7),
            Location = Location.Create("Main Street", "123"),
            CreatedAt = now,
            UpdatedAt = now
        };

        // Assert
        appointment.CreatedAt.Should().BeCloseTo(now, TimeSpan.FromSeconds(1));
        appointment.UpdatedAt.Should().BeCloseTo(now, TimeSpan.FromSeconds(1));
    }

    [Theory]
    [InlineData("Main Street", "123")]
    [InlineData("Oak Avenue", "456")]
    [InlineData("First Street", "1")]
    public void DoctorAppointment_ShouldAccept_DifferentLocations(string street, string buildingNumber)
    {
        // Arrange & Act
        var location = Location.Create(street, buildingNumber);
        var appointment = new DoctorAppointment
        {
            Id = Guid.NewGuid(),
            CreatedByUserId = Guid.NewGuid(),
            AppointmentForUserId = Guid.NewGuid(),
            AppointmentDateTime = DateTime.UtcNow.AddDays(7),
            Location = location
        };

        // Assert
        appointment.Location.ToString().Should().Be($"{street} {buildingNumber}");
    }
}
