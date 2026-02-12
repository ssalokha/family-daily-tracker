using FamilyTracker.Application.DTOs;
using MediatR;

namespace FamilyTracker.Application.Queries.Users;

public class LoginQuery : IRequest<LoginResponseDto>
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
