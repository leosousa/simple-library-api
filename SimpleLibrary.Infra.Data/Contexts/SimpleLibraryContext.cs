using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleLibrary.Domain.Entities;
using SimpleLibrary.Infra.Data.Mappings;

namespace SimpleLibrary.Infra.Data.Contexts;

public class SimpleLibraryContext : DbContext, IDatabaseContext
{
    // Para rodar a migration de criação do banco, descomente
    //public SimpleLibraryContext()
    //{

    //}

    // Para rodar a migration de criação do banco, comente
    public SimpleLibraryContext(DbContextOptions<SimpleLibraryContext> options)
        : base(options)
    {
    }

    public DbSet<Book>? Books { get; set; }
    public DbSet<Author>? Authors { get; set; }
    public DbSet<PublishingCompany>? PublishingCompanies { get; set; }

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new BookMap());
        modelBuilder.ApplyConfiguration(new AuthorMap());
        modelBuilder.ApplyConfiguration(new PublishingCompanyMap());
    }

    public override int SaveChanges()
    {
        SetupDateRegisterOnlyAdd("CreatedAt");
        SetupDateRegisterOnlyUpdate("UpdatedAt");
        return base.SaveChanges();
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
}
