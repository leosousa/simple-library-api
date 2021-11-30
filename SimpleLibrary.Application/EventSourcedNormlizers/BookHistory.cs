using SimpleLibrary.Domain.Shared.Events;
using System.Text.Json;

namespace SimpleLibrary.Application.EventSourcedNormlizers;

public class BookHistory
{
    public static IList<BookHistoryData> HistoryData { get; set; }

    public static IList<BookHistoryData> ToJavaScriptCustomerHistory(IList<StoredEvent> storedEvents)
    {
        HistoryData = new List<BookHistoryData>();
        CustomerHistoryDeserializer(storedEvents);

        var sorted = HistoryData.OrderBy(c => c.Timestamp);
        var list = new List<BookHistoryData>();
        var last = new BookHistoryData();

        foreach (var change in sorted)
        {
            var jsSlot = new BookHistoryData
            {
                Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                    ? ""
                    : change.Id,
                Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                    ? ""
                    : change.Name,
                Email = string.IsNullOrWhiteSpace(change.Email) || change.Email == last.Email
                    ? ""
                    : change.Email,
                BirthDate = string.IsNullOrWhiteSpace(change.BirthDate) || change.BirthDate == last.BirthDate
                    ? ""
                    : change.BirthDate.Substring(0, 10),
                Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                Timestamp = change.Timestamp,
                Who = change.Who
            };

            list.Add(jsSlot);
            last = change;
        }
        return list;
    }

    private static void CustomerHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
    {
        foreach (var e in storedEvents)
        {
            var historyData = JsonSerializer.Deserialize<BookHistoryData>(e.Data);
            historyData.Timestamp = DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

            switch (e.MessageType)
            {
                case "BookRegisteredEvent":
                    historyData.Action = "Registered";
                    historyData.Who = e.User;
                    break;
                case "BookUpdatedEvent":
                    historyData.Action = "Updated";
                    historyData.Who = e.User;
                    break;
                case "BookRemovedEvent":
                    historyData.Action = "Removed";
                    historyData.Who = e.User;
                    break;
                default:
                    historyData.Action = "Unrecognized";
                    historyData.Who = e.User ?? "Anonymous";
                    break;

            }
            HistoryData.Add(historyData);
        }
    }
}
