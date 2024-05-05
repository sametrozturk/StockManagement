using MediatR;
using StockManagement.Domain.Common;
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
        ;

        var user = new Domain.User.User(
            Guid.NewGuid(),
            emailResult.Value,
            request.FirstName,
            request.LastName,
            request.PhoneNumber,
            request.PhoneCountryCode
        );



        return await Task.FromResult(Unit.Value);
    }
}
