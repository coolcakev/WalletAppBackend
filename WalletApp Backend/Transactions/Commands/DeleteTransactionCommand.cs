using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletApp_Backend.DataBase;

namespace WalletApp_Backend.Transactions.Commands
{
    public record DeleteTransactionCommand(int id) : IRequest<Response<EmptyValue>>;


    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Response<EmptyValue>>
    {
        private readonly ApplicationDbContext _context;
        public DeleteTransactionCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<EmptyValue>> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
           var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
            if (transaction == null)
                return FailureResponses.NotFound<EmptyValue>("Transaction not found");

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync(cancellationToken);
            return SuccessResponses.Ok();
        }
    }

}