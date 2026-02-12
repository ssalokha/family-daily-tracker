using FamilyTracker.Application.DTOs;
using FamilyTracker.Application.Interfaces;
using FamilyTracker.Domain.Exceptions;
using FamilyTracker.Domain.ValueObjects;
using MediatR;

namespace FamilyTracker.Application.Commands.Appointments;

public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, DoctorAppointmentDto>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUserRepository _userRepository;

    public UpdateAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository,
        IUserRepository userRepository)
    {
        _appointmentRepository = appointmentRepository;
        _userRepository = userRepository;
    }

    public async Task<DoctorAppointmentDto> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (appointment == null)
            throw new EntityNotFoundException("DoctorAppointment", request.Id);

        appointment.AppointmentForUserId = request.AppointmentForUserId;
        appointment.AppointmentDateTime = request.AppointmentDateTime;
        appointment.Location = new Location(request.Street, request.BuildingNumber);
        appointment.IsCompleted = request.IsCompleted;
        appointment.UpdatedAt = DateTime.UtcNow;

        await _appointmentRepository.UpdateAsync(appointment, cancellationToken);

        var createdByUser = await _userRepository.GetByIdAsync(appointment.CreatedByUserId, cancellationToken);
        var appointmentForUser = await _userRepository.GetByIdAsync(appointment.AppointmentForUserId, cancellationToken);

        return new DoctorAppointmentDto
        {
            Id = appointment.Id,
            CreatedByUserId = appointment.CreatedByUserId,
            CreatedByUserName = createdByUser?.UserName ?? "",
            AppointmentForUserId = appointment.AppointmentForUserId,
            AppointmentForUserName = appointmentForUser?.UserName ?? "",
            AppointmentDateTime = appointment.AppointmentDateTime,
            Location = appointment.Location.ToString(),
            IsCompleted = appointment.IsCompleted,
            CreatedAt = appointment.CreatedAt,
            UpdatedAt = appointment.UpdatedAt
        };
    }
}
