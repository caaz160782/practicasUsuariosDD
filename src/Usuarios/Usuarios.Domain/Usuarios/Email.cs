using System.Text.RegularExpressions;
using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios;

public record Email
{
    public string Value { get; init; }

    private Email(string value)
    {
        Value = value;
    }

    public static Result <Email> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<Email>(UsuarioErrores.EmailInvalid);
        }
     var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (!Regex.IsMatch(value, emailPattern, RegexOptions.IgnoreCase))
        {
            return Result.Failure<Email>(UsuarioErrores.EmailInvalid);
        }

        
        return Result.Success(new Email(value));
    
    }
    
    
}
