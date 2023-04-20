using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WalletApp_Backend.User.Entities;

namespace WalletApp_Backend.Authorization.Commands
{
    public record LoginCommandResponse(string Token, DateTime Expiration);

    public record LoginCommand(string Username, string Password) : IRequest<Response<LoginCommandResponse>>;


    public class LoginCommandHandler : IRequestHandler<LoginCommand, Response<LoginCommandResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<Response<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null && !await _userManager.CheckPasswordAsync(user, request.Password))
                return FailureResponses.UnAuthorized<LoginCommandResponse>();

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

            var loginCommandResponse = new LoginCommandResponse(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
            return SuccessResponses.Ok(loginCommandResponse);
        }
    }
}