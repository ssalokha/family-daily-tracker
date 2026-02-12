namespace FamilyTracker.Application.Interfaces;

public interface IEmailService
{
    Task SendShoppingListEmailAsync(string toEmail, string userName, IEnumerable<string> shoppingItems, CancellationToken cancellationToken = default);
}
