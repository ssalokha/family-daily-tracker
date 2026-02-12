using FamilyTracker.Domain.Entities;
using FamilyTracker.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace FamilyTracker.UnitTests.Domain;

public class UserTests
{
    [Fact]
    public void UserAge_ShouldBeCalculated_FromBirthday()
    {
        // Arrange
        var birthday = new DateTime(1986, 6, 21);
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = "Sergey",
            Birthday = birthday,
            UserRole = UserRole.User,
            PasswordHash = "hashedpassword"
        };

        // Act
        var age = user.UserAge;

        // Assert
        var expectedAge = DateTime.Today.Year - birthday.Year;
        if (birthday.Date > DateTime.Today.AddYears(-expectedAge)) expectedAge--;
        
        age.Should().Be(expectedAge);
    }

    [Fact]
    public void User_ShouldHave_RequiredProperties()
    {
        // Arrange & Act
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = "TestUser",
            Birthday = new DateTime(1990, 1, 1),
            Email = "test@example.com",
            UserRole = UserRole.User,
            PasswordHash = "hashedpassword"
        };

        // Assert
        user.UserName.Should().Be("TestUser");
        user.Email.Should().Be("test@example.com");
        user.UserRole.Should().Be(UserRole.User);
        user.PasswordHash.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [InlineData(UserRole.User)]
    [InlineData(UserRole.TabletUser)]
    [InlineData(UserRole.AdminUser)]
    public void User_ShouldAccept_AllRoleTypes(UserRole role)
    {
        // Arrange & Act
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = "TestUser",
            Birthday = DateTime.Today.AddYears(-30),
            UserRole = role,
            PasswordHash = "hashedpassword"
        };

        // Assert
        user.UserRole.Should().Be(role);
    }

    [Fact]
    public void User_WithNullEmail_ShouldBeValid()
    {
        // Arrange & Act
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = "TestUser",
            Birthday = DateTime.Today.AddYears(-10),
            Email = null,
            UserRole = UserRole.User,
            PasswordHash = "hashedpassword"
        };

        // Assert
        user.Email.Should().BeNull();
    }
}
