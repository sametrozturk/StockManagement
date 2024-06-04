using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.User.Commands.CreateUser;
using StockManagement.Domain.Shared;

namespace StockManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> RegisterMember(
            [FromBody] CreateUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new CreateUserCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password,
                request.PhoneNumber
            );

            return Ok(await _mediator.Send(command, cancellationToken));
        }
    }
}
