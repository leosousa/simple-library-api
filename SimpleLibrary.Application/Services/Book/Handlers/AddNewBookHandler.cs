using MediatR;
using SimpleLibrary.Application.Services.Base;
using SimpleLibrary.Application.Services.Book.Commands;
using SimpleLibrary.Domain.Interfaces.Repositories.Base;

namespace SimpleLibrary.Application.Services.Book.Handlers;

public class AddNewBookHandler : HandlerBase, IRequestHandler<AddNewBookCommand, Guid>
{
    public AddNewBookHandler(IUnitWork unitWork) : base(unitWork)
    {

    }

    public async Task<Guid> Handle(AddNewBookCommand request, CancellationToken cancellationToken)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            _unitWork.bookRepository.Create(request.Book);
            await _unitWork.SaveChangesAsync();
            return await Task.FromResult(request.Book.Id);
        });
    }
}
