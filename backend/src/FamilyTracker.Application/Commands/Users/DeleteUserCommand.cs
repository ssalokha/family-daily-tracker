using MediatR;

namespace FamilyTracker.Application.Commands.Users;

public class DeleteUserCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
