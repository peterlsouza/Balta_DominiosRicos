using System;
using Payment.Domain.ValueObjects;

namespace Payment.Domain.Entities
{
    public class BoletoPayment : PaymentMethod
    {
        public BoletoPayment(
                            string barcode,
                            string boletoNumber,
                            DateTime paidDate,
                            DateTime expireDate,
                            decimal total,
                            decimal totalPaid,
                            string payer,
                            Document document,
                            Address address,
                            Email email) : base(paidDate, expireDate, total, totalPaid, payer, document, address, email)
        {
            Barcode = barcode;
            BoletoNumber = boletoNumber;
        }

        public string Barcode { get; private set; }
        public string BoletoNumber { get; private set; }

    }
}
