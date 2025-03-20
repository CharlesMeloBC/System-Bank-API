using Transactions.Domain.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transactions.Data;
using Transactions.Domain.Enums;
using Transactions.Domain.Models;


namespace Transactions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly BankAccountService _bankAccountService;


        public TransactionController(AppDbContext context, IMapper mapper, BankAccountService bankAccountService)
        {
            _context = context;
            _mapper = mapper;
            _bankAccountService = bankAccountService;
        }

        // 1. Buscar transação pelo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return NotFound("Transaction not found");

            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            return Ok(transactionDto);
        }

        // 2. Buscar todas as transações de uma conta bancária com filtros e ordenação
        [HttpGet("account/{bankAccountId}")]
        public async Task<IActionResult> GetByBankAccountId(
            int bankAccountId,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] TransactionType? transactionType)
        {
            var query = _context.Transactions
                .Where(t => t.BankAccountId == bankAccountId)
                .OrderByDescending(t => t.CreatedAt)
                .AsQueryable();

            if (startDate.HasValue)
                query = query.Where(t => t.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.CreatedAt <= endDate.Value);

            if (transactionType.HasValue)
                query = query.Where(t => t.TransactionType == transactionType.Value);

            var transactions = await query.ToListAsync();
            var transactionDtos = _mapper.Map<List<TransactionDto>>(transactions);

            return Ok(transactionDtos);
        }

        // 3. Buscar transações pelo código do banco e número da conta da contraparte
        [HttpGet("counterparty")]
        public async Task<IActionResult> GetByCounterparty(
            [FromQuery] string bankCode,
            [FromQuery] string accountNumber)
        {
            var transactions = await _context.Transactions
                .Where(t => t.CounterpartyBankCode == bankCode && t.CounterpartyAccountNumber == accountNumber)
                .ToListAsync();

            var transactionDtos = _mapper.Map<List<TransactionDto>>(transactions);
            return Ok(transactionDtos);
        }
        [HttpPut]
        public async Task<ActionResult<TransactionDto>> CreateTransaction([FromBody] TransactionDto transactionDto)       
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

            var transaction =  _mapper.Map<TransactionModel>(transactionDto);
          
            var updated = await _bankAccountService.UpdateBalanceAsync(transaction);

            if (!updated)
                return BadRequest("Erro ao atualizar saldo.");

            return Ok(updated);
        }
    }
}
