using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios;

public record Password
{
    public string Value { get; init; }

    private Password(string value)
    {
        Value = value;
    }
    
    public static Result<Password> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)|| value.Length < 6)
        {
            return Result.Failure<Password>(UsuarioErrores.PasswordInvalid);
        }

        return  Result.Success(new Password(value));
    }
    
}