using MediatR;
using Microsoft.EntityFrameworkCore;
using Usuarios.Domain.Abstractions;

namespace Usuarios.Infrastructure;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IPublisher publisher
        ) : base(options)
    {
        _publisher = publisher;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            await PubllishDomainEventsAsync();
            return result;
        }
        catch (System.Exception ex)
        {
            // Log the exception or handle it as needed
            throw new InvalidOperationException("Error detecting changes in the context.", ex);
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);        
    }

    private async Task PubllishDomainEventsAsync()
    {
        var domainEntities = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();
                entity.ClearDomainEvents();
                return domainEvents;
            }).ToList();

        foreach (var domainEvent in domainEntities)
        {
            await _publisher.Publish(domainEvent, CancellationToken.None);
        }
    }

}