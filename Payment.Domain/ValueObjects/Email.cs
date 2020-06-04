using Flunt.Validations;
using Payment.Shared.ValueObject;

namespace Payment.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;

            AddNotifications(new Contract()
            .Requires()
            .IsEmail(Address, "Email.Address", "Email inválido!"));
        }

        public string Address { get; private set; }
    }
}
