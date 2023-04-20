using Application.Models;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletApp_Backend.DataBase;

namespace WalletApp_Backend.User.Queries
{
    public record GetUserTransactionsQueryResponse(string Id, decimal Value, string Name, string Description, DateTime CreationDate, string ApproveBy);

    public record GetUserTransactionsQuery(string Id) : IRequest<Response<List<GetUserTransactionsQueryResponse>>>;


    public class GetUserTransactionsQueryHandler : IRequestHandler<GetUserTransactionsQuery, Response<List<GetUserTransactionsQueryResponse>>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetUserTransactionsQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<List<GetUserTransactionsQueryResponse>>> Handle(GetUserTransactionsQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(x => x.ApprovedTransactions).ThenInclude(x => x.ApproveUser)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

            if (user == null)
                return FailureResponses.NotFound<List<GetUserTransactionsQueryResponse>>("User not found");

            var getUserTransactionsQueryResponses = _mapper.Map<List<GetUserTransactionsQueryResponse>>(user.ApprovedTransactions);
            return SuccessResponses.Ok(getUserTransactionsQueryResponses);
        }
    }
}