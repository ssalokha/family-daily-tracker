using FamilyTracker.Application.Commands.Appointments;
using FamilyTracker.Domain.ValueObjects;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace FamilyTracker.UnitTests.Validators;

public class CreateAppointmentCommandValidatorTests
{
    private readonly CreateAppointmentCommandValidator _validator;

    public CreateAppointmentCommandValidatorTests()
    {
        _validator = new CreateAppointmentCommandValidator();
    }

    [Fact]
    public void Should_HaveError_When_CreatedByUserIdIsEmpty()
    {
        // Arrange
        var command = new CreateAppointmentCommand
        {
            CreatedByUserId = Guid.Empty,
            AppointmentForUserId = Guid.NewGuid(),
            AppointmentDateTime = DateTime.UtcNow.AddDays(7),
            Street = "Main Street",
            BuildingNumber = "123"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CreatedByUserId);
    }

    [Fact]
    public void Should_HaveError_When_AppointmentForUserIdIsEmpty()
    {
        // Arrange
        var command = new CreateAppointmentCommand
        {
            CreatedByUserId = Guid.NewGuid(),
            AppointmentForUserId = Guid.Empty,
            AppointmentDateTime = DateTime.UtcNow.AddDays(7),
            Street = "Main Street",
            BuildingNumber = "123"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.AppointmentForUserId);
    }

    [Fact]
    public void Should_HaveError_When_AppointmentDateTimeIsInPast()
    {
        // Arrange
        var command = new CreateAppointmentCommand
        {
            CreatedByUserId = Guid.NewGuid(),
            AppointmentForUserId = Guid.NewGuid(),
            AppointmentDateTime = DateTime.UtcNow.AddDays(-1),
            Street = "Main Street",
            BuildingNumber = "123"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.AppointmentDateTime);
    }

    [Fact]
    public void Should_HaveError_When_StreetIsEmpty()
    {
        // Arrange
        var command = new CreateAppointmentCommand
        {
            CreatedByUserId = Guid.NewGuid(),
            AppointmentForUserId = Guid.NewGuid(),
            AppointmentDateTime = DateTime.UtcNow.AddDays(7),
            Street = "",
            BuildingNumber = "123"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Street);
    }

    [Fact]
    public void Should_HaveError_When_BuildingNumberIsEmpty()
    {
        // Arrange
        var command = new CreateAppointmentCommand
        {
            CreatedByUserId = Guid.NewGuid(),
            AppointmentForUserId = Guid.NewGuid(),
            AppointmentDateTime = DateTime.UtcNow.AddDays(7),
            Street = "Main Street",
            BuildingNumber = ""
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BuildingNumber);
    }

    [Fact]
    public void Should_NotHaveError_When_CommandIsValid()
    {
        // Arrange
        var command = new CreateAppointmentCommand
        {
            CreatedByUserId = Guid.NewGuid(),
            AppointmentForUserId = Guid.NewGuid(),
            AppointmentDateTime = DateTime.UtcNow.AddDays(7),
            Street = "Main Street",
            BuildingNumber = "123"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("Oak Avenue", "456")]
    [InlineData("First Street", "1")]
    [InlineData("Market Square", "99B")]
    public void Should_NotHaveError_When_ValidLocationsProvided(string street, string buildingNumber)
    {
        // Arrange
        var command = new CreateAppointmentCommand
        {
            CreatedByUserId = Guid.NewGuid(),
            AppointmentForUserId = Guid.NewGuid(),
            AppointmentDateTime = DateTime.UtcNow.AddDays(7),
            Street = street,
            BuildingNumber = buildingNumber
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(7)]
    [InlineData(30)]
    [InlineData(365)]
    public void Should_NotHaveError_When_AppointmentIsInFuture(int daysInFuture)
    {
        // Arrange
        var command = new CreateAppointmentCommand
        {
            CreatedByUserId = Guid.NewGuid(),
            AppointmentForUserId = Guid.NewGuid(),
            AppointmentDateTime = DateTime.UtcNow.AddDays(daysInFuture),
            Street = "Main Street",
            BuildingNumber = "123"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
