using FamilyTracker.Domain.Common;
using FamilyTracker.Domain.ValueObjects;

namespace FamilyTracker.Domain.Entities;

public class DoctorAppointment : BaseEntity
{
    public Guid CreatedByUserId { get; set; }
    public Guid AppointmentForUserId { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public Location Location { get; set; } = null!;
    public bool IsCompleted { get; set; }

    // Navigation properties
    public User CreatedByUser { get; set; } = null!;
    public User AppointmentForUser { get; set; } = null!;
}
