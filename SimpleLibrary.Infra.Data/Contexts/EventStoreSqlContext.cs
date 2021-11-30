using Microsoft.EntityFrameworkCore;
using SimpleLibrary.Domain.Shared.Events;
using SimpleLibrary.Infra.Data.Mappings;

namespace SimpleLibrary.Infra.Data.Contexts;

public class EventStoreSqlContext : DbContext
{
    public EventStoreSqlContext(DbContextOptions<EventStoreSqlContext> options) : base(options) { }

    public DbSet<StoredEvent> StoredEvent { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StoredEventMap());

        base.OnModelCreating(modelBuilder);
    }
}
