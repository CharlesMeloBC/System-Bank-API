using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transactions.Data;
using Transactions.Domain.DTOs;
using Transactions.Domain.Enums;
using Transactions.Domain.Models;

namespace Transactions.Domain.Services
{
    public class TransactionService : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly BankAccountService _bankAccountService;
        public TransactionService(AppDbContext context, IMapper mapper, BankAccountService bankAccountService)
        {
            _context = context;
            _mapper = mapper;
            _bankAccountService = bankAccountService;
        }
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            return Ok(transactionDto);
        }

        public async Task<List<TransactionDto>> GetByBankAccountId(
            int bankAccountId,
            DateTime? startDate,
            DateTime? endDate,
            TransactionType? transactionType)
        {
            // Consulta inicial para obter transações da conta bancária
            var query = _context.Transactions
                .Where(t => t.BankAccountId == bankAccountId)
                .OrderByDescending(t => t.CreatedAt)
                .AsQueryable();

            // Filtro por data inicial (se fornecido)
            if (startDate.HasValue)
                query = query.Where(t => t.CreatedAt >= startDate.Value);

            // Filtro por data final (se fornecido)
            if (endDate.HasValue)
                query = query.Where(t => t.CreatedAt <= endDate.Value);

            // Filtro por tipo de transação (se fornecido)
            if (transactionType.HasValue)
                query = query.Where(t => t.TransactionType == transactionType.Value);

            // Executa a consulta e mapeia para o DTO
            var transactions = await query.ToListAsync();
            var transactionDtos = _mapper.Map<List<TransactionDto>>(transactions);

            return transactionDtos;
        }
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetByCounterpartyBankCode(string conterpartyBankCode)
        {
            if (string.IsNullOrEmpty(conterpartyBankCode))
                return NotFound("Bank code not provided.");

            var transactions = await _context.Transactions
                .Where(e => e.CounterpartyBankCode == conterpartyBankCode)
                .ToListAsync();

            if (!transactions.Any())
                return NotFound("No transactions found for this bank code.");

            var transactionList = _mapper.Map<List<TransactionDto>>(transactions);
            return Ok(transactionList);
        }

        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetByCounterpartyAccountNumber(string counterpartyAccountNumber)
        {
            if (string.IsNullOrEmpty(counterpartyAccountNumber))
                return NotFound("Account number not provided.");

            var transactions = await _context.Transactions
                .Where(e => e.CounterpartyAccountNumber == counterpartyAccountNumber)
                .ToListAsync();

            if (!transactions.Any())
                return NotFound("No transactions found for this account number.");

            var transactionList = _mapper.Map<List<TransactionDto>>(transactions);
            return Ok(transactionList);
        }

        public async Task<ActionResult<TransactionDto>> ProcessTransaction(TransactionDto transactionDto)
        {

            if (transactionDto == null)
                return BadRequest("Transação inválida.");

            var balance = await _bankAccountService.GetBalanceAsync(transactionDto.BankAccountId);
            if (balance == null)
            {
                return NotFound("Conta não encontrada.");
            }

            if (transactionDto.TransactionType == TransactionType.DEBIT && balance.Amount > transactionDto.Amount)
            {
                return BadRequest("Saldo insuficiente.");
            }

            var transaction = _mapper.Map<TransactionModel>(transactionDto);

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            var updated = await _bankAccountService.UpdateBalanceAsync(transaction);
            if (!updated)
                return BadRequest("Erro ao atualizar saldo.");

            transactionDto.Id = transaction.Id;
            transactionDto.CreatedAt = transaction.CreatedAt;
            return Ok(transactionDto);
        }

    }
}









