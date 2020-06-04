using System;
using Payment.Domain.ValueObjects;

namespace Payment.Domain.Entities
{
    public class CreditCardPayment : PaymentMethod
    {
        public CreditCardPayment(
                            string cardHolderName,
                            string cardNumber,
                            string lastTransactionNumber,
                            DateTime paidDate,
                            DateTime expireDate,
                            decimal total,
                            decimal totalPaid,
                            string payer,
                            Document document,
                            Address address,
                            Email email) : base(paidDate, expireDate, total, totalPaid, payer, document, address, email)
        {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            LastTransactionNumber = lastTransactionNumber;
        }

        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public string LastTransactionNumber { get; private set; }
    }
}
