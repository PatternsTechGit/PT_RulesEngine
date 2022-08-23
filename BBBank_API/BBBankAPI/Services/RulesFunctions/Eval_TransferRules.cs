using Entities;
using Infrastructure;
using Nito.AsyncEx.Synchronous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RulesFunctions
{
    public class Eval_TransferRules
    {
        public static bool FromAccountIsActiveWithSufficentBalance(string AccountFromId, decimal Amount, BBBankContext context)
        {
            if (IsAmountValid(Amount))
            {
                var task = CheckActiveAndSufficentBalance(AccountFromId, Amount, context);
                // because rules engine can only work with static function we have to use Nito.AsyncEx library to call another async static function to call DB
                return task.WaitAndUnwrapException();
            }
            return false;
        }
        private async static Task<bool> CheckActiveAndSufficentBalance(string AccountFromId, decimal Amount, BBBankContext context)
        {
            var account = context.Accounts.Where(x => x.AccountNumber == AccountFromId).FirstOrDefault(); 
                //await unitOfWork.AccountRepository.GetAsync(AccountFromId);
            return account != null && account.AccountStatus == AccountStatus.Active && account.CurrentBalance > Amount;
        }

        public static bool ToAccountIsActive(string AccountToId, BBBankContext context)
        {
            var task = CheckActive(AccountToId, context);
            return task.WaitAndUnwrapException();
        }
        private async static Task<bool> CheckActive(string AccountToId, BBBankContext context)
        {
            var account = context.Accounts.Where(x => x.AccountNumber == AccountToId).FirstOrDefault();
                //await unitOfWork.AccountRepository.GetAsync(AccountToId);
            return account != null && account.AccountStatus == AccountStatus.Active;
        }
        public static bool IsAmountValid(decimal amount)
        {
            return amount > 0 ? true : false;
        }
    }
}
