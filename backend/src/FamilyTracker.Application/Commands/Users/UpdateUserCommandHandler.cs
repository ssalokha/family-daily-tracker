using FamilyTracker.Application.DTOs;
using FamilyTracker.Application.Interfaces;
using FamilyTracker.Domain.Entities;
using FamilyTracker.Domain.Exceptions;
using MediatR;

namespace FamilyTracker.Application.Commands.Users;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
            throw new EntityNotFoundException(nameof(User), request.Id);

        user.UserName = request.UserName;
        user.Birthday = request.Birthday;
        user.Email = request.Email;
        user.UserRole = request.UserRole;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user, cancellationToken);

        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Birthday = user.Birthday,
            UserAge = user.UserAge,
            Email = user.Email,
            UserRole = user.UserRole
        };
    }
}
