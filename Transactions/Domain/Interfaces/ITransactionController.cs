using Microsoft.AspNetCore.Mvc;
using Transactions.Domain.DTOs;
using Transactions.Domain.Enums;

namespace Transactions.Domain.Interfaces
{
    public interface ITransactionController
    {
    public Task<IActionResult> GetById(int id);
    public Task<IActionResult> GetByBankAccountId(int bankAccountId, DateTime? startDate, DateTime? endDate, TransactionType? transactionType);
    public Task<ActionResult<IEnumerable<TransactionDto>>>GetByCounterpartyBankCode(string conterpartyBankCode);
    public Task<ActionResult<IEnumerable<TransactionDto>>>GetByCounterpartyAccountNumber(string counterpartyAccountNumber);
    public Task<ActionResult<TransactionDto>>ProcessTransaction(TransactionDto transactionDto);
    }

}

