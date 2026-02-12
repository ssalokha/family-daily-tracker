using FamilyTracker.Application.DTOs;
using FamilyTracker.Application.Interfaces;
using FamilyTracker.Domain.Entities;
using MediatR;

namespace FamilyTracker.Application.Commands.Users;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHashService passwordHashService)
    {
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = request.UserName,
            Birthday = request.Birthday,
            Email = request.Email,
            PasswordHash = _passwordHashService.HashPassword(request.Password),
            UserRole = request.UserRole
        };

        var createdUser = await _userRepository.CreateAsync(user, cancellationToken);

        return new UserDto
        {
            Id = createdUser.Id,
            UserName = createdUser.UserName,
            Birthday = createdUser.Birthday,
            UserAge = createdUser.UserAge,
            Email = createdUser.Email,
            UserRole = createdUser.UserRole
        };
    }
}
