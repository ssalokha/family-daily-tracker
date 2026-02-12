using FamilyTracker.Application.Commands.Shopping;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace FamilyTracker.UnitTests.Validators;

public class CreateShoppingItemCommandValidatorTests
{
    private readonly CreateShoppingItemCommandValidator _validator;

    public CreateShoppingItemCommandValidatorTests()
    {
        _validator = new CreateShoppingItemCommandValidator();
    }

    [Fact]
    public void Should_HaveError_When_NameIsEmpty()
    {
        // Arrange
        var command = new CreateShoppingItemCommand
        {
            Name = "",
            Quantity = 1,
            CreatedByUserId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_HaveError_When_QuantityIsZeroOrNegative()
    {
        // Arrange
        var command = new CreateShoppingItemCommand
        {
            Name = "Milk",
            Quantity = 0,
            CreatedByUserId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }

    [Fact]
    public void Should_HaveError_When_CreatedByUserIdIsEmpty()
    {
        // Arrange
        var command = new CreateShoppingItemCommand
        {
            Name = "Bread",
            Quantity = 2,
            CreatedByUserId = Guid.Empty
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CreatedByUserId);
    }

    [Fact]
    public void Should_NotHaveError_When_CommandIsValid()
    {
        // Arrange
        var command = new CreateShoppingItemCommand
        {
            Name = "Eggs",
            Quantity = 12,
            CreatedByUserId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("Milk", 1)]
    [InlineData("Bread", 2)]
    [InlineData("Eggs", 12)]
    [InlineData("Butter", 500)]
    public void Should_NotHaveError_When_ValidItemsProvided(string name, int quantity)
    {
        // Arrange
        var command = new CreateShoppingItemCommand
        {
            Name = name,
            Quantity = quantity,
            CreatedByUserId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
