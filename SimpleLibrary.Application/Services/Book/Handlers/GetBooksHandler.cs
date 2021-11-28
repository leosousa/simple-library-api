using MediatR;
using SimpleLibrary.Application.Services.Base;
using SimpleLibrary.Application.Services.Book.Queries;
using SimpleLibrary.Domain.Interfaces.Repositories.Base;

namespace SimpleLibrary.Application.Services.Book.Handlers;

public class GetBooksHandler : HandlerBase, IRequestHandler<GetBooksQuery, IEnumerable<Domain.Entities.Book>>
{
    public GetBooksHandler(IUnitWork unitWork) : base(unitWork)
    {

    }

    public async Task<IEnumerable<Domain.Entities.Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            return await Task.FromResult(_unitWork.bookRepository.List());
        });
    }
}
