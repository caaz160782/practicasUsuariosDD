using Usuarios.Application.Abstractions.Messaging;

namespace Usuarios.Application.Usuarios.CrearUsuario;

public sealed record CrearUsuarioCommand(
    string Nombre,
    string ApellidoPaterno,
    string ApellidoMaterno,
    string NombreUsuario,
    string Email,
    string Password,
    DateTime FechaNacimiento,
    string Calle,
    string Ciudad,
    string Estado,
    string CodigoPostal,
    string Pais,
    string Rol
) : ICommand<Guid>;
