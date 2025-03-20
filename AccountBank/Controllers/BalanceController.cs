using AccountBank.Data;
using AccountBank.Domain.Enums;
using AccountBank.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountBank.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        readonly private AppDbContext _context;
        public BalanceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BalanceModel>> GetBalance(int id)
        {
            var balance = await _context.Balances.FindAsync(id);
            if(balance == null)
            {
                return NotFound();
            }
            return Ok(balance);
        }

        [HttpPut("update-balance")]
        public async Task<ActionResult<string>> UpdateBalance([FromBody] AccountTransactionModel transaction)
        {
            var account = await _context.Accounts
                .Include(a => a.Balance)
                .FirstOrDefaultAsync(a => a.Id == transaction.BankAccountId);

            if (account == null)
            {
                return NotFound("Conta não encontrada.");
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

            return Ok("Saldo atualizado com sucesso");
        }

    }
}
