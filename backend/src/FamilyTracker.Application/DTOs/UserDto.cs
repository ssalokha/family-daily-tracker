using FamilyTracker.Domain.Enums;

namespace FamilyTracker.Application.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
    public int UserAge { get; set; }
    public string? Email { get; set; }
    public UserRole UserRole { get; set; }
}
