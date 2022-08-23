using Entities.Responses;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BBBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;
        public AccountsController(
            IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        [HttpGet]
        [Route("AccountNumberExists/{accountNumber}")]
        public async Task<bool> AccountNumberExists(string accountNumber)
        {
            var result = await _accountsService.AccountNumberExists(accountNumber);
            return result;
        }

        [HttpGet]
        [Route("GetAccountByUser/{userId}")]
        public async Task<AccountByUserResponse> GetAccountByUser(string userId)
        {

            var account = await _accountsService.GetAccountByUser(userId);
            return account;

        }

        [HttpGet]
        [Route("GetAccountByAccountNumber/{accountNumber}")]
        public async Task<AccountByUserResponse> GetAccountByAccountNumber(string accountNumber)
        {
            var account = await _accountsService.GetAccountByAccountNumber(accountNumber);
            return account;
        }
    }
}
