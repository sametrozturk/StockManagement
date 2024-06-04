using MediatR;
using Microsoft.AspNetCore.Identity;
using StockManagement.Domain.Repositories;
using StockManagement.Domain.ValueObjects;

namespace StockManagement.Application.User.Commands.CreateUser;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<Domain.Identity.User> _userManager;

    public CreateUserCommandHandler(
        IUnitOfWork unitOfWork,
        UserManager<Domain.Identity.User> userManager
    )
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = Domain.Identity.User.CreateNew(
            request.FirstName,
            request.LastName,
            request.PhoneNumber,
            request.Email
        );

        var result = await _userManager.CreateAsync(user, request.Password);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(Unit.Value);
    }
}
