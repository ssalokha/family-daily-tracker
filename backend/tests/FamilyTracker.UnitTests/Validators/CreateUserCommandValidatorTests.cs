using FamilyTracker.Application.Commands.Users;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace FamilyTracker.UnitTests.Validators;

public class CreateUserCommandValidatorTests
{
    private readonly CreateUserCommandValidator _validator;

    public CreateUserCommandValidatorTests()
    {
        _validator = new CreateUserCommandValidator();
    }

    [Fact]
    public void Should_HaveError_When_UserNameIsEmpty()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            UserName = "",
            Birthday = DateTime.Today.AddYears(-25),
            Password = "password123",
            UserRole = 0
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserName);
    }

    [Fact]
    public void Should_HaveError_When_PasswordIsEmpty()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            UserName = "TestUser",
            Birthday = DateTime.Today.AddYears(-25),
            Password = "",
            UserRole = 0
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Should_HaveError_When_PasswordIsTooShort()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            UserName = "TestUser",
            Birthday = DateTime.Today.AddYears(-25),
            Password = "123",
            UserRole = 0
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Should_HaveError_When_BirthdayIsInFuture()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            UserName = "TestUser",
            Birthday = DateTime.Today.AddDays(1),
            Password = "password123",
            UserRole = 0
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Birthday);
    }

    [Fact]
    public void Should_NotHaveError_When_CommandIsValid()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            UserName = "ValidUser",
            Birthday = DateTime.Today.AddYears(-25),
            Email = "test@example.com",
            Password = "securepassword123",
            UserRole = 0
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("User1", "password123")]
    [InlineData("Admin", "adminpass456")]
    [InlineData("TestUser", "testpass789")]
    public void Should_NotHaveError_When_ValidUsernamesAndPasswordsProvided(string username, string password)
    {
        // Arrange
        var command = new CreateUserCommand
        {
            UserName = username,
            Birthday = DateTime.Today.AddYears(-30),
            Password = password,
            UserRole = 0
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_NotHaveError_When_EmailIsNull()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            UserName = "TestUser",
            Birthday = DateTime.Today.AddYears(-25),
            Email = null,
            Password = "password123",
            UserRole = 0
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Should_HaveError_When_EmailIsInvalid()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            UserName = "TestUser",
            Birthday = DateTime.Today.AddYears(-25),
            Email = "invalid-email",
            Password = "password123",
            UserRole = 0
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }
}
