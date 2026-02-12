namespace FamilyTracker.Application.DTOs;

public class DoctorAppointmentDto
{
    public Guid Id { get; set; }
    public Guid CreatedByUserId { get; set; }
    public string CreatedByUserName { get; set; } = string.Empty;
    public Guid AppointmentForUserId { get; set; }
    public string AppointmentForUserName { get; set; } = string.Empty;
    public DateTime AppointmentDateTime { get; set; }
    public string Location { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
