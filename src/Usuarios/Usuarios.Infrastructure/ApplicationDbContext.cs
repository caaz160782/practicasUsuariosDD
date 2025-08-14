using Microsoft.EntityFrameworkCore;
using Usuarios.Domain.Abstractions;

namespace Usuarios.Infrastructure;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options
        ) : base(options)
    {
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    } 

}