using Mapster;
using WalletApp_Backend.Transactions.Entity;
using WalletApp_Backend.Transactions.Queries;

namespace WalletApp_Backend.Transactions.MappingConfiguration
{
    public class TransactionMapping  :IRegister
    {

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Transaction, GetTransactionByIdResponse>()
                    .Map(dest => dest.ApprovedBy, src => src.ApproveUser.UserName);

            config.NewConfig<Transaction, GetTransactionsQueryResponse>()
                    .Map(dest => dest.ApprovedBy, src => src.ApproveUser.UserName)
                    .Map(dest => dest.CreatedBy, src => src.CreatedBy.UserName);

        }
    }
}