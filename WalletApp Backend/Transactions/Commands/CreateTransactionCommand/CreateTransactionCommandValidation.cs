using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WalletApp_Backend.DataBase;

namespace WalletApp_Backend.Transactions.Commands.CreateTransactionCommand
{
    public class CreateTransactionCommandValidation : AbstractValidator<CreateTransactionCommand>
    {
        private readonly ApplicationDbContext _context;
        public CreateTransactionCommandValidation(ApplicationDbContext context)
        {
            _context = context;
            RuleFor(x => x.Value).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Icon).NotEmpty();
            RuleFor(x=>x.ApproveUserId).MustAsync(async (id, cancellation) =>
            {
                bool exists = await _context.Users.AnyAsync(x => x.Id == id);
                return exists;
            }).WithMessage("User not found").When(x=>!string.IsNullOrWhiteSpace(x.ApproveUserId));

        }
    }
}