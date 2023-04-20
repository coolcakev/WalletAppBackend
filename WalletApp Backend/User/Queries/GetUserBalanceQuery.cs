using Application.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WalletApp_Backend.User.Entities;

namespace WalletApp_Backend.User.Queries
{
    public record GetUserBalanceQueryResponse(decimal Balance, decimal Available);
    public record GetUserBalanceQuery(string Id) : IRequest<Response<GetUserBalanceQueryResponse>>;

    public class GetUserBalanceQueryHandler : IRequestHandler<GetUserBalanceQuery, Response<GetUserBalanceQueryResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public GetUserBalanceQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<GetUserBalanceQueryResponse>> Handle(GetUserBalanceQuery request, CancellationToken cancellationToken)
        {
           var balanceQueryResponse=await _userManager.Users.Where(x=>x.Id==request.Id).Take(1).ProjectToType<GetUserBalanceQueryResponse>().FirstOrDefaultAsync(cancellationToken: cancellationToken);
           if (balanceQueryResponse==null)
                 return FailureResponses.NotFound<GetUserBalanceQueryResponse>("User not found");

           return SuccessResponses.Ok(balanceQueryResponse);
        }
    }
}