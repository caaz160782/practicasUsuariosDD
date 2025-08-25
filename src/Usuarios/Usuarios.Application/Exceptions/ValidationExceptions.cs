namespace Usuarios.Application.Exceptions;

public class ValidationExceptions : Exception
{
    public IEnumerable<ValidatonError> Errors { get; }

    public ValidationExceptions(IEnumerable<ValidatonError> errors)
    {
        Errors = errors;
    }
}