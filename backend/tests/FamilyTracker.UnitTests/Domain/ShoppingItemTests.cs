using FamilyTracker.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace FamilyTracker.UnitTests.Domain;

public class ShoppingItemTests
{
    [Fact]
    public void ShoppingItem_ShouldInitialize_WithRequiredProperties()
    {
        // Arrange & Act
        var item = new ShoppingItem
        {
            Id = Guid.NewGuid(),
            Name = "Milk",
            Quantity = 2,
            CreatedByUserId = Guid.NewGuid()
        };

        // Assert
        item.Id.Should().NotBeEmpty();
        item.Name.Should().Be("Milk");
        item.Quantity.Should().Be(2);
        item.CreatedByUserId.Should().NotBeEmpty();
    }

    [Theory]
    [InlineData("Milk", 1)]
    [InlineData("Bread", 2)]
    [InlineData("Eggs", 12)]
    [InlineData("Butter", 500)]
    public void ShoppingItem_ShouldAccept_DifferentNamesAndQuantities(string name, int quantity)
    {
        // Arrange & Act
        var item = new ShoppingItem
        {
            Id = Guid.NewGuid(),
            Name = name,
            Quantity = quantity,
            CreatedByUserId = Guid.NewGuid()
        };

        // Assert
        item.Name.Should().Be(name);
        item.Quantity.Should().Be(quantity);
    }

    [Fact]
    public void ShoppingItem_ShouldHave_CreatedAndUpdatedTimestamps()
    {
        // Arrange
        var now = DateTime.UtcNow;
        var item = new ShoppingItem
        {
            Id = Guid.NewGuid(),
            Name = "Milk",
            Quantity = 2,
            CreatedByUserId = Guid.NewGuid(),
            CreatedAt = now,
            UpdatedAt = now
        };

        // Assert
        item.CreatedAt.Should().BeCloseTo(now, TimeSpan.FromSeconds(1));
        item.UpdatedAt.Should().BeCloseTo(now, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void ShoppingItem_Quantity_CanBeUpdated()
    {
        // Arrange
        var item = new ShoppingItem
        {
            Id = Guid.NewGuid(),
            Name = "Milk",
            Quantity = 2,
            CreatedByUserId = Guid.NewGuid()
        };

        // Act
        item.Quantity = 5;

        // Assert
        item.Quantity.Should().Be(5);
    }

    [Fact]
    public void ShoppingItem_Name_CanBeUpdated()
    {
        // Arrange
        var item = new ShoppingItem
        {
            Id = Guid.NewGuid(),
            Name = "Milk",
            Quantity = 2,
            CreatedByUserId = Guid.NewGuid()
        };

        // Act
        item.Name = "Bread";

        // Assert
        item.Name.Should().Be("Bread");
    }
}
