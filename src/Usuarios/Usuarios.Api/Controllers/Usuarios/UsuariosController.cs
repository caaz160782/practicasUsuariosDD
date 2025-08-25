using MediatR;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Application.Usuarios.CrearUsuario;
using Usuarios.Application.Usuarios.GetUsuario;

namespace Usuarios.Api.Controllers.Usuarios;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly ISender _sender;

    public UsuariosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var query = new GetUsuarioQuery(id);
        var result = await _sender.Send(query, cancellationToken);
        return result is null ? NotFound() : Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(
        CrearUsuarioRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new CrearUsuarioCommand(
            request.Nombre,
            request.ApellidoPaterno,
            request.ApellidoMaterno,
            request.Password,
            request.CorreoElectronico,
            request.FechaNacimiento,
            request.Pais,
            request.Departamento,
            request.Ciudad,
            request.Distrito,
            request.Calle,
            request.Rol
        );

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return CreatedAtAction(nameof(GetUser), new { id = result.Value }, result.Value);
        }
        return BadRequest(result.Error);

    }
}