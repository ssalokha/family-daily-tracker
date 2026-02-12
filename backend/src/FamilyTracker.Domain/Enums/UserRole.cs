namespace FamilyTracker.Domain.Enums;

[Flags]
public enum UserRole
{
    User = 1,
    TabletUser = 2,
    AdminUser = 4
}
