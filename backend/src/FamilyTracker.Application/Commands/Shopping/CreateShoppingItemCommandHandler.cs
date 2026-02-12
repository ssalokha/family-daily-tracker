using FamilyTracker.Application.DTOs;
using FamilyTracker.Application.Interfaces;
using FamilyTracker.Domain.Entities;
using MediatR;

namespace FamilyTracker.Application.Commands.Shopping;

public class CreateShoppingItemCommandHandler : IRequestHandler<CreateShoppingItemCommand, ShoppingItemDto>
{
    private readonly IShoppingRepository _shoppingRepository;
    private readonly IUserRepository _userRepository;

    public CreateShoppingItemCommandHandler(
        IShoppingRepository shoppingRepository,
        IUserRepository userRepository)
    {
        _shoppingRepository = shoppingRepository;
        _userRepository = userRepository;
    }

    public async Task<ShoppingItemDto> Handle(CreateShoppingItemCommand request, CancellationToken cancellationToken)
    {
        var item = new ShoppingItem
        {
            Name = request.Name,
            Quantity = request.Quantity,
            CreatedByUserId = request.CreatedByUserId
        };

        var created = await _shoppingRepository.CreateAsync(item, cancellationToken);
        var user = await _userRepository.GetByIdAsync(created.CreatedByUserId, cancellationToken);

        return new ShoppingItemDto
        {
            Id = created.Id,
            Name = created.Name,
            Quantity = created.Quantity,
            CreatedByUserId = created.CreatedByUserId,
            CreatedByUserName = user?.UserName ?? "",
            CreatedAt = created.CreatedAt,
            UpdatedAt = created.UpdatedAt
        };
    }
}
