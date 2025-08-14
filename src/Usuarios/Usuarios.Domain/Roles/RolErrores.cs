using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Roles;

public static class RolErrores
{
    public static Error RolNombreInvalido => new Error("RolErrores.RolNombreInvalido", "El nombre del rol no puede estar vacío o tener más de 50 caracteres.");
    public static Error RolDescripcionInvalida => new Error("RolErrores.RolDescripcionInvalida", "La descripción del rol no puede estar vacía o tener más de 200 caracteres.");
    public static Error RolNoEncontrado => new Error("RolErrores.RolNoEncontrado", "El rol no fue encontrado.");
}