using MediatR;

namespace StockManagement.Application.User.Commands.CreateUser;

public sealed record CreateUserCommand(
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string Password,
    string PhoneNumber,
    short PhoneCountryCode) : IRequest<Unit>;