using AccountBank.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountBank.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        public DbSet<AccountBankModel> Accounts { get; set; }
        public DbSet<BalanceModel> Balances { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountBankModel>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<AccountBankModel>()
                .HasOne(c => c.Balance)
                .WithOne()
                .HasForeignKey<BalanceModel>(c => c.Id)
                .IsRequired();

            modelBuilder.Entity<AccountBankModel>()
                .Property(x => x.HolderName)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<AccountBankModel>()
                .Property(x => x.HolderEmail)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<AccountBankModel>()
                .Property(x => x.HolderDocuments)
                .IsRequired();

            modelBuilder.Entity<AccountBankModel>()
                .Property(x => x.Branch)
                .IsRequired()
                .HasMaxLength(5);

            modelBuilder.Entity<AccountBankModel>()
                .Property(x => x.CodeBank)
                .IsRequired()
                .HasMaxLength(3);

        }
    }
}
