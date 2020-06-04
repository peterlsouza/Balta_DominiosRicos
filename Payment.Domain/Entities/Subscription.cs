using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using Payment.Shared.Entities;

namespace Payment.Domain.Entities
{
    public class Subscription : Entity
    {
        private IList<PaymentMethod> _payments;

        public Subscription(DateTime? expireDate)
        {
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            ExpireDate = expireDate;
            Active = true;
            _payments = new List<PaymentMethod>();
        }

        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool Active { get; private set; }
        public string Address { get; private set; }
        public IReadOnlyCollection<PaymentMethod> Payments { get { return _payments.ToArray(); } }

        public void AddPayment(PaymentMethod payment)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(DateTime.Now, payment.PaidDate, "Subscription.Payments", "A data do pagamento deve ser futura"));

            //if(Valid) //só adiciona se for valido
            _payments.Add(payment);
        }

        public void Activate()
        {
            Active = true;
            LastUpdateDate = DateTime.Now;
        }

        public void Inactivate()
        {
            Active = false;
            LastUpdateDate = DateTime.Now;
        }


    }
}
