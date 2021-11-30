using FluentValidation.Results;
using MediatR;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using SimpleLibrary.Domain.Shared.Events;

namespace SimpleLibrary.Infra.Bus;

public class MemoryBus : IMediatorHandler
{
    private readonly IMediator _mediator;
    private readonly IEventStore _eventStore;

    public MemoryBus(IEventStore eventStore, IMediator mediator)
    {
        _eventStore = eventStore;
        _mediator = mediator;
    }

    public async Task PublishEvent<T>(T @event) where T : Event
    {
        if (!@event.MessageType.Equals("DomainNotification"))
            _eventStore?.Save(@event);

        await _mediator.Publish(@event);
    }

    public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
    {
        return await _mediator.Send(command);
    }
}