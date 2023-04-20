using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WalletApp_Backend.Transactions.Entity;
using WalletApp_Backend.User.Entities;

namespace WalletApp_Backend.DataBase
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Transaction>()
                            .HasOne(t => t.CreatedBy)
                            .WithMany(x=>x.CreatedTransactions)
                            .HasForeignKey(t => t.CreatedById)
                            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Transaction>()
                            .HasOne(t => t.ApproveUser)
                            .WithMany(x=>x.ApprovedTransactions)
                            .HasForeignKey(t => t.ApproveUserId)
                            .OnDelete(DeleteBehavior.Restrict);


        }
    }
}