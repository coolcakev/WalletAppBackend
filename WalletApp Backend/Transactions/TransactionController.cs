using MediatR;
using Microsoft.AspNetCore.Mvc;
using WalletApp_Backend.Common;
using WalletApp_Backend.Transactions.Commands;
using WalletApp_Backend.Transactions.Commands.CreateTransactionCommand;
using WalletApp_Backend.Transactions.Queries;

namespace WalletApp_Backend.Transactions{
    [Route("api/transactions")]
    public class TransactionController : BaseController
    {
        public TransactionController(IMediator mediator) : base(mediator) { }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction(int id)
        {
            var response = await _mediator.Send(new GetTransactionById(id));
            return Response(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var response = await _mediator.Send(new GetTransactionsQuery());
            return Response(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command)
        {
            var response = await _mediator.Send(command);
            return Response(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var response = await _mediator.Send(new DeleteTransactionCommand(id));
            return Response(response);
        }



    }
}