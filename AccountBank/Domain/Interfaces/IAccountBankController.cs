using AccountBank.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AccountBank.Domain.Interfaces
{
    public interface IAccountBankController
    {
        public Task<ActionResult<IEnumerable<AccountBankDto>>> GetAll();

        public Task<ActionResult<AccountBankDto>> GetId(int id);

        public Task<ActionResult<AccountBankDto>> GetAccountWithNumber(string NumberAccount);

        public Task<ActionResult<IEnumerable<AccountBankDto>>> GetAccountWithAgenc(string Branch);

        public Task<ActionResult<IEnumerable<AccountBankDto>>> GetAllAccountHolder(string holder);

        public Task<ActionResult<AccountBankDto>> Post([FromBody] AccountBankDto accountDto);

    }
}
