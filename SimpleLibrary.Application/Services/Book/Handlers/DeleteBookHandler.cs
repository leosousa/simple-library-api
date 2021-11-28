using MediatR;
using SimpleLibrary.Application.Services.Base;
using SimpleLibrary.Application.Services.Book.Commands;
using SimpleLibrary.Domain.Interfaces.Repositories.Base;

namespace SimpleLibrary.Application.Services.Book.Handlers;

public class DeleteBookHandler : HandlerBase, IRequestHandler<DeleteBookCommand, Guid>
{
    public DeleteBookHandler(IUnitWork unitOfWork) : base(unitOfWork)
    {

    }
    public async Task<Guid> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            _unitWork.bookRepository.Remove(request.Id);
            await _unitWork.SaveChangesAsync();
            return await Task.FromResult(request.Id);
        });
    }

}
