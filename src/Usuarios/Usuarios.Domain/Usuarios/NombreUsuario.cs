using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios;

public class NombreUsuario
{
    public string Value { get; private set; }

    private NombreUsuario(string value)
    {
        Value = value;
    }

    public static Result<NombreUsuario> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
        {
            return Result.Failure<NombreUsuario>(UsuarioErrores.NameUserInvalid);
        }

        return Result.Success(new NombreUsuario(value));
    }



}
