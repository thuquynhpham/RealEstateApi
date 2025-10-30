using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Api.Handlers.User;
using RealEstate.Api.Handlers.User.Dtos;
using RealEstate.Core;

namespace RealEstate.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController(ILogger<UsersController> logger, IMediator mediator, IRequestContextProvider requestContextProvider) : ApiControllerBase(logger, mediator, requestContextProvider)
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto user, CancellationToken ct)
        {
            return await HandleCommandAsync(new RegisterUserCommand(user), ct);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto user, CancellationToken ct)
        {
            return await HandleQueryAsync(new SignInUserQuery(user), ct);
        }
    }
}
