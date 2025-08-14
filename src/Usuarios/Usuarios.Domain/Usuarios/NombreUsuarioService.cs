using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios;

public class NombreUsuarioService
{
    public Result<NombreUsuario> GenerarNombreUsuario(string nombre, string apellidoPaterno)
    {
        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidoPaterno))
        {
            return Result.Failure<NombreUsuario>(UsuarioErrores.ParametrosNombreUsuarioInvalidos);
        }

        var nombreUsuario = $"{nombre.ToUpper().Trim().Substring(0,1)}.{apellidoPaterno.ToUpper().Trim()}";
        return NombreUsuario.Create(nombreUsuario);
    }
}