using MediatR;

namespace SimpleLibrary.Application.Services.Book.Queries;

public class GetBooksQuery : IRequest<IEnumerable<Domain.Entities.Book>>
{

}
