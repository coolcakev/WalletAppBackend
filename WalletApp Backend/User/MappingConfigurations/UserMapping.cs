using Mapster;
using WalletApp_Backend.Transactions.Entity;
using WalletApp_Backend.User.Entities;
using WalletApp_Backend.User.Queries;

namespace WalletApp_Backend.User.MappingConfigurations
{
    public class UserMapping : IRegister
    {

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ApplicationUser, GetUserBalanceQueryResponse>()
                    .Map(dest => dest.Balance, src => src.Balance.Value)
                    .Map(dest => dest.Available, src => Balance.Limit - src.Balance.Value);

            config.NewConfig<Transaction, GetUserTransactionsQueryResponse>()
                    .Map(dest => dest.ApproveBy, src =>src.ApproveUser.UserName);

            config.NewConfig<ApplicationUser, GetUserByIdQueryResponse>()
                    .Map(dest => dest.Points, src =>src.Points/1000+"K");
        }
    }
}