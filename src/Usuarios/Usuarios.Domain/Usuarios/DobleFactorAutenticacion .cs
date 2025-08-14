using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Shared;

namespace Usuarios.Domain.Usuarios;

public class DobleFactorAutenticacion : Entity
{
    public Guid UsuarioId { get; private set; }
    public Usuario? Usuario { get; private set; }
    public string Codigo { get; private set; }
    public Estados Estado { get; private set; } = Estados.Activo;
    public DateTime FechaCreacion { get; private set; } = DateTime.Now;
    public TipoDobleFactor TipoDobleFactor { get; private set; }

    private DobleFactorAutenticacion(
        Guid id,
        Guid usuarioId,
        string codigo,
        Estados estado,
        TipoDobleFactor tipoDobleFactor): base(id)
    {
        UsuarioId = usuarioId;
        Codigo = codigo;
        Estado = estado;
        FechaCreacion = DateTime.Now;
        TipoDobleFactor = tipoDobleFactor;
    }
    public static Result<DobleFactorAutenticacion> Create(Guid usuarioId, string codigo, Estados estado, TipoDobleFactor tipoDobleFactor)
    {
        if (usuarioId == Guid.Empty)
        {
            return Result.Failure<DobleFactorAutenticacion>(UsuarioErrores.UserIDInvalid);
        }

        if (string.IsNullOrWhiteSpace(codigo) || codigo.Length < 6)
        {
            return Result.Failure<DobleFactorAutenticacion>(new Error("DobleFactor.CodigoInvalido", "El código debe tener al menos 6 caracteres."));
        }
        
        return Result.Success(new DobleFactorAutenticacion(new Guid(),usuarioId, codigo, estado, tipoDobleFactor));     
    }
}
