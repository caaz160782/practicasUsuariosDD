using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios;

public class UsuarioErrores
{
    public static Error PasswordInvalid => new Error("Usuario.PasswordNoValido", "El password no es valido.");
    public static Error EmailInvalid => new Error("Usuario.EmailNoValido", "El email no es valido.");
    public static Error NameUserInvalid => new Error("Usuario.NameUserNoValido", "El nombre de usuario no es valido.");
    public static Error UserNotFound => new Error("Usuario.NoEncontrado", "El usuario no fue encontrado.");
    public static Error UserIDInvalid => new Error("Usuario.idinvalid", "El id del usuario es invalido.");
    public static Error ParametrosNombreUsuarioInvalidos => new Error("UsuarioErrores.ParametrosNombreUsuarioInvalidos", "Los parámetros para generar el nombre de usuario no son válidos.");
    
    
        
        
}
