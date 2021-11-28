using MediatR;
using SimpleLibrary.Application.Services.Base;
using SimpleLibrary.Application.Services.Book.Commands;
using SimpleLibrary.Domain.Interfaces.Repositories.Base;

namespace SimpleLibrary.Application.Services.Book.Handlers;

public class EditBookHandler : HandlerBase, IRequestHandler<EditBookCommand, Domain.Entities.Book>
{
    public EditBookHandler(IUnitWork unitWork) : base(unitWork)
    {

    }

    public async Task<Domain.Entities.Book> Handle(EditBookCommand request, CancellationToken cancellationToken)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            _unitWork.bookRepository.Update(request.Book);
            await _unitWork.SaveChangesAsync();
            return await Task.FromResult(request.Book);
        });
    }
}
