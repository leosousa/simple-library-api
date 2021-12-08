using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLibrary.Domain.Commands.Book;

public abstract class BookCommand : Command
{
    public Guid Id { get; protected set; }
    public string Title { get; protected set; }
    public DateTime PublishDate { get; protected set; }
    public string ISBN { get; protected set; }
    public int Edition { get; protected set; }
}
