using FamilyTracker.Domain.Entities;

namespace FamilyTracker.Application.Interfaces;

public interface IAppointmentRepository
{
    Task<DoctorAppointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<DoctorAppointment>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<DoctorAppointment>> GetUpcomingAsync(int daysAhead = 14, CancellationToken cancellationToken = default);
    Task<IEnumerable<DoctorAppointment>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<DoctorAppointment> CreateAsync(DoctorAppointment appointment, CancellationToken cancellationToken = default);
    Task UpdateAsync(DoctorAppointment appointment, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
