using Application.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletApp_Backend.DataBase;

namespace WalletApp_Backend.Transactions.Queries
{
    public record GetTransactionByIdResponse(int Id, decimal Value, string Name, string Description, string Status, string ApprovedBy);

    public record GetTransactionById(int Id) : IRequest<Response<GetTransactionByIdResponse>>;


    public class GetTransactionByIdHandler : IRequestHandler<GetTransactionById, Response<GetTransactionByIdResponse>>
    {
        private readonly ApplicationDbContext _context;
        public GetTransactionByIdHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<GetTransactionByIdResponse>> Handle(GetTransactionById request, CancellationToken cancellationToken)
        {
           var getTransactionByIdResponse= await _context.Transactions.Where(x => x.Id == request.Id)
                   .ProjectToType<GetTransactionByIdResponse>().FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (getTransactionByIdResponse == null)
                return FailureResponses.NotFound<GetTransactionByIdResponse>("Transaction not found");

            return SuccessResponses.Ok(getTransactionByIdResponse);
        }
    }
}