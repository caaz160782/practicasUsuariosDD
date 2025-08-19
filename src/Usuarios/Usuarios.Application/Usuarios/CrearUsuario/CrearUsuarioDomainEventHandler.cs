using MediatR;
using Usuarios.Application.Abstractions.Email;
using Usuarios.Domain.Usuarios;
using Usuarios.Domain.Usuarios.Events;

namespace Usuarios.Application.Usuarios.CrearUsuario;

public class CrearUsuarioDomainEventHandler : INotificationHandler<UserCreateDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly IUsuarioRepository _usuarioRepository;

    public CrearUsuarioDomainEventHandler(IEmailService emailService, IUsuarioRepository usuarioRepository)
    {
        _emailService = emailService;
        _usuarioRepository = usuarioRepository;
    }

    public async Task Handle(UserCreateDomainEvent notification, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(notification.IdUsuario, cancellationToken);

        if (usuario is null)
        {
            return;
        }

        await _emailService.SendEmailAsync(usuario.CorreoElectronico!.Value,
        "Bienvenido al sistema",
        "Bienvenido al sistema");
    }
}