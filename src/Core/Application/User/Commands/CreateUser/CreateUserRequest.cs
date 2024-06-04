namespace StockManagement.Application.User.Commands.CreateUser;

public sealed record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PhoneNumber
);
