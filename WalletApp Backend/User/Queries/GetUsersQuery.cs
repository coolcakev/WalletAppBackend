using Application.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletApp_Backend.DataBase;

namespace WalletApp_Backend.User.Queries
{
    public record GetUsersQueryResponse(string Id, string UserName, string Email);

    public record GetUsersQuery : IRequest<Response<List<GetUsersQueryResponse>>>;


    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Response<List<GetUsersQueryResponse>>>
    {
        private readonly ApplicationDbContext _context;
        public GetUsersQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<GetUsersQueryResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var getUsersQueryResponses = await _context.Users.ProjectToType<GetUsersQueryResponse>().ToListAsync(cancellationToken);
            return SuccessResponses.Ok(getUsersQueryResponses);
        }
    }
}