namespace SimpleLibrary.Application.EventSourcedNormlizers;

public class BookHistoryData
{
    public string Action { get; set; }
    public string Id { get; set; }
    public string Title { get; set; }
    public string PublishDate { get; set; }
    public string ISBN { get; set; }
    public string Edition { get; set; }
    public string Timestamp { get; set; }
    public string Who { get; set; }
}
