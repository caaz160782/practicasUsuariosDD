using Usuarios.Application.Abstractions.Time;

namespace Usuarios.Infrastructure.Abstractions.Time;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime CurrentTime => DateTime.UtcNow;
}