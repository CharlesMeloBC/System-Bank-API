using AccountBank.Data;
using AccountBank.Domain.DTOs;
using AccountBank.Domain.Enums;
using AccountBank.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountBank.Domain.Services
{
    public class BankAccountService
    {
            private readonly IMapper _mapper;
            private readonly AppDbContext _context;

            public BankAccountService(AppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<AccountBankDto>> GetAll()
            {
                var accounts = await _context.Accounts.ToListAsync();
                return _mapper.Map<IEnumerable<AccountBankDto>>(accounts);
            }

            [HttpGet("{id}")]
            public async Task<AccountBankDto> GetId(int id)
            {
                var account = await _context.Accounts.FindAsync(id);
                return _mapper.Map<AccountBankDto>(account);
            }

            public async Task<ActionResult<AccountBankDto>> GetAccountWithNumber(string NumberAccount)
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(e => e.NumberAccount == NumberAccount);
                return _mapper.Map<AccountBankDto>(account);
            }

            public async Task<IEnumerable<AccountBankDto>>GetAccountWithAgenc(string Branch)
            {
                var account = await _context.Accounts
                    .Where(e => e.Branch == Branch)
                    .ToListAsync();

                var accountDto = _mapper.Map<List<AccountBankDto>>(account);
                return accountDto;
            }

            public async Task<IEnumerable<AccountBankDto>> GetAllAccountHolder(string holder)
            {
                var account = await _context.Accounts
                    .Where(e => e.HolderName.Trim().ToLower() == holder.ToLower().Trim())
                    .ToListAsync();
                return _mapper.Map<List<AccountBankDto>>(account);
                
            }

            public async Task<AccountBankDto>Post([FromBody] AccountBankDto accountDto)
            {
                var account = _mapper.Map<AccountBankModel>(accountDto);
                account = new AccountBankModel(
                    account.HolderName, 
                    account.TypeAccount, 
                    account.HolderEmail, 
                    account.HolderDocuments, 
                    account.HolderType);

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();

                account.Balance = new BalanceModel(account.Id);
                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();

                return _mapper.Map<AccountBankDto>(account);

            }
        public async Task<string> UpdateAccountBalance(AccountTransactionModel transaction)
        {
            var account = await _context.Accounts
                .Include(a => a.Balance)  
                .FirstOrDefaultAsync(a => a.Id == transaction.ToAccountId);  

            if (account == null)
            {
                throw new ArgumentException("Conta não encontrada.");
            }

            if (transaction.TransactionType == TransactionType.CREDIT)
            {
                account.Balance.AddAmount(transaction.Amount);
            }
            else if (transaction.TransactionType == TransactionType.DEBIT)
            {
                account.Balance.SubAmount(transaction.Amount);
            }

            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();

            return "Saldo atualizado com sucesso";
        }

    }
}
    
