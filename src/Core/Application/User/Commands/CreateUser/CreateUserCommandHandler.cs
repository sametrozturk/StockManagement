using MediatR;

namespace StockManagement.Application.User.Commands.CreateUser;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
{

    public CreateUserCommandHandler()
    {
    }

    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {


        return await Task.FromResult(Unit.Value);
    }
}