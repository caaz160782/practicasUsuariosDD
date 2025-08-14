using Usuarios.Application.Abstractions.Email;

namespace Usuarios.Infrastructure.Abstractions.Email;

internal sealed class EmailService : IEmailService
{
    public Task SendEmailAsync(string to, string subject, string body)
    {
        return Task.CompletedTask;
    }
}