namespace Usuarios.Application.Usuarios.GetUsuario;

public record UsuarioResponse
(
    string Nombre,
    string ApellidoPaterno,
    string ApellidoMaterno,
    string NombreUsuario,
    string Email,    
    DateTime FechaNacimiento,
    string Calle,
    string Ciudad,
    string Estado,
    string CodigoPostal,
    string Pais,
    string Rol,
    DateTime fechaUltimoCambio,
    string Estado1
);