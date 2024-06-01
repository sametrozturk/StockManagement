using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}
