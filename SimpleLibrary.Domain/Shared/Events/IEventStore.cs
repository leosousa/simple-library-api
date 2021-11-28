namespace SimpleLibrary.Domain.Shared.Events;

public interface IEventStore
{
    void Save<T>(T theEvent) where T : class;
}
