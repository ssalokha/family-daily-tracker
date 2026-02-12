using FamilyTracker.Domain.Entities;

namespace FamilyTracker.Application.Interfaces;

public interface IShoppingRepository
{
    Task<ShoppingItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ShoppingItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ShoppingItem> CreateAsync(ShoppingItem item, CancellationToken cancellationToken = default);
    Task UpdateAsync(ShoppingItem item, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task ClearAllAsync(CancellationToken cancellationToken = default);
}
