using MediatR;
using StockManagement.Domain.Repositories;
using StockManagement.Domain.ValueObjects;

namespace StockManagement.Application.User.Commands.CreateUser;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);

        return await Task.FromResult(Unit.Value);
    }
}
