using FluentValidation;

namespace Usuarios.Application.Usuarios.CrearUsuario;

public class CrearUsuarioCommandValidator : AbstractValidator<CrearUsuarioCommand>
{
    public CrearUsuarioCommandValidator()
    {
        RuleFor(u => u.ApellidoPaterno).NotEmpty()
        .WithMessage("El apellido paterno no puede estar vacio.");

        RuleFor(u => u.FechaNacimiento).LessThan(DateTime.Now)
        .WithMessage("La fecha de nacimiento no puede ser futura");
    }
}