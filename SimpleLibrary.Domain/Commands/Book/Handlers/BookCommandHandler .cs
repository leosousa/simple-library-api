using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using SimpleLibrary.Domain.Events;
using SimpleLibrary.Domain.Interfaces.Repositories;

namespace SimpleLibrary.Domain.Commands.Book.Handlers;

public class BookCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewBookCommand, ValidationResult>
{
    private readonly IBookRepository _bookRepository;

    public BookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<ValidationResult> Handle(RegisterNewBookCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        var book = new Entities.Book(Guid.NewGuid(), message.Title, message.PublishDate, message.ISBN, message.Edition);

        if (_bookRepository.Get(b => b.ISBN == message.ISBN) != null)
        {
            AddError("O livro já está cadastrado.");
            return ValidationResult;
        }

        book.AddDomainEvent(new BookRegisteredEvent(book.Id, book.Title, book.PublishDate, book.ISBN, book.Edition));

        _bookRepository.Create(book);

        //return await Commit(_bookRepository.UnitOfWork);
        return null;
    }

    //public async Task<ValidationResult> Handle(UpdateCustomerCommand message, CancellationToken cancellationToken)
    //{
    //    if (!message.IsValid()) return message.ValidationResult;

    //    var customer = new Customer(message.Id, message.Name, message.Email, message.BirthDate);
    //    var existingCustomer = await _bookRepository.GetByEmail(customer.Email);

    //    if (existingCustomer != null && existingCustomer.Id != customer.Id)
    //    {
    //        if (!existingCustomer.Equals(customer))
    //        {
    //            AddError("The customer e-mail has already been taken.");
    //            return ValidationResult;
    //        }
    //    }

    //    customer.AddDomainEvent(new CustomerUpdatedEvent(customer.Id, customer.Name, customer.Email, customer.BirthDate));

    //    _bookRepository.Update(customer);

    //    return await Commit(_bookRepository.UnitOfWork);
    //}

    //public async Task<ValidationResult> Handle(RemoveCustomerCommand message, CancellationToken cancellationToken)
    //{
    //    if (!message.IsValid()) return message.ValidationResult;

    //    var customer = await _bookRepository.GetById(message.Id);

    //    if (customer is null)
    //    {
    //        AddError("The customer doesn't exists.");
    //        return ValidationResult;
    //    }

    //    customer.AddDomainEvent(new CustomerRemovedEvent(message.Id));

    //    _bookRepository.Remove(customer);

    //    return await Commit(_bookRepository.UnitOfWork);
    //}

    //public void Dispose()
    //{
    //    _bookRepository.Dispose();
    //}
}
