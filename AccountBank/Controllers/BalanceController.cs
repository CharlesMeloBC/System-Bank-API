using AccountBank.Domain.Interfaces;
using AccountBank.Domain.Models;
using AccountBank.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountBank.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase, IBalanceController
    {
        readonly private BalanceService _balanceService;
        public BalanceController(BalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BalanceModel>> GetBalance(int id)
        {
            var balance = await _balanceService.GetBalanceAsync(id);
            
            return Ok(balance);
        }

        [HttpPut("update-balance")]
        public async Task<ActionResult<string>> UpdateBalance([FromBody] AccountTransactionModel transaction)
        {
            var account = await _balanceService.UpdateBalanceAsync(transaction);
            if(account == null)
            {
                NotFound();
            }
            return Ok("Saldo atualizado com sucesso");
        }

    }
}
