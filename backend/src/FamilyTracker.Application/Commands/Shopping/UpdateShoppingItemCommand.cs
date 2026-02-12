using FamilyTracker.Application.DTOs;
using MediatR;

namespace FamilyTracker.Application.Commands.Shopping;

public class UpdateShoppingItemCommand : IRequest<ShoppingItemDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
