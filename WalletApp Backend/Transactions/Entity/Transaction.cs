using WalletApp_Backend.User.Entities;

namespace WalletApp_Backend.Transactions.Entity
{
    public class Transaction
    {
        public int Id { get; set; }

        public TransactionType Type { get; set; }

        public decimal Value { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public TransactionStatus Status { get; set; }
        public string Icon { get; set; }

        public string CreatedById { get; set; }

        public ApplicationUser CreatedBy { get; set; }

        public string ApproveUserId { get; set; }
        public ApplicationUser ApproveUser { get; set; }

    }

    public enum TransactionType
    {
        Payment,
        Credit
    }

    public enum TransactionStatus
    {
        Pending,
        Approved,
        Rejected
    }
}