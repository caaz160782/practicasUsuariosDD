namespace Usuarios.Domain.Usuarios;

    public record Direccion (
        string Calle,
        string Ciudad,
        string Estado,
        string CodigoPostal,
        string Pais
    );
