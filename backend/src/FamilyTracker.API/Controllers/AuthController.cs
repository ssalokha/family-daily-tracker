using FamilyTracker.Application.Commands.Users;
using FamilyTracker.Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
