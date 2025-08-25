namespace Usuarios.Api.Controllers.Usuarios;

public record CrearUsuarioRequest
(
    string Nombre,
    string ApellidoPaterno,
    string ApellidoMaterno,
    string Password,
    string CorreoElectronico,
    DateTime FechaNacimiento,
    string Pais,
    string Departamento,
    string Ciudad,
    string Distrito,
    string Calle,
    string Rol
);