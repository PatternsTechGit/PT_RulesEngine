using Entities.Responses;

namespace Services.Contracts
{
    public interface IAccountsService
    {
        Task<bool> AccountNumberExists(string Accountnumber);
        Task<AccountByUserResponse> GetAccountByUser(string userId);
        Task<AccountByUserResponse> GetAccountByAccountNumber(string accountNumber);
    }
}
