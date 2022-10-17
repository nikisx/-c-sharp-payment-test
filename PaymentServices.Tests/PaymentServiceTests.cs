using NUnit.Framework;
using PaymentServices.Data;
using PaymentServices.Services;
using PaymentServices.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentServices.Tests
{
    public class PaymentServiceTests
    {
        [Test]
        public void PaymentService_Should_Return_True()
        {
            var dataStore = new AccountDataStore();

            var paymentService = new PaymentService(dataStore);

            var request = new MakePaymentRequest()
            {
                Amount = 300,
                PaymentScheme = PaymentScheme.Bacs,
            };

            var result = paymentService.MakePayment(request).Success;

            Assert.IsTrue(result);
        }
        [Test]
        public void PaymentService_Should_Return_False_For_Missing_Flag()
        {
            var dataStore = new AccountDataStore();

            var paymentService = new PaymentService(dataStore);

            var request = new MakePaymentRequest()
            {
                Amount = 300,
                PaymentScheme = PaymentScheme.FasterPayments,
            };

            var result = paymentService.MakePayment(request).Success;

            Assert.IsFalse(result);
        }
        [Test]
        public void PaymentService_Should_Return_False_For_Insufficient_Account_Balance()
        {
            var dataStore = new BackupAccountDataStore();

            var paymentService = new PaymentService(dataStore);

            var request = new MakePaymentRequest()
            {
                Amount = 300,
                PaymentScheme = PaymentScheme.FasterPayments,
            };

            var result = paymentService.MakePayment(request).Success;

            Assert.IsFalse(result);
        }
        [Test]
        public void PaymentService_Should_Return_True_Different_PaymentScheme()
        {
            var dataStore = new BackupAccountDataStore();

            var paymentService = new PaymentService(dataStore);

            var request = new MakePaymentRequest()
            {
                Amount = 50,
                PaymentScheme = PaymentScheme.FasterPayments,
            };

            var result = paymentService.MakePayment(request).Success;

            Assert.True(result);
        }
    }
}
