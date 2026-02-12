using FamilyTracker.Application.Interfaces;
using FamilyTracker.Domain.Entities;
using FamilyTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FamilyTracker.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ApplicationDbContext _context;

    public AppointmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DoctorAppointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.DoctorAppointments
            .Include(a => a.CreatedByUser)
            .Include(a => a.AppointmentForUser)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<DoctorAppointment>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.DoctorAppointments
            .Include(a => a.CreatedByUser)
            .Include(a => a.AppointmentForUser)
            .OrderBy(a => a.AppointmentDateTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<DoctorAppointment>> GetUpcomingAsync(int daysAhead = 14, CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        var endDate = now.AddDays(daysAhead);

        return await _context.DoctorAppointments
            .Include(a => a.CreatedByUser)
            .Include(a => a.AppointmentForUser)
            .Where(a => a.AppointmentDateTime >= now && a.AppointmentDateTime <= endDate)
            .OrderBy(a => a.AppointmentDateTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<DoctorAppointment>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.DoctorAppointments
            .Include(a => a.CreatedByUser)
            .Include(a => a.AppointmentForUser)
            .Where(a => a.AppointmentForUserId == userId || a.CreatedByUserId == userId)
            .OrderBy(a => a.AppointmentDateTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<DoctorAppointment> CreateAsync(DoctorAppointment appointment, CancellationToken cancellationToken = default)
    {
        _context.DoctorAppointments.Add(appointment);
        await _context.SaveChangesAsync(cancellationToken);
        
        // Reload with navigation properties
        return (await GetByIdAsync(appointment.Id, cancellationToken))!;
    }

    public async Task UpdateAsync(DoctorAppointment appointment, CancellationToken cancellationToken = default)
    {
        _context.DoctorAppointments.Update(appointment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var appointment = await _context.DoctorAppointments.FindAsync(new object[] { id }, cancellationToken);
        if (appointment != null)
        {
            _context.DoctorAppointments.Remove(appointment);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
