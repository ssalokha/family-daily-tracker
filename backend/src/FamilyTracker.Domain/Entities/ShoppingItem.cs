using FamilyTracker.Domain.Common;

namespace FamilyTracker.Domain.Entities;

public class ShoppingItem : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public Guid CreatedByUserId { get; set; }

    // Navigation property
    public User CreatedBy { get; set; } = null!;
}
