using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios;

public record NombreUsuario
{
    public string Value { get; init; }

    private NombreUsuario(string value)
    {
        Value = value;
    }
    public static Result<NombreUsuario> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
        {
            return Result.Failure<NombreUsuario>(UsuarioErrores.NombreUsuarioInvalido);
        }

        return Result.Success(new NombreUsuario(value));
    }
}