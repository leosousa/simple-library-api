using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetDevPack.Data;
using NetDevPack.Domain;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using SimpleLibrary.Domain.Entities;
using SimpleLibrary.Infra.Data.Mappings;

namespace SimpleLibrary.Infra.Data.Contexts;

public class SimpleLibraryContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;

    public SimpleLibraryContext(DbContextOptions<SimpleLibraryContext> options, IMediatorHandler mediatorHandler) : base(options)
    {
        _mediatorHandler = mediatorHandler;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<PublishingCompany> PublishingCompanies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);

    //    modelBuilder.ApplyConfiguration(new BookMap());
    //    modelBuilder.ApplyConfiguration(new AuthorMap());
    //    modelBuilder.ApplyConfiguration(new PublishingCompanyMap());
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfiguration(new BookMap());
        modelBuilder.ApplyConfiguration(new AuthorMap());
        modelBuilder.ApplyConfiguration(new PublishingCompanyMap());

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        SetupDateRegisterOnlyAdd("CreatedAt");
        SetupDateRegisterOnlyUpdate("UpdatedAt");
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetupDateRegisterOnlyAdd("CreatedAt");
        SetupDateRegisterOnlyUpdate("UpdatedAt");
        return await base.SaveChangesAsync();
    }

    protected virtual void SetupDateRegisterOnlyAdd(string nameDateField)
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty(nameDateField) != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(nameDateField).CurrentValue = DateTime.Now;
            }
        }
    }

    protected virtual void SetupDateRegisterOnlyUpdate(string nameDateField)
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty(nameDateField) != null))
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                entry.Property(nameDateField).CurrentValue = DateTime.Now;
            }
        }
    }

    public async Task<bool> Commit()
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        //await _mediatorHandler.PublishDomainEvents(this).ConfigureAwait(false);
        await _mediatorHandler.PublishDomainEvents(this).ConfigureAwait(false);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var success = await SaveChangesAsync() > 0;

        return success;
    }
}

public static class MediatorExtension
{
    public static async Task PublishDomainEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        var tasks = domainEvents
            .Select(async (domainEvent) => {
                await mediator.PublishEvent(domainEvent);
            });

        await Task.WhenAll(tasks);
    }
}
