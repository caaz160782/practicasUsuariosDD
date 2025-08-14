using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Roles;
using Usuarios.Domain.Shared;
using Usuarios.Domain.Usuarios.Events;

namespace Usuarios.Domain.Usuarios;

public class Usuario : Entity
{
    public readonly List<DobleFactorAutenticacion> autenticacions = new();
    public string Nombre { get; private set; } = string.Empty;
    public string ApellidoPaterno { get; private set; } = string.Empty;
    public string ApellidoMaterno { get; private set; } = string.Empty;
    public NombreUsuario NombreUsuario { get; private set; } = NombreUsuario.Create(string.Empty).Value;
    public Email Email { get; private set; } = Email.Create(string.Empty).Value;
    public Password Password { get; private set; } = Password.Create(string.Empty).Value;
    public Direccion Direccion { get; private set; } = new Direccion(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
    public Estados Estado { get; private set; } = Estados.Activo;
    public DateTime FechaUltimoCambio { get; private set; } = DateTime.Now;
    public DateTime FechaNacimiento { get; private set; }
    public IReadOnlyList<DobleFactorAutenticacion> dobleFactorAutenticacions => autenticacions.AsReadOnly();
    
    public Guid RolId { get; private set; }
    public Rol? Rol { get; private set; }

    private Usuario(
         Guid id,
         string nombre,
         string apellidoPaterno,
         string apellidoMaterno,
         NombreUsuario nombreUsuario,
         Email email,
         Password password,
         Direccion direccion,
         Estados estado,
         DateTime fechaUltimoCambio,
         DateTime fechaNacimiento,
         Guid rolId
         ) : base(id)
    {
        Nombre = nombre;
        ApellidoPaterno = apellidoPaterno;
        ApellidoMaterno = apellidoMaterno;
        NombreUsuario = nombreUsuario;
        Email = email;
        Password = password;
        Direccion = direccion;
        Estado = estado;
        FechaUltimoCambio = fechaUltimoCambio;
        FechaNacimiento = fechaNacimiento;
        RolId = rolId;
    }
  
    public static Result<Usuario> Create(
        string? nombre,
        string? apellidoPaterno,
        string? apellidoMaterno,
        Password? password,
        DateTime fechaUltimoCambio,
        DateTime fechaNacimiento,
        Email email,
        Direccion direccion,
        Guid rolId,
        NombreUsuarioService nombreUsuarioService
        )
    {
        var nombreUsuario = nombreUsuarioService.GenerarNombreUsuario(nombre ?? string.Empty, apellidoPaterno ?? string.Empty);
        if (nombreUsuario.IsSuccess)
        {
            return Result.Failure<Usuario>(nombreUsuario.Error);
        }   
        var usuario = new Usuario(
            Guid.NewGuid(),
            nombre ?? string.Empty,
            apellidoPaterno ?? string.Empty,
            apellidoMaterno ?? string.Empty,
            nombreUsuario.Value,
            email,
            password ?? Password.Create(string.Empty).Value,
            direccion,
            Estados.Activo,
            fechaUltimoCambio,
            fechaNacimiento,
            rolId
        );
        usuario.AddDomaintEvent(new UserCreateDomainEvent(usuario.Id));

        return Result.Success(usuario);
    }

    public void InactivarUsuario()
    {
        if (Estado == Estados.Inactivo) return;
        Estado = Estados.Inactivo;
    }
}
