using Microsoft.AspNetCore.Identity;
using WalletApp_Backend.Transactions.Entity;

namespace WalletApp_Backend.User.Entities
{
    public class ApplicationUser :IdentityUser
    {
        public int Points { get; set; }
        public IEnumerable<Transaction> CreatedTransactions { get; set; }
        public IEnumerable<Transaction> ApprovedTransactions { get; set; }
        public Balance Balance { get; set; }
    }
}