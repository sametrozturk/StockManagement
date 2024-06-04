using MediatR;
using Microsoft.AspNetCore.Identity;
using StockManagement.Domain.Repositories;

namespace StockManagement.Application.User.Commands.CreateUser;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<Domain.Identity.User> _userManager;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(
        IUnitOfWork unitOfWork,
        UserManager<Domain.Identity.User> userManager,
        IUserRepository userRepository
    )
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = Domain.Identity.User.CreateNew(
            request.FirstName,
            request.LastName,
            request.PhoneNumber,
            request.Email
        );

        bool isEmailUnique = await _userRepository.IsEmailUniqueAsync(request.Email, cancellationToken);

        if (!isEmailUnique)
        {

        }

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.s)
        {

        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(Unit.Value);
    }
}
