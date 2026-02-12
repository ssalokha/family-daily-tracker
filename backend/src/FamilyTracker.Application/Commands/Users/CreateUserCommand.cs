using FamilyTracker.Application.DTOs;
using FamilyTracker.Domain.Enums;
using MediatR;

namespace FamilyTracker.Application.Commands.Users;

public class CreateUserCommand : IRequest<UserDto>
{
    public string UserName { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; } = string.Empty;
    public UserRole UserRole { get; set; }
}
