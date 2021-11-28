using MediatR;

namespace SimpleLibrary.Application.Services.Book.Commands;

public class AddNewBookCommand : IRequest<Guid>
{
    public Domain.Entities.Book Book { get; set; }

    public AddNewBookCommand(Domain.Entities.Book book)
    {
        Book = book;
    }
}
