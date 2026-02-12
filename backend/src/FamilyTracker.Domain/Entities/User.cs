using FamilyTracker.Domain.Common;
using FamilyTracker.Domain.Enums;

namespace FamilyTracker.Domain.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
    public string? Email { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole UserRole { get; set; }

    public int UserAge
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - Birthday.Year;
            if (Birthday.Date > today.AddYears(-age))
                age--;
            return age;
        }
    }

    // Navigation properties
    public ICollection<DoctorAppointment> AppointmentsCreated { get; set; } = new List<DoctorAppointment>();
    public ICollection<DoctorAppointment> AppointmentsFor { get; set; } = new List<DoctorAppointment>();
    public ICollection<ShoppingItem> ShoppingItemsCreated { get; set; } = new List<ShoppingItem>();
}
