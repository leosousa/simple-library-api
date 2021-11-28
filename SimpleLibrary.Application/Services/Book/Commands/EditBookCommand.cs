using MediatR;

namespace SimpleLibrary.Application.Services.Book.Commands;

public class EditBookCommand : IRequest<Domain.Entities.Book>
{
    public Domain.Entities.Book Book;

    public EditBookCommand(Domain.Entities.Book book)
    {
        Book = book;
    }
}
