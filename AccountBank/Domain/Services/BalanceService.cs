using AccountBank.Data;
using AccountBank.Domain.Enums;
using AccountBank.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountBank.Services
{
    public class BalanceService : ControllerBase
    {
        private readonly AppDbContext _context;

        public BalanceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BalanceModel> GetBalanceAsync(int id)
        {
            var balance = await _context.Balances.FindAsync(id);
            if (balance == null)
            {
                throw new KeyNotFoundException("Saldo não encontrado.");
            }
            return balance;
        }

        public async Task<string> UpdateBalanceAsync(AccountTransactionModel transaction)
        {
            var account = await _context.Accounts
                .Include(a => a.Balance)
                .FirstOrDefaultAsync(a => a.Id == transaction.BankAccountId);

            if (account == null)
            {
                throw new KeyNotFoundException("Conta não encontrada.");
            }
            if (account.Status != AccountStatus.ACTIVE)
            {
                throw new InvalidOperationException("A conta não está ativa.");
            }

            if (transaction.TransactionType == "CREDIT")
            {
                account.Balance.AddAmount(transaction.Amount);
            }
            else if (transaction.TransactionType == "DEBIT")
            {
                account.Balance.SubAmount(transaction.Amount);
            }

            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();

            return "Saldo atualizado com sucesso";
        }
    }
}
