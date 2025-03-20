using Transactions.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Transactions.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TransactionModel> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionModel>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.TransactionType).IsRequired();
                entity.Property(t => t.Amount).IsRequired().HasColumnType("decimal(18, 2)");
                entity.Property(t => t.CounterpartyBankCode).IsRequired().HasMaxLength(3);
                entity.Property(t => t.CounterpartyBankName).IsRequired().HasMaxLength(100);
                entity.Property(t => t.CounterpartyBranch).IsRequired().HasMaxLength(5);
                entity.Property(t => t.CounterpartyAccountNumber).IsRequired().HasMaxLength(20);
                entity.Property(t => t.CounterpartyHolderName).IsRequired().HasMaxLength(200);
                entity.Property(t => t.CounterpartyHolderDocument).IsRequired();
            });
        }
    }
}
