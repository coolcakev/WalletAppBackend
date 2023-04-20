using Application.Models;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WalletApp_Backend.User.Entities;

namespace WalletApp_Backend.Authorization.Commands
{
    public record RegisterCommand(string UserName,string Email, string Password) : IRequest<Response<EmptyValue>>;


    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<EmptyValue>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async  Task<Response<EmptyValue>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _userManager.FindByNameAsync(request.UserName);
            if (userExists != null)
                return FailureResponses.Conflict("User already exists!");

            var user = _mapper.Map<ApplicationUser>(request);
            user.Balance = new Balance()
            {
                Value = Random.Shared.Next(0, Balance.Limit),
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return FailureResponses.BadRequest(result.Errors.Select(e => e.Description).ToArray());

            return SuccessResponses.Ok();
        }
    }
}