using MediatR;
using StockManagement.Domain.Shared;

namespace StockManagement.Application.User.Commands.CreateUser;

public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PhoneNumber
) : IRequest<Result<Guid>>;
