using MediatR;
using StockManagement.Domain.Errors;
using StockManagement.Domain.Repositories;
using StockManagement.Domain.Shared;

namespace StockManagement.Application.User.Commands.CreateUser;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = Domain.Identity.User.CreateNew(
            request.FirstName,
            request.LastName,
            request.PhoneNumber,
            request.Email
        );

        bool isEmailUnique = await _userRepository.IsEmailUniqueAsync(
            request.Email,
            cancellationToken
        );

        if (!isEmailUnique)
        {
            return Result.Failure<Guid>(DomainErrors.User.EmailAlreadyInUse);
        }

        var result = await _userRepository.Add(user, request.Password);

        if (!result.Succeeded)
        {
            var error = string.Join(";", result.Errors);
            return Result.Failure<Guid>(new Error("User.UserCreationFailed", error));
        }

        //using var transaction = _unitOfWork.BeginTransaction(
        //    System.Data.IsolationLevel.ReadCommitted
        //);

        //try
        //{
        //    var result = await _userRepository.Add(user, request.Password);

        //    if (!result.Succeeded)
        //    {
        //        var error = string.Join(";", result.Errors);
        //        return Result.Failure<Guid>(DomainErrors.User.UserCreationFailed);
        //    }

        //    await _unitOfWork.SaveChangesAsync(cancellationToken);
        //}
        //catch (Exception ex)
        //{
        //    transaction.Rollback();
        //}

        return user.Id;
    }
}
