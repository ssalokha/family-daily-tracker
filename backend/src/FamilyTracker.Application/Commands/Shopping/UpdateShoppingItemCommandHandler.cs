using FamilyTracker.Application.DTOs;
using FamilyTracker.Application.Interfaces;
using FamilyTracker.Domain.Exceptions;
using MediatR;

namespace FamilyTracker.Application.Commands.Shopping;

public class UpdateShoppingItemCommandHandler : IRequestHandler<UpdateShoppingItemCommand, ShoppingItemDto>
{
    private readonly IShoppingRepository _shoppingRepository;
    private readonly IUserRepository _userRepository;

    public UpdateShoppingItemCommandHandler(
        IShoppingRepository shoppingRepository,
        IUserRepository userRepository)
    {
        _shoppingRepository = shoppingRepository;
        _userRepository = userRepository;
    }

    public async Task<ShoppingItemDto> Handle(UpdateShoppingItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _shoppingRepository.GetByIdAsync(request.Id, cancellationToken);
        if (item == null)
            throw new EntityNotFoundException("ShoppingItem", request.Id);

        item.Name = request.Name;
        item.Quantity = request.Quantity;
        item.UpdatedAt = DateTime.UtcNow;

        await _shoppingRepository.UpdateAsync(item, cancellationToken);

        var user = await _userRepository.GetByIdAsync(item.CreatedByUserId, cancellationToken);

        return new ShoppingItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Quantity = item.Quantity,
            CreatedByUserId = item.CreatedByUserId,
            CreatedByUserName = user?.UserName ?? "",
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt
        };
    }
}
