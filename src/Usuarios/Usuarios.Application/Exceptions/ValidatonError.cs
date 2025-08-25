namespace Usuarios.Application.Exceptions;

public record ValidatonError
(
    string PropertyName,
    string ErrorMessage
);