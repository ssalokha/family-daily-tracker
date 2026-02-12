using MediatR;

namespace FamilyTracker.Application.Commands.Shopping;

public class DeleteShoppingItemCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
