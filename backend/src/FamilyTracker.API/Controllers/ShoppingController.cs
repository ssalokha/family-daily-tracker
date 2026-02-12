using FamilyTracker.Application.Commands.Shopping;
using FamilyTracker.Application.Queries.Shopping;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ShoppingController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShoppingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _mediator.Send(new GetShoppingListQuery());
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateShoppingItemCommand command)
    {
        var item = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateShoppingItemCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch");

        var item = await _mediator.Send(command);
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteShoppingItemCommand { Id = id });
        return NoContent();
    }

    [HttpPost("clear")]
    public async Task<IActionResult> Clear()
    {
        await _mediator.Send(new ClearShoppingListCommand());
        return NoContent();
    }

    [HttpPost("send-email")]
    public async Task<IActionResult> SendEmail([FromBody] SendShoppingListEmailCommand command)
    {
        await _mediator.Send(command);
        return Ok(new { message = "Shopping list sent successfully" });
    }
}
