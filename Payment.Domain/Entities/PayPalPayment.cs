using System;
using Payment.Domain.ValueObjects;

namespace Payment.Domain.Entities
{
    public class PayPalPayment : PaymentMethod
    {
        public PayPalPayment(
                           string transactionCode,
                           DateTime paidDate,
                           DateTime expireDate,
                           decimal total,
                           decimal totalPaid,
                           string payer,
                           Document document,
                           Address address,
                           Email email) : base(paidDate, expireDate, total, totalPaid, payer, document, address, email)
        {
            TransactionCode = transactionCode;
        }

        public string TransactionCode { get; private set; }


    }
}
