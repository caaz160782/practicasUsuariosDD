using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Roles;

public class Rol : Entity
{
    public string Nombre { get; private set; }
    public string Descripcion { get; private set; }

    private Rol(Guid id, string nombre, string descripcion) : base(id)
    {
        Nombre = nombre;
        Descripcion = descripcion;
    }

    public static Result<Rol> Create( string nombre, string descripcion)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            return Result.Failure<Rol>(RolErrores.RolNombreInvalido);
        }

        if (string.IsNullOrWhiteSpace(descripcion))
        {
            return Result.Failure<Rol>(RolErrores.RolDescripcionInvalida);
        }

        return Result.Success(new Rol(Guid.NewGuid(), nombre, descripcion));
    }

}