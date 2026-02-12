using FamilyTracker.Application.DTOs;
using FamilyTracker.Domain.Enums;
using MediatR;

namespace FamilyTracker.Application.Commands.Users;

public class UpdateUserCommand : IRequest<UserDto>
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
    public string? Email { get; set; }
    public UserRole UserRole { get; set; }
}
