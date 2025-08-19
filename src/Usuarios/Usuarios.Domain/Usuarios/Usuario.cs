using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Roles;
using Usuarios.Domain.Shared;
using Usuarios.Domain.Usuarios.Events;

namespace Usuarios.Domain.Usuarios;

public class Usuario : Entity
{
    public readonly List<DobleFactorAutenticacion> autenticacions = new();

    public string? NombrePersona { get; private set; }
    public string? ApellidoPaterno { get; private set; }
    public string? ApellidoMaterno { get; private set; }
    public Password? Password { get; private set; }
    public NombreUsuario? NombreUsuario { get; private set; }
    public DateTime FechaUltimoCambio { get; private set; }
    public DateTime FechaNacimiento { get; private set; }
    public CorreoElectronico? CorreoElectronico { get; private set; }
    public Direccion? Direccion { get; private set; }
    public Estados Estado { get; private set; }
    public IReadOnlyList<DobleFactorAutenticacion> dobleFactorAutenticacions => autenticacions.AsReadOnly();
    public Guid RolId { get; private set; }
    public Rol? Rol { get; private set; }

    private Usuario(
        Guid id,
        string? nombrePersona,
        string? apellidoPaterno,
        string? apellidoMaterno,
        Password? password,
        NombreUsuario? nombreUsuario,
        DateTime fechaUltimoCambio,
        DateTime fechaNacimiento,
        CorreoElectronico? correoElectronico,
        Direccion? direccion,
        Estados estado,
        Guid rolId) : base(id)
    {
        NombrePersona = nombrePersona;
        ApellidoPaterno = apellidoPaterno;
        ApellidoMaterno = apellidoMaterno;
        Password = password;
        NombreUsuario = nombreUsuario;
        FechaUltimoCambio = fechaUltimoCambio;
        FechaNacimiento = fechaNacimiento;
        CorreoElectronico = correoElectronico;
        Direccion = direccion;
        Estado = estado;
        RolId = rolId;
    }

    public static Result<Usuario> Create(
        string? nombrePersona,
        string? apellidoPaterno,
        string? apellidoMaterno,
        Password? password,
        DateTime fechaUltimoCambio,
        DateTime fechaNacimiento,
        CorreoElectronico? correoElectronico,
        Direccion? direccion,
        Guid rolId,
        NombreUsuarioService nombreUsuarioService)
    {
        var nombreUsuario = nombreUsuarioService.GenerarNombreUsuario(nombrePersona ?? string.Empty, apellidoPaterno ?? string.Empty);

        if (!nombreUsuario.IsSuccess)
        {
            return Result.Failure<Usuario>(nombreUsuario.Error);
        }

        var usuario = new Usuario(
            Guid.NewGuid(),
            nombrePersona,
            apellidoPaterno,
            apellidoMaterno,
            password,
            nombreUsuario.Value,
            fechaUltimoCambio,
            fechaNacimiento,
            correoElectronico,
            direccion,
            Estados.Activo,
            rolId);

        usuario.AddDomainEvent(new UserCreateDomainEvent(usuario.Id));

        return Result.Success(usuario);
    }

    public void InactivarUsuario()
    {
        if (Estado == Estados.Inactivo) return;

        Estado = Estados.Inactivo;
    }
}