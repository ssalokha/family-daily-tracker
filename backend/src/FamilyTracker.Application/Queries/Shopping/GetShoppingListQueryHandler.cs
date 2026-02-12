using FamilyTracker.Application.DTOs;
using FamilyTracker.Application.Interfaces;
using MediatR;

namespace FamilyTracker.Application.Queries.Shopping;

public class GetShoppingListQueryHandler : IRequestHandler<GetShoppingListQuery, IEnumerable<ShoppingItemDto>>
{
    private readonly IShoppingRepository _shoppingRepository;
    private readonly IUserRepository _userRepository;

    public GetShoppingListQueryHandler(
        IShoppingRepository shoppingRepository,
        IUserRepository userRepository)
    {
        _shoppingRepository = shoppingRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<ShoppingItemDto>> Handle(GetShoppingListQuery request, CancellationToken cancellationToken)
    {
        var items = await _shoppingRepository.GetAllAsync(cancellationToken);
        var users = await _userRepository.GetAllAsync(cancellationToken);
        var userDict = users.ToDictionary(u => u.Id, u => u.UserName);

        return items.Select(i => new ShoppingItemDto
        {
            Id = i.Id,
            Name = i.Name,
            Quantity = i.Quantity,
            CreatedByUserId = i.CreatedByUserId,
            CreatedByUserName = userDict.GetValueOrDefault(i.CreatedByUserId) ?? "",
            CreatedAt = i.CreatedAt,
            UpdatedAt = i.UpdatedAt
        });
    }
}
