using MediatR;
using Microsoft.AspNetCore.Mvc;
using WalletApp_Backend.Common;
using WalletApp_Backend.User.Commands;
using WalletApp_Backend.User.Queries;

namespace WalletApp_Backend.User{
    [Route("api/users")]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator) { }

        [HttpGet("{id}/balance")]
        public async Task<IActionResult> GetUserBalance(string id)
        {
            var response = await _mediator.Send(new GetUserBalanceQuery(id));
            return Response(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _mediator.Send(new GetUsersQuery());
            return Response(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery(id));
            return Response(response);
        }

        [HttpGet("{id}/transactions")]
        public async Task<IActionResult> GetTransactions(string id)
        {
            var response = await _mediator.Send(new GetUserTransactionsQuery(id));
            return Response(response);
        }
        [HttpPost("/points")]
        public async Task<IActionResult> UpdatePoints()
        {
            var response = await _mediator.Send(new UpdateUserPointsCommand());
            return Response(response);
        }


    }
}