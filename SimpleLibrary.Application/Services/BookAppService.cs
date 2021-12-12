using AutoMapper;
using FluentValidation.Results;
using NetDevPack.Mediator;
using SimpleLibrary.Application.Interfaces;
using SimpleLibrary.Application.ViewModels;
using SimpleLibrary.Domain.Commands.Book;
using SimpleLibrary.Domain.Interfaces.Repositories;
using SimpleLibrary.Domain.Interfaces.Repositories.Base;

namespace SimpleLibrary.Application.Services;

public class BookAppService : IBookAppService
{
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly IMediatorHandler _mediator;

    public BookAppService(IMapper mapper,
                              IBookRepository bookRepository,
                              IMediatorHandler mediator,
                              IEventStoreRepository eventStoreRepository)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
        _mediator = mediator;
        _eventStoreRepository = eventStoreRepository;
    }

    public async Task<ValidationResult> Register(BookViewModel bookViewModel)
    {
        var registerCommand = _mapper.Map<RegisterNewBookCommand>(bookViewModel);
        return await _mediator.SendCommand(registerCommand);
    }

    public async Task<IEnumerable<BookViewModel>> GetAll()
    {
        return _mapper.Map<IEnumerable<BookViewModel>>(await _bookRepository.GetAll());
    }

    public async Task<BookViewModel> GetById(Guid id)
    {
        return _mapper.Map<BookViewModel>(await _bookRepository.GetById(id));
    }

    public async Task<ValidationResult> Update(BookViewModel bookViewModel)
    {
        var updateCommand = _mapper.Map<UpdateBookCommand>(bookViewModel);
        return await _mediator.SendCommand(updateCommand);
    }

    public async Task<ValidationResult> Remove(Guid id)
    {
        var removeCommand = new RemoveBookCommand(id);
        return await _mediator.SendCommand(removeCommand);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
