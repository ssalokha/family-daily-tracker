using FamilyTracker.Domain.Entities;

namespace FamilyTracker.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}
