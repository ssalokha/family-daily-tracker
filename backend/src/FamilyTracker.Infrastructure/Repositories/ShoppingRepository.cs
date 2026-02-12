using FamilyTracker.Application.Interfaces;
using FamilyTracker.Domain.Entities;
using FamilyTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FamilyTracker.Infrastructure.Repositories;

public class ShoppingRepository : IShoppingRepository
{
    private readonly ApplicationDbContext _context;

    public ShoppingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ShoppingItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ShoppingItems
            .Include(s => s.CreatedBy)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ShoppingItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.ShoppingItems
            .Include(s => s.CreatedBy)
            .OrderBy(s => s.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<ShoppingItem> CreateAsync(ShoppingItem item, CancellationToken cancellationToken = default)
    {
        _context.ShoppingItems.Add(item);
        await _context.SaveChangesAsync(cancellationToken);
        
        // Reload with navigation properties
        return (await GetByIdAsync(item.Id, cancellationToken))!;
    }

    public async Task UpdateAsync(ShoppingItem item, CancellationToken cancellationToken = default)
    {
        _context.ShoppingItems.Update(item);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var item = await _context.ShoppingItems.FindAsync(new object[] { id }, cancellationToken);
        if (item != null)
        {
            _context.ShoppingItems.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task ClearAllAsync(CancellationToken cancellationToken = default)
    {
        _context.ShoppingItems.RemoveRange(_context.ShoppingItems);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
