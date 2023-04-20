using Infrastructure;
using WalletApp_Backend.User.Services;

namespace WalletApp_Backend.User
{
    public static class ConfigService
    {
        public static IServiceCollection AddUserConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddHttpContextAccessor();
            return services;

        }
    }
}