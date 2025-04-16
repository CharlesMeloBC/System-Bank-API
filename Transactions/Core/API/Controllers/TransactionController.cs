using Transactions.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Transactions.Domain.Enums;
using Transactions.Domain.Interfaces;
using Transactions.Domain.Services;


namespace Transactions.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase, ITransactionController
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // Buscar transação pelo ID

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await _transactionService.GetById(id);
            if (transaction == null) return NotFound("Transaction not found");

            return Ok(transaction);
        }

        //Busca todas as transação pelo ID com filtros

        [HttpGet("account/{bankAccountId}")]
        public async Task<IActionResult> GetByBankAccountId(
            int bankAccountId,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] TransactionType? transactionType)
        {
            var query = await _transactionService.GetByBankAccountId(bankAccountId, startDate, endDate, transactionType);

            if (query == null || !query.Any())
                return NotFound("Nenhuma transação encontrada para os filtros especificados.");

            return Ok(query);
        }
        // Buscar transações pelo código do banco 

        [HttpGet("counterparty/bank/{conterpartyBankCode}")]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetByCounterpartyBankCode(string conterpartyBankCode)
        {
            var transactions = await _transactionService.GetByCounterpartyBankCode(conterpartyBankCode);

            return Ok(transactions.Result);
        }

        // Buscar transações pelo número da conta da contraparte

        [HttpGet("counterparty/account/{counterpartyAccountNumber}")]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetByCounterpartyAccountNumber(string counterpartyAccountNumber)
        {
            var transactions = await _transactionService.GetByCounterpartyAccountNumber(counterpartyAccountNumber);

            return Ok(transactions.Result);
        }

        // Realiza transação entre contas

        [HttpPost]
        public async Task<ActionResult<TransactionDto>> ProcessTransaction([FromBody] TransactionDto transactionDto)
        {
            var transaction = await _transactionService.ProcessTransaction(transactionDto);

            return Ok(transactionDto);
        }
    }
}
