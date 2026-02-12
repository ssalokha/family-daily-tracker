using FamilyTracker.Application.DTOs;
using MediatR;

namespace FamilyTracker.Application.Commands.Shopping;

public class CreateShoppingItemCommand : IRequest<ShoppingItemDto>
{
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public Guid CreatedByUserId { get; set; }
}
