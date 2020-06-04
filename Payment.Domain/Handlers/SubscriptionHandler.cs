using System;
using Flunt.Notifications;
using Payment.Domain.Commands;
using Payment.Domain.Entities;
using Payment.Domain.Enums;
using Payment.Domain.Repositories;
using Payment.Domain.Services;
using Payment.Domain.ValueObjects;
using Payment.Shared.Commands;
using Payment.Shared.Handlers;

namespace Payment.Domain.Handlers
{
    public class SubscriptionHandler :
        Notifiable,
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreatePayPalSubscriptionCommand>,
        IHandler<CreateCreditCardSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;
        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail fast validation
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommanResult(false, "Não foi possivel realizar sua assinatura");
            }

            //verificar se o doc ta cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já esta em uso");

            //verificar se o email ta cadastrado
            if (_repository.DocumentExists(command.Email))
                AddNotification("Email", "Este Email já esta em uso");

            //gerar as VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.Barcode,
                command.BoletoNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email);

            //relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //agrupar validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //salvar as infos
            _repository.CreateSubscription(student);

            //enviar email boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo novato!!", "Assinatura Criada");
            //retornar infos
            return new CommanResult(true, "Assinatura cadastrada com sucesso!");

        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {

            //verificar se o doc ta cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já esta em uso");

            //verificar se o email ta cadastrado
            if (_repository.DocumentExists(command.Email))
                AddNotification("Email", "Este Email já esta em uso");

            //gerar as VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email);

            //relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);


            //agrupar validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //salvar as infos
            _repository.CreateSubscription(student);

            //enviar email boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo novato!!", "Assinatura Criada");
            //retornar infos
            return new CommanResult(true, "Assinatura cadastrada com sucesso!");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {

            //verificar se o doc ta cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já esta em uso");

            //verificar se o email ta cadastrado
            if (_repository.DocumentExists(command.Email))
                AddNotification("Email", "Este Email já esta em uso");

            //gerar as VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(
                command.CardHolderName,
                command.CardNumber,
                command.LastTransactionNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email);

            //relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);


            //agrupar validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //salvar as infos
            _repository.CreateSubscription(student);

            //enviar email boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo novato!!", "Assinatura Criada");
            //retornar infos
            return new CommanResult(true, "Assinatura cadastrada com sucesso!");
        }
    }
}
