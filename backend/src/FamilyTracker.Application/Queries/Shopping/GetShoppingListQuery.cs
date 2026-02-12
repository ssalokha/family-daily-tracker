using FamilyTracker.Application.DTOs;
using MediatR;

namespace FamilyTracker.Application.Queries.Shopping;

public class GetShoppingListQuery : IRequest<IEnumerable<ShoppingItemDto>>
{
}
