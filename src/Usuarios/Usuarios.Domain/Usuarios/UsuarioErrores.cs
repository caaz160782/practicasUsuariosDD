using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios;

public class UsuarioErrores
{
    public static Error PasswordInvalido => new Error("UsuarioErrores.PasswordInvalido", "La contraseña debe tener al menos 6 caracteres.");
    public static Error NombreUsuarioInvalido => new Error("UsuarioErrores.NombreUsuarioInvalido", "El nombre de usuario debe tener al menos 3 caracteres.");
    public static Error CorreoElectronicoInvalido => new Error("UsuarioErrores.CorreoElectronicoInvalido", "El correo electrónico no es válido.");
    public static Error UsuarioNoEncontrado => new Error("UsuarioErrores.UsuarioNoEncontrado", "El usuario no fue encontrado.");
    public static Error UsuarioIdInvalido => new Error("UsuarioErrores.UsuarioIdInvalido", "El ID del usuario no es válido.");
    public static Error CodigoDobleFactorInvalido => new Error("UsuarioErrores.CodigoDobleFactorInvalido", "El código de doble factor debe tener al menos 6 caracteres.");
    public static Error ParametrosNombreUsuarioInvalidos => new Error("UsuarioErrores.ParametrosNombreUsuarioInvalidos", "Los parámetros para generar el nombre de usuario no son válidos.");
}