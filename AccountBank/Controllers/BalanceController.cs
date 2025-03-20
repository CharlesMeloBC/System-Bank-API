using AccountBank.Data;
using AccountBank.Domain.Models;
using Microsoft.AspNetCore.Mvc;

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

    }
}
