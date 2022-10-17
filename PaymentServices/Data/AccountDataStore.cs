using LearnSolidAndTesting.Data;
using PaymentServices.Types;

namespace PaymentServices.Data
{
    public class AccountDataStore : IDataStore
    {
        public Account GetAccount(string accountNumber)
        {
            return new Account()
            {
                AccountNumber = accountNumber,
                Balance = 500,
                AllowedPaymentSchemes = (AllowedPaymentSchemes)2,
            };
        }

        public void UpdateAccount(Account account)
        {
        }
    }
}
