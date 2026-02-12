using FamilyTracker.Application.DTOs;
using MediatR;

namespace FamilyTracker.Application.Queries.Users;

public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
{
}
