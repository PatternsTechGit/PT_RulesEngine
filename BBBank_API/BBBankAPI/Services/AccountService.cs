using Entities;
using Entities.Responses;
using Infrastructure;
using Services.Contracts;

namespace Services
{
    public class AccountService : IAccountsService
    {
        private readonly BBBankContext _bbBankContext;
        public AccountService(BBBankContext BBBankContext)
        {
            _bbBankContext = BBBankContext;
        }
        public IEnumerable<Account> GetAllAccounts()
        {
            return _bbBankContext.Accounts.ToList();
        }
        public async Task<AccountByUserResponse> GetAccountByUser(string userId)
        {
            var account =  _bbBankContext.Accounts.Where(x => x.User.Id == userId).FirstOrDefault();
            var transactions = _bbBankContext.Transactions.Where(x => x.Account.Id == account.Id).ToList(); 
            var currentBlanace = transactions.Sum(x => x.TransactionAmount);
            if (account == null)
                return null;
            else
                return new AccountByUserResponse
                {
                    AccountId = account.Id,
                    AccountNumber = account.AccountNumber,
                    AccountStatus = account.AccountStatus,
                    AccountTitle = account.AccountTitle,
                    CurrentBalance = currentBlanace,
                    UserImageUrl = account.User.ProfilePicUrl
                };
        }
        public async Task<AccountByUserResponse> GetAccountByAccountNumber(string accountNumber)
        {
            var account = _bbBankContext.Accounts.Where(x => x.AccountNumber == accountNumber).FirstOrDefault(); 
            if (account == null)
                return null;
            else
            {
                return new AccountByUserResponse
                {
                    AccountId = account.Id,
                    AccountNumber = account.AccountNumber,
                    AccountStatus = account.AccountStatus,
                    AccountTitle = account.AccountTitle,
                    CurrentBalance = account.CurrentBalance,
                    UserImageUrl = account.User.ProfilePicUrl
                };
            }
        }

        public async Task<bool> AccountNumberExists(string Accountnumber)
        {
            var account = _bbBankContext.Accounts.Find(x => x.AccountNumber == Accountnumber);
            if (account != null)
                return true;
            else
                return false;
        }

    }
}