using System.Text.RegularExpressions;
using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios;

public record CorreoElectronico
{
    public string Value { get; init; }

    private CorreoElectronico(string value)
    {
        Value = value;
    }

    public static Result<CorreoElectronico> Create(string value)
    {
        if (!EsCorreoValido(value))
        {
            return Result.Failure<CorreoElectronico>(UsuarioErrores.CorreoElectronicoInvalido);
        }

        return Result.Success(new CorreoElectronico(value));
    }

    public static bool EsCorreoValido(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        const string emailPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
        return Regex.Match(value, emailPattern).Success;
    }
}