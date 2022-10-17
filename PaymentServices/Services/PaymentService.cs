using System.Configuration;
using LearnSolidAndTesting.Data;
using PaymentServices.Data;
using PaymentServices.Types;

namespace PaymentServices.Services
{
    public class PaymentService : IPaymentService
    {
        private IDataStore accountDataStore;

        public PaymentService(IDataStore accountDataStore)
        {
            this.accountDataStore = accountDataStore;
        }
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            Account account = accountDataStore.GetAccount(request.DebtorAccountNumber);

            var result = new MakePaymentResult();

            switch (request.PaymentScheme)
            {
                case PaymentScheme.Bacs:

                    result.Success = account != null && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);

                    break;

                case PaymentScheme.FasterPayments:

                    result.Success = account != null && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) && account.Balance >= request.Amount;
                    
                    break;

                case PaymentScheme.Chaps:

                    result.Success = account != null && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) && account.Status == AccountStatus.Live;
                  
                    break;
            }

            if (result.Success)
            {
                account.Balance -= request.Amount;
                accountDataStore.UpdateAccount(account);
            }

            return result;
        }
    }
}
