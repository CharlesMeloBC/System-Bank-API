using AccountBank.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountBank.Domain.Interfaces
{
    public interface IBalanceController
    {

        Task<ActionResult<BalanceModel>>GetBalance(int id);
        Task<ActionResult<string>>UpdateBalance(AccountTransactionModel transaction);
    }
}
