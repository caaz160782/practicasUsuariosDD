using Microsoft.EntityFrameworkCore;
using Usuarios.Domain.Roles;

namespace Usuarios.Infrastructure.Repositories;

internal sealed class RolRepository : Repository<Rol>, IRolRepository
{
    public RolRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Rol?> GetByNameAsync(string nombre, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Rol>().FirstOrDefaultAsync(r => r.Nombre == nombre, cancellationToken);
    }
}