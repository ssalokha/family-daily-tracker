using FamilyTracker.Application.DTOs;
using FamilyTracker.Application.Interfaces;
using FamilyTracker.Domain.Entities;
using FamilyTracker.Domain.ValueObjects;
using MediatR;

namespace FamilyTracker.Application.Commands.Appointments;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, DoctorAppointmentDto>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUserRepository _userRepository;

    public CreateAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository,
        IUserRepository userRepository)
    {
        _appointmentRepository = appointmentRepository;
        _userRepository = userRepository;
    }

    public async Task<DoctorAppointmentDto> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var createdByUser = await _userRepository.GetByIdAsync(request.CreatedByUserId, cancellationToken);
        var appointmentForUser = await _userRepository.GetByIdAsync(request.AppointmentForUserId, cancellationToken);

        var appointment = new DoctorAppointment
        {
            CreatedByUserId = request.CreatedByUserId,
            AppointmentForUserId = request.AppointmentForUserId,
            AppointmentDateTime = request.AppointmentDateTime,
            Location = new Location(request.Street, request.BuildingNumber),
            IsCompleted = false
        };

        var created = await _appointmentRepository.CreateAsync(appointment, cancellationToken);

        return new DoctorAppointmentDto
        {
            Id = created.Id,
            CreatedByUserId = created.CreatedByUserId,
            CreatedByUserName = createdByUser?.UserName ?? "",
            AppointmentForUserId = created.AppointmentForUserId,
            AppointmentForUserName = appointmentForUser?.UserName ?? "",
            AppointmentDateTime = created.AppointmentDateTime,
            Location = created.Location.ToString(),
            IsCompleted = created.IsCompleted,
            CreatedAt = created.CreatedAt,
            UpdatedAt = created.UpdatedAt
        };
    }
}
