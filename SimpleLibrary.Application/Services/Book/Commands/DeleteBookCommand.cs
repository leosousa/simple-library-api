using MediatR;

namespace SimpleLibrary.Application.Services.Book.Commands;

public class DeleteBookCommand : IRequest<Guid>
{
    public Guid Id;

    public DeleteBookCommand(Guid id)
    {
        Id = id;
    }
}
