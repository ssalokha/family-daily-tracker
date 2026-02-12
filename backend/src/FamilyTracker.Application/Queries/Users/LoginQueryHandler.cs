using FamilyTracker.Application.DTOs;
using FamilyTracker.Application.Interfaces;
using FamilyTracker.Domain.Exceptions;
using MediatR;

namespace FamilyTracker.Application.Queries.Users;

public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginQueryHandler(
        IUserRepository userRepository, 
        IPasswordHashService passwordHashService,
        IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUserNameAsync(request.UserName, cancellationToken);
        
        if (user == null || !_passwordHashService.VerifyPassword(request.Password, user.PasswordHash))
            throw new UnauthorizedException("Invalid username or password");

        var token = _jwtTokenService.GenerateToken(user);

        return new LoginResponseDto
        {
            Token = token,
            User = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Birthday = user.Birthday,
                UserAge = user.UserAge,
                Email = user.Email,
                UserRole = user.UserRole
            }
        };
    }
}
