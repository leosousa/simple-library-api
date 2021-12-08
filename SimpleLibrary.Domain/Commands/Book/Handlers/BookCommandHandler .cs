using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using SimpleLibrary.Domain.Events;
using SimpleLibrary.Domain.Interfaces.Repositories;

namespace SimpleLibrary.Domain.Commands.Book.Handlers;

public class BookCommandHandler : CommandHandler,
    IRequestHandler<RegisterNewBookCommand, ValidationResult>,
    IRequestHandler<UpdateBookCommand, ValidationResult>,
    IRequestHandler<RemoveBookCommand, ValidationResult>
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

    public async Task<ValidationResult> Handle(UpdateBookCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        var book = new Entities.Book(message.Id, message.Title, message.PublishDate, message.ISBN, message.Edition);
        var existingBook = await _bookRepository.GetByIsbn(book.ISBN);

        if (existingBook != null && existingBook.Id != book.Id)
        {
            if (!existingBook.Equals(book))
            {
                AddError("The book ISBN has already been taken.");
                return ValidationResult;
            }
        }

        book.AddDomainEvent(new BookUpdatedEvent(book.Id, book.Title, book.PublishDate, book.ISBN, book.Edition));

        _bookRepository.Update(book);

        //return await Commit(_bookRepository.UnitOfWork);
        return null;
    }

    public async Task<ValidationResult> Handle(RemoveBookCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        var book = _bookRepository.GetById(message.Id);

        if (book is null)
        {
            AddError("The book doesn't exists.");
            return ValidationResult;
        }

        book.AddDomainEvent(new BookRemovedEvent(message.Id));

        _bookRepository.Remove(book);

        //return await Commit(_bookRepository.UnitOfWork);
        return null;
    }

    //public void Dispose()
    //{
    //    _bookRepository.Dispose();
    //}
}
