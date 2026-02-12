using MediatR;

namespace FamilyTracker.Application.Commands.Shopping;

public class SendShoppingListEmailCommand : IRequest<Unit>
{
    public Guid UserId { get; set; }
}
