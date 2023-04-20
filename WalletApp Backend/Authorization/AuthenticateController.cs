using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletApp_Backend.Authorization.Commands;
using WalletApp_Backend.Authorization.DTOs;
using WalletApp_Backend.Common;

namespace WalletApp_Backend.Authorization{
    [Route("api/authorization")]
    [AllowAnonymous]
    public class AuthenticateController : BaseController
    {
        public AuthenticateController(IMediator mediator):base(mediator){}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _mediator.Send(new LoginCommand(model.Username, model.Password));
            return Response(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var response = await _mediator.Send(new RegisterCommand(model.Username,model.Email, model.Password));
            return Response(response);
        }
    }
}