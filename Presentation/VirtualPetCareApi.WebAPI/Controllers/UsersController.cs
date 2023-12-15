using VirtualPetCareApi.Application.Features.Commands.AppUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VirtualPetCareApi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand createUserCommand)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommand);
            return Ok(response);
        }
    }
}
