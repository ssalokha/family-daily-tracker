using FamilyTracker.Application.DTOs;
using FamilyTracker.Application.Interfaces;
using MediatR;

namespace FamilyTracker.Application.Queries.Users;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        
        return users.Select(u => new UserDto
        {
            Id = u.Id,
            UserName = u.UserName,
            Birthday = u.Birthday,
            UserAge = u.UserAge,
            Email = u.Email,
            UserRole = u.UserRole
        });
    }
}
