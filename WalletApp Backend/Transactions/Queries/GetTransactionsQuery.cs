using Application.Models;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletApp_Backend.DataBase;
using WalletApp_Backend.User.Queries;

namespace WalletApp_Backend.Transactions.Queries
{
    public record GetTransactionsQueryResponse(string Id, decimal Value, string Name, string Description, DateTime CreationDate,string CreatedBy, string ApprovedBy);

    public record GetTransactionsQuery(int Amount=10) : IRequest<Response<List<GetTransactionsQueryResponse>>>;


    public class GetUserTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, Response<List<GetTransactionsQueryResponse>>>
    {
        private readonly ApplicationDbContext _context;
        public GetUserTransactionsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<GetTransactionsQueryResponse>>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var getTransactionsQueryResponses = await _context.Transactions.OrderByDescending(x=>x.CreationDate)
                    .Take(request.Amount).ProjectToType<GetTransactionsQueryResponse>().ToListAsync(cancellationToken);

            return SuccessResponses.Ok(getTransactionsQueryResponses);
        }
    }
}