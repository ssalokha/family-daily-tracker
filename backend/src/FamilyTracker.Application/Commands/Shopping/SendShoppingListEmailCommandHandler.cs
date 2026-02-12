using FamilyTracker.Application.Interfaces;
using FamilyTracker.Domain.Exceptions;
using MediatR;

namespace FamilyTracker.Application.Commands.Shopping;

public class SendShoppingListEmailCommandHandler : IRequestHandler<SendShoppingListEmailCommand, Unit>
{
    private readonly IShoppingRepository _shoppingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public SendShoppingListEmailCommandHandler(
        IShoppingRepository shoppingRepository,
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _shoppingRepository = shoppingRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<Unit> Handle(SendShoppingListEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
            throw new EntityNotFoundException("User", request.UserId);

        if (string.IsNullOrWhiteSpace(user.Email))
            throw new InvalidEntityStateException("User does not have an email address");

        var shoppingItems = await _shoppingRepository.GetAllAsync(cancellationToken);
        var itemList = shoppingItems.Select(i => $"{i.Name} (x{i.Quantity})").ToList();

        await _emailService.SendShoppingListEmailAsync(user.Email, user.UserName, itemList, cancellationToken);

        return Unit.Value;
    }
}
