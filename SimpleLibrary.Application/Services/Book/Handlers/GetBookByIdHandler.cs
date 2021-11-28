using MediatR;
using SimpleLibrary.Application.Services.Base;
using SimpleLibrary.Application.Services.Book.Queries;
using SimpleLibrary.Domain.Interfaces.Repositories.Base;

namespace SimpleLibrary.Application.Services.Book.Handlers;

public class GetBookByIdHandler : HandlerBase, IRequestHandler<GetBookByIdQuery, Domain.Entities.Book>
{
    public GetBookByIdHandler(IUnitWork unitWork) : base(unitWork)
    {

    }

    public async Task<Domain.Entities.Book?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        Guid id = Guid.Empty;
        if (!Guid.TryParse(request.Id, out id))
        {
            return null;
        }

        return await _retryPolicy.ExecuteAsync(async () =>
        {
            return await Task.FromResult(_unitWork.bookRepository.GetById(id));
        });
    }
}
