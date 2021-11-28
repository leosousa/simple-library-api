using MediatR;

namespace SimpleLibrary.Application.Services.Book.Queries;

public class GetBookByIdQuery : IRequest<Domain.Entities.Book>
{
    public string Id { get; set; }

    public GetBookByIdQuery(string id)
    {
        Id = id;
    }
}
