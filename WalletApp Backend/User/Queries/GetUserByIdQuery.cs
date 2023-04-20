using Application.Models;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WalletApp_Backend.User.Entities;

namespace WalletApp_Backend.User.Queries
{
    public record GetUserByIdQueryResponse(string Id,string UserName,string Email,string Points);
    public record GetUserByIdQuery(string Id) : IRequest<Response<GetUserByIdQueryResponse>>;

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<GetUserByIdQueryResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public GetUserByIdQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Response<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user =await  _userManager.FindByIdAsync(request.Id);
            if(user is null)
                return FailureResponses.NotFound<GetUserByIdQueryResponse>("User not found");

            var getUserByIdQueryResponse = _mapper.Map<GetUserByIdQueryResponse>(user);
            return SuccessResponses.Ok(getUserByIdQueryResponse);
        }
    }
}