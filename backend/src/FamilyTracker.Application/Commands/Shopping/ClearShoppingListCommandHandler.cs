using FamilyTracker.Application.Interfaces;
using MediatR;

namespace FamilyTracker.Application.Commands.Shopping;

public class ClearShoppingListCommandHandler : IRequestHandler<ClearShoppingListCommand, Unit>
{
    private readonly IShoppingRepository _shoppingRepository;

    public ClearShoppingListCommandHandler(IShoppingRepository shoppingRepository)
    {
        _shoppingRepository = shoppingRepository;
    }

    public async Task<Unit> Handle(ClearShoppingListCommand request, CancellationToken cancellationToken)
    {
        await _shoppingRepository.ClearAllAsync(cancellationToken);
        return Unit.Value;
    }
}
