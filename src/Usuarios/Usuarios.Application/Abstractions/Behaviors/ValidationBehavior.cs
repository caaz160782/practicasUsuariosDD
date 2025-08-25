using FluentValidation;
using MediatR;
using Usuarios.Application.Abstractions.Messaging;
using Usuarios.Application.Exceptions;

namespace Usuarios.Application.Abstractions.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IBaseCommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationsErrors = _validators.Select(v => v.Validate(context))
        .Where(result => result.Errors.Any())
        .SelectMany(result => result.Errors)
        .Select(error =>
        new ValidatonError(
            error.PropertyName,
            error.ErrorMessage
        )).ToList();

        if (validationsErrors.Any())
        {
            throw new ValidationExceptions(validationsErrors);
        }
        return await next();
    }
}