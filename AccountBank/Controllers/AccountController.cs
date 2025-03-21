using AccountBank.Domain.Dtos;
using AccountBank.Domain.DTOs;
using AccountBank.Domain.Enums;
using AccountBank.Domain.Interfaces;
using AccountBank.Domain.Services;
using Microsoft.AspNetCore.Mvc;
namespace AccountBank.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase, IAccountBankController
    {
        private readonly BankAccountService _bankAccountService;

        public AccountController(BankAccountService bankAccountService) 
        {
            _bankAccountService = bankAccountService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountBankDto>>> GetAll()
        {
            var accounts = await _bankAccountService.GetAll();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountBankDto>> GetId(int id)
        {
            var account = await _bankAccountService.GetId(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpGet("number/{NumberAccount}")]
        public async Task<ActionResult<AccountBankDto>> GetAccountWithNumber(string NumberAccount)
        {
            var account = await _bankAccountService.GetAccountWithNumber(NumberAccount);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpGet("branch/{Branch}")]
        public async Task<ActionResult<IEnumerable<AccountBankDto>>> GetAccountWithAgenc(string Branch)
        {
            var account = await _bankAccountService.GetAccountWithAgenc(Branch);

            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpGet("holder/{holder}")]
        public async Task<ActionResult<IEnumerable<AccountBankDto>>> GetAllAccountHolder(string holder)
        {
            var account = await _bankAccountService.GetAllAccountHolder(holder);
            if(account == null)
            {
              return  NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult<AccountBankDto>> Post([FromBody] AccountBankDto accountDto)
        {
            if (accountDto == null)
            {
                return BadRequest();
            }

            var account = await _bankAccountService.Post(accountDto);

            return Ok(account);
        }

        [HttpPut("update-email/{accountId}")]
        public async Task<IActionResult> UpdateEmail(int accountId, [FromBody] UpdateEmailDto request)
        { 
           var account = await _bankAccountService.UpdateEmail(accountId, request.NewEmail);
            if (account == null)
            {
                return BadRequest();
            }
            return Ok("E-mail atualizado com sucesso."); 
        }



        [HttpPut("update-status/{accountId}")]
        public async Task<IActionResult> UpdateStatus(int accountId, [FromBody] UpdateAccountStatusDto request)
        {
            if (!Enum.TryParse<AccountStatus>(request.Status, true, out var newStatus))
            {
                return BadRequest("Status inválido.");
            }

            var account = await _bankAccountService.UpdateStatus(accountId, newStatus);
            if (account == null)
            {
                return BadRequest();
            }

            return Ok("Status atualizado com sucesso.");
        }

    }
}
