using LearnSolidAndTesting.Data;
using PaymentServices.Types;

namespace PaymentServices.Data
{
    public class BackupAccountDataStore : IDataStore
    {
        public Account GetAccount(string accountNumber)
        {
            return new Account()
            {
                AccountNumber = accountNumber,
                Balance = 100,
                AllowedPaymentSchemes = (AllowedPaymentSchemes)1,
            };
        }

        public void UpdateAccount(Account account)
        {
        }
    }
}
