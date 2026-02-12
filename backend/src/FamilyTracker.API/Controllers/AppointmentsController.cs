using FamilyTracker.Application.Commands.Appointments;
using FamilyTracker.Application.Queries.Appointments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcoming([FromQuery] int daysAhead = 14)
    {
        var appointments = await _mediator.Send(new GetUpcomingAppointmentsQuery { DaysAhead = daysAhead });
        return Ok(appointments);
    }

    [HttpGet("history")]
    [Authorize(Roles = "AdminUser")]
    public async Task<IActionResult> GetHistory(
        [FromQuery] string? userId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] bool? isCompleted)
    {
        var appointments = await _mediator.Send(new GetAppointmentHistoryQuery
        {
            UserId = userId,
            StartDate = startDate,
            EndDate = endDate,
            IsCompleted = isCompleted
        });
        return Ok(appointments);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand command)
    {
        var appointment = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetUpcoming), new { id = appointment.Id }, appointment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAppointmentCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch");

        var appointment = await _mediator.Send(command);
        return Ok(appointment);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteAppointmentCommand { Id = id });
        return NoContent();
    }
}
