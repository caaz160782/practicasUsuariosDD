namespace Usuarios.Domain.Roles;

public interface IRolRepository
{
    Task<Rol?> GetByNameAsync(string nombre, CancellationToken cancellationToken = default);
}