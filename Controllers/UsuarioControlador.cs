using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microservicio.Login.Api.Aplicacion;

namespace Microservicio.Login.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioControlador : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioControlador(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioDto>>> GetLogins()
        {
            return await _mediator.Send(new ConsultaUsuario.ListaUsuario());
        }

        // Endpoint para buscar por usuario
        [HttpGet("usuario/{usuario}")]
        public async Task<ActionResult<UsuarioDto>> GetPorUsuario(string usuario)
        {
            try
            {
                var result = await _mediator.Send(new ConsultaPorUsuario.Ejecuta
                {
                    Usuario = usuario // Búsqueda exacta (case-sensitive)
                });
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 si no existe
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearLogin([FromBody] NuevoUsuario.Ejecuta data)
        {
            await _mediator.Send(data);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarLogin([FromBody] ActualizarUsuario.EjecutaActualizar data)
        {
            try
            {
                await _mediator.Send(data);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
