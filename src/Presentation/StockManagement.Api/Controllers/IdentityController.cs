using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Domain.User;

namespace StockManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        public IdentityController() { }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(string email, string password)
        {
            return NoContent();
        }
    }
}
