using System.ComponentModel.DataAnnotations;

namespace WalletApp_Backend.User.Entities
{
    public class Balance
    {
        public static int Limit = 1500;
        public int Id { get; set; }
        [Range(0, 1500)]
        public decimal Value { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}