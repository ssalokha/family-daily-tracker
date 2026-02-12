using FamilyTracker.Application.Commands.Users;
using FamilyTracker.Application.Interfaces;
using FamilyTracker.Domain.Entities;
using FamilyTracker.Domain.Enums;
using FluentAssertions;
using Moq;
using Xunit;

namespace FamilyTracker.UnitTests.Application.Commands;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHashService> _passwordHashServiceMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHashServiceMock = new Mock<IPasswordHashService>();
        _handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _passwordHashServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateUser_WhenCommandIsValid()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            UserName = "TestUser",
            Birthday = new DateTime(1990, 1, 1),
            Email = "test@example.com",
            Password = "password123",
            UserRole = UserRole.User
        };

        var hashedPassword = "hashed_password123";
        _passwordHashServiceMock.Setup(x => x.HashPassword(command.Password))
            .Returns(hashedPassword);

        var createdUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = command.UserName,
            Birthday = command.Birthday,
            Email = command.Email,
            PasswordHash = hashedPassword,
            UserRole = command.UserRole
        };

        _userRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdUser);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.UserName.Should().Be(command.UserName);
        result.Email.Should().Be(command.Email);
        result.UserRole.Should().Be(command.UserRole);
        result.Birthday.Should().Be(command.Birthday);

        _passwordHashServiceMock.Verify(x => x.HashPassword(command.Password), Times.Once);
        _userRepositoryMock.Verify(x => x.CreateAsync(It.Is<User>(u => 
            u.UserName == command.UserName && 
            u.PasswordHash == hashedPassword
        ), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldHashPassword_BeforeCreatingUser()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            UserName = "TestUser",
            Birthday = new DateTime(1990, 1, 1),
            Password = "plaintext_password",
            UserRole = UserRole.User
        };

        var hashedPassword = "bcrypt_hashed_password";
        _passwordHashServiceMock.Setup(x => x.HashPassword(command.Password))
            .Returns(hashedPassword);

        var createdUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = command.UserName,
            Birthday = command.Birthday,
            PasswordHash = hashedPassword,
            UserRole = command.UserRole
        };

        _userRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdUser);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _passwordHashServiceMock.Verify(x => x.HashPassword("plaintext_password"), Times.Once);
        _userRepositoryMock.Verify(x => x.CreateAsync(
            It.Is<User>(u => u.PasswordHash == hashedPassword),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnUserDto_WithCalculatedAge()
    {
        // Arrange
        var birthday = new DateTime(1990, 1, 1);
        var command = new CreateUserCommand
        {
            UserName = "TestUser",
            Birthday = birthday,
            Password = "password123",
            UserRole = UserRole.User
        };

        _passwordHashServiceMock.Setup(x => x.HashPassword(It.IsAny<string>()))
            .Returns("hashed");

        var createdUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = command.UserName,
            Birthday = birthday,
            PasswordHash = "hashed",
            UserRole = command.UserRole
        };

        _userRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdUser);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        var expectedAge = DateTime.Today.Year - birthday.Year;
        if (birthday.Date > DateTime.Today.AddYears(-expectedAge)) expectedAge--;
        
        result.UserAge.Should().Be(expectedAge);
    }

    [Theory]
    [InlineData(UserRole.User)]
    [InlineData(UserRole.TabletUser)]
    [InlineData(UserRole.AdminUser)]
    public async Task Handle_ShouldCreateUser_WithDifferentRoles(UserRole role)
    {
        // Arrange
        var command = new CreateUserCommand
        {
            UserName = "TestUser",
            Birthday = new DateTime(1990, 1, 1),
            Password = "password123",
            UserRole = role
        };

        _passwordHashServiceMock.Setup(x => x.HashPassword(It.IsAny<string>()))
            .Returns("hashed");

        var createdUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = command.UserName,
            Birthday = command.Birthday,
            PasswordHash = "hashed",
            UserRole = role
        };

        _userRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdUser);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.UserRole.Should().Be(role);
    }
}
