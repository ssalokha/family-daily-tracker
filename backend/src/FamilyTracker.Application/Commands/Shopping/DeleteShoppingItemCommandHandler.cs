using FamilyTracker.Application.Interfaces;
using MediatR;

namespace FamilyTracker.Application.Commands.Shopping;

public class DeleteShoppingItemCommandHandler : IRequestHandler<DeleteShoppingItemCommand, Unit>
{
    private readonly IShoppingRepository _shoppingRepository;

    public DeleteShoppingItemCommandHandler(IShoppingRepository shoppingRepository)
    {
        _shoppingRepository = shoppingRepository;
    }

    public async Task<Unit> Handle(DeleteShoppingItemCommand request, CancellationToken cancellationToken)
    {
        await _shoppingRepository.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}
